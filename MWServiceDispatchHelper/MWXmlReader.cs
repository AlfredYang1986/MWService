/**
 * Created by Alfred Yang
 * 30-8-2013
 * 
 * the basic element that this architecture has
 * which means that every Server have to have the Components element 
 * to specific what messages that this server can handle
 * 
 * if the server shall pass the message to another server
 * is shall have the dispatch component
 * 
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
    class MWXmlReader
    {
        private XmlDocument _doc = new XmlDocument();
        private XmlElement _root;

        public XmlDocument doc { get { return _doc; } }

        public void initDocument(String path)
        {
            try
            {
                _doc.Load(path);
                _root = _doc.DocumentElement;
            }
            catch (System.Exception ex)
            {
                Console.Out.WriteLine(ex.Message);	
            }
        }

        public void createServiceComponents()
        {
            XmlNodeList components = _doc.SelectNodes(MWServiceConstConfig.strComponentPath);
            
            foreach (XmlNode component in components)
            {
                XmlAttributeCollection attrs = component.Attributes;
                string strFactoryName = attrs.GetNamedItem(MWServiceConstConfig.strFactoryTag).Value;
                Type typeFactoryType = Type.GetType(MWServiceConstConfig.strNameSpace + "." + strFactoryName);

                // all component factory only have one parameter
                Type[] paramTypes = new Type[2];
                paramTypes[0] = Type.GetType(MWServiceConstConfig.strSystemObjectType);
                paramTypes[1] = Type.GetType(MWServiceConstConfig.strStringType);

                // method
                MethodInfo method = typeFactoryType.GetMethod(MWServiceConstConfig.strCreateMethod, paramTypes);
                
                // parameters
                Object[] parameters = new Object[2];
                parameters[0] = component;
                parameters[1] = attrs.GetNamedItem(MWServiceConstConfig.strIDTag).Value;

                // component
                MWAbstrctComponent serverComponent =
                    (MWAbstrctComponent)method.Invoke(typeFactoryType, parameters);

                Singleton.GetInstance<MWComponentManagement>().PushComponent(serverComponent);
            }
        }
    }
}
