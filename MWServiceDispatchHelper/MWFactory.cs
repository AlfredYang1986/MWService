using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MWServiceDispatchHelper
{
    public class MWFactory { }

    class MWAbstractFactory : MWFactory 
    {
        public static MWAbstrctComponent MWCreate(Object o, string strClassName) 
        {
            if (!(o is XmlNode)) return default(MWAbstrctComponent);

            Type classType = Type.GetType(MWServiceConstConfig.strNameSpace + "." + strClassName);

            ConstructorInfo constructor =
                classType.GetConstructor(Type.EmptyTypes);

            MWAbstrctComponent com = (MWAbstrctComponent)constructor.Invoke(null);
            com.setUpServerComponent(o);
            return com;
        }
    }

    class MWAbstactClassFactory : MWFactory
    {
        public static ComponentType MWCreate<ComponentType>(Type classType)
        {
            ConstructorInfo constructor =
                classType.GetConstructor(Type.EmptyTypes);

            return (ComponentType)constructor.Invoke(null);
        }
    }
}
