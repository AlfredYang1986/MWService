using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWServiceDispatchHelper
{
    abstract class MWAbstactClient
    {
        protected List<string> capableRequest = new List<string>();

        public void instertMessage(string messageName) { capableRequest.Add(messageName); }

        public virtual Boolean canRequest(string strMessageName)
        {
            return capableRequest.Contains(strMessageName);
        }

        abstract public Object request(string strMessagename, string strRequestBosy);

        abstract public string getClientName();
        abstract public string getClientFactoryName();
    }
}