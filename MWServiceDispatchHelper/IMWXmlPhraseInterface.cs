using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MWServiceDispatchHelper
{
    /************************************************************************/
    /* Factory method for the Dll                                           */
    /************************************************************************/
    public class MWXmlPhraseInterfaceFactory
    {
        /************************************************************************/
        /* singleton for one Application                                        */
        /************************************************************************/
        static IMWXmlPhraseInterface _impl = new MWXmlPhraseService();

        public static IMWXmlPhraseInterface NewInstance()
        {
            return _impl;
        }
    }

    public interface IMWXmlPhraseInterface : IDisposable
    {
        string getRequestInterface(string messageName);

        void initXMLReaderApplication();
    }
}
