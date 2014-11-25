using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MWDispatchService
{
    class MWPropertyFactory
    {
        private const string pref = "MWDispatchService.MW";
        private const string suff = "Proxy";

        public static Object MWCreateProperty(Type classType)
        {
            ConstructorInfo constructor = classType.GetConstructor(Type.EmptyTypes);
            Object o = constructor.Invoke(null);
            return o;
        }

        public static Object MWCreateProperty(string strPropertyName)
        {
            if (isBuildInProperty(strPropertyName))
            {
                strPropertyName = System.Globalization.CultureInfo.
                    CurrentCulture.TextInfo.ToTitleCase(strPropertyName);
                strPropertyName += suff;
            }

            strPropertyName = pref + strPropertyName;
            Type t = Type.GetType(strPropertyName);
            return MWCreateProperty(t);
        }

        public static Boolean isBuildInProperty(string strPropertyName)
        {
            Boolean bReVal = false;
            string[] buildInType = { "string", "int", "float", "bool" };
            foreach (string type in buildInType)
            {
                if (strPropertyName.ToLower().Equals(type))
                {
                    bReVal = true;
                    break;
                }
            }
            return bReVal;
        }

        public static void toBuildInValue(string value, ref Object o)
        {

        }
    }
}
