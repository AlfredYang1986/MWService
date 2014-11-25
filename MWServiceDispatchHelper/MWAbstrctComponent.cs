/**
 * base class of Component
 * Command design pattern
 * Created by Alfred Yang
 * 31-8-2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MWServiceDispatchHelper
{
    abstract class MWAbstrctComponent
    {
        protected List<MWAbstactClient> remoteInterfaces = new List<MWAbstactClient>();

        public virtual void setUpServerComponent(Object setUpSource)
        {
            if (!(setUpSource is XmlNode)) return;

            XmlNodeList subList = ((XmlNode)setUpSource).ChildNodes;
            foreach (XmlNode sub in subList)
            {
                string strSubName = sub.Attributes.GetNamedItem(MWServiceConstConfig.strRef).Value;
                XmlNode node = Singleton.GetInstance<MWServerApplication>()
                    .XmlReaderInstance.doc.SelectSingleNode(
                    MWServiceConstConfig.strSubPath + strSubName + MWServiceConstConfig.strInterfacetag);

                XmlAttributeCollection attrs = node.Attributes;
                string strFactoryName = attrs.GetNamedItem(MWServiceConstConfig.strFactoryTag).Value;
                string strClassName = attrs.GetNamedItem(MWServiceConstConfig.strIDTag).Value;
                Type typeFactoryType = Type.GetType(MWServiceConstConfig.strNameSpace + "." + strFactoryName);

                // all component factory only have one parameter
                Type[] paramTypes = new Type[1];
                paramTypes[0] = Type.GetType(MWServiceConstConfig.strStringType);

                // method
                MethodInfo method = typeFactoryType.GetMethod(MWServiceConstConfig.strCreateMethod, paramTypes);

                // parameters
                Object[] parameters = new Object[1];
                parameters[0] = strClassName;

                // component
                MWAbstactClient client =
                    (MWAbstactClient)method.Invoke(typeFactoryType, parameters);
                remoteInterfaces.Add(client);
                initInterfaceMessage(node, client);
            }
        }

        public virtual MWAbstactClient canHandleRequest(String strRequestName)
        {
            foreach (MWAbstactClient client in remoteInterfaces)
            {
                if (client.canRequest(strRequestName))
                    return client;
            }
            return null;
        }

        private void initInterfaceMessage(XmlNode InterfaceNode, MWAbstactClient client)
        {
            XmlNodeList messageList = InterfaceNode.ChildNodes;
            foreach (XmlNode message in messageList)
            {
                client.instertMessage(message.Attributes.
                    GetNamedItem(MWServiceConstConfig.strIDTag).Value);
            }
        }
    }
}
