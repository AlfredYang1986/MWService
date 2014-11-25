using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWDispatchService
{
    abstract class MWBulidInProxy
    {
        abstract public Object toBuildInValue(string strValue); 
    }

    class MWStringProxy : MWBulidInProxy
    {
        public override Object toBuildInValue(string strValue)
        {
            return strValue as Object;
        } 
    }

    class MWIntProxy : MWBulidInProxy
    {
        public override Object toBuildInValue(string strValue)
        {
            return int.Parse(strValue) as Object;
        }
    }

    class MWFloatProxy : MWBulidInProxy
    {
        public override Object toBuildInValue(string strValue)
        {
            return float.Parse(strValue);
        }
    }

    class MWBoolProxy : MWBulidInProxy
    {
        public override Object toBuildInValue(string strValue)
        {
            return bool.Parse(strValue);
        }
    }
}
