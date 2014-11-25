using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MWDispatchService
{
    class MWComponentFactory
    {
        private const string pref = "MWDispatchService.";
        private const string suffer = "Component";

        public static T MWCreateComponent<T>(Type classType)
        {
            ConstructorInfo constructor = classType.GetConstructor(Type.EmptyTypes);
            Object o = constructor.Invoke(null);
            return (T)o;
        }

        public static T MWCreateComponent<T>(string strTypeName)
        {
            strTypeName = pref + strTypeName + suffer;
            Type t = Type.GetType(strTypeName);
            return MWCreateComponent<T>(t);
        }
    }
}
