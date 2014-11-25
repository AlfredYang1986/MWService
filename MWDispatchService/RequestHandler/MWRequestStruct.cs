/************************************************************************/
/* phrase the string to the specific Server                             */
/* the return type shall consist as follows                             */
/*      1. Service Name                                                 */
/*      2. Message Name                                                 */
/*      3. Argument List                                                */
/*      4. Return List                                                  */
/*                                                                      */
/*  Created By Alfred Yang                                              */
/*  11-09-2013                                                          */
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MWDispatchService
{
    class MWRequestStruct
    {
        private string strServerName;
        private string strMessageName;
        private IDictionary<string, Object> argvs;
        private List<Object> reVals;

        public string ServerName 
        { 
            get { return strServerName; } 
            set { strServerName = value; } 
        }
        
        public string MessageName 
        { 
            get { return strMessageName; } 
            set { strMessageName = value; } 
        }

        public IDictionary<string, Object> Arguments
        {
            get { return argvs; }
            set { argvs = value; }
        }

        public List<Object> Returns
        {
            get { return reVals; }
            set { reVals = value; }
        }

        public string invokes()
        {
            MWAbstractComponent component = 
                MWComponentFactory.MWCreateComponent<MWAbstractComponent>(strServerName);

            Type[] paramTypes = new Type[1];
            paramTypes[0] = argvs["param"].GetType();
            MethodInfo method = component.GetType().GetMethod(strMessageName, paramTypes);

            Object[] parameters = new Object[1];
            parameters[0] = argvs["param"];

            Object reVal = method.Invoke(component, parameters);

            return reVal.ToString();
        }
    }
}
