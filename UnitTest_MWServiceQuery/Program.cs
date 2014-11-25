using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; 

namespace UnitTest_MWServiceQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            string queryString = "Searching[input(string)Navy]";
            string url = String.Format("http://localhost:1247/Service.svc/request/{0}", queryString);

            WebClient serviceRequest = new WebClient();
            string response = serviceRequest.DownloadString(new Uri(url));

            System.Console.WriteLine(response);
        }
    }
}
