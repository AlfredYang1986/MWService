using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MWServiceDispatchHelper
{
    public class MWXmlPhraseService : IMWXmlPhraseInterface
    {
        public void Dispose()
        {

        }

        private Boolean bInit = false; 

        public void initXMLReaderApplication()
        {
            if (!bInit)
            {
                MWServerApplication app = Singleton.GetInstance<MWServerApplication>();
                app.XmlReaderInstance.initDocument(app.XMLPath);
                app.XmlReaderInstance.createServiceComponents();
                bInit = true;
            }
        }

        public string getRequestInterface(string messageName)
        {
            initXMLReaderApplication();
            MWServerApplication app = Singleton.GetInstance<MWServerApplication>();
            return app.getServiceInterface(messageName);
        }
    }
}
