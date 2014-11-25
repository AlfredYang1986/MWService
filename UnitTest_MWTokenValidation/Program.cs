using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UnitTest_MWTokenValidation
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
           * Created by Ayush
           * Testing Authorization (validate token) via OAuth protocol
           */

            string token = "2jmkHXst0UjUHYzZjwCyBOmACZjs+EJ+";


            // Check the Authorisation here
            string url = String.Format("http://localhost:1296/Service.svc/ValidateToken/{0}",
                token
                );
            WebClient serviceRequest = new WebClient();
            string response = serviceRequest.DownloadString(new Uri(url));
            System.Console.WriteLine(response);

            XDocument source = XDocument.Parse(response);

            var authorisationOutput = source.Descendants(XName.Get("boolean", source.Root.Name.NamespaceName)).First().Value;
            //Find a better way to check this
            if (authorisationOutput.ToString().ToLower().Contains("false"))
            {
                //Token Expired
                System.Console.WriteLine(authorisationOutput.ToString());
                //return "Token Expired";
            }
            System.Console.Read();
        }
    }
}
