/**
 * Created by Alfred Yang
 * 30-8-2013
 * Application Entrance
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MWServiceDispatchHelper
{
    class MWServerApplication
    {
        private string strPath = Path.Combine(HostingEnvironment.MapPath("~/Config"), @"\XMLFile1.xml");
        private MWXmlReader _xmlReader = new MWXmlReader();

        public string getServiceInterface(string strMessage)
        {
            bool bHandle = false;
            string reVal = null;
            IList<MWAbstrctComponent> list = Singleton.GetInstance<MWComponentManagement>().ComponentList;
            foreach (MWAbstrctComponent com in list)
            {
                MWAbstactClient client = com.canHandleRequest(strMessage);
                if (client != null)
                {
                    reVal = client.getClientName();
                    bHandle = true;
                    break;
                }
            }
            if (!bHandle)
                Console.WriteLine("Cannot handle the request!");

            return reVal;
        }

        void startTestServerFactory()
        {
            bool isRunning = true;
            while (isRunning)
            {
                bool bHandle = false;
                string strRequest = Console.ReadLine();

                if (strRequest.Length == 0) continue;
                strRequest = strRequest.Trim();
                if (strRequest.Equals("q")) break;

                int index = strRequest.IndexOf(" ");
                if (index < 0)
                {
                    Console.WriteLine("please input parameters!!");
                    continue;
                }

                string strMessage = strRequest.Substring(0, index);
                strRequest = strRequest.Substring(strRequest.IndexOf(" ") + 1);
                IList<MWAbstrctComponent> list = Singleton.GetInstance<MWComponentManagement>().ComponentList;
                foreach (MWAbstrctComponent com in list)
                {
                    MWAbstactClient client = com.canHandleRequest(strMessage);
                    if (client != null)
                    {
                        client.request(strMessage, strRequest);
                        bHandle = true;
                        break;
                    }
                }
                if (!bHandle)
                    Console.WriteLine("Cannot handle the request!");
                
            }
        }

        public MWXmlReader XmlReaderInstance { get { return _xmlReader; } }
        public string XMLPath { get { return strPath; } }

        //static void Main(string[] args)
        //{
        //    MWServerApplication app = Singleton.GetInstance<MWServerApplication>();
        //    app.XmlReaderInstance.initDocument(strPath);
        //    app.XmlReaderInstance.createServiceComponents();
        //    app.startTestServerFactory();
        //}
    }
}
