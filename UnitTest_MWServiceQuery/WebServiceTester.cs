using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.ServiceModel.Web;

namespace UnitTest_MWServiceQuery
{
    public class WebServiceTester
    {
        //////////////////////////////////////////////////////////////////
        //                                                              //
        //  Get results for the query string from DB w/ web service     //
        //                                                              //
        //////////////////////////////////////////////////////////////////

        public void TestString(String str)
        {
            //string queryString = "Searching|[input(string)I want a Navy clothing]";

            string url = String.Format("http://localhost:1247/Service.svc/request");

            WebClient serviceRequest = new WebClient();
            
            
            /************************
             *  Ayush Edit Begins   *
             ***********************/
            string token = "4u8Rsdcz0UjUHYzZjwCyBOmACZjs+EJ+";
            string myJSON = "";
            try
            {
                myJSON  = "{ \"Request\":"
                                                    + " { \"Token\": \""+ token +"\","
                                                    + " \"RequestString\": \"Searching|[input(string) Nike  ]\"  "
                                                    + " } "
                                             + "}";
            }
            catch (Exception e)
            {
                Console.Write("");
            }
            serviceRequest.Headers[HttpRequestHeader.ContentType] = "application/json";
            //string HtmlResult = serviceRequest.UploadString(url,"POST", myJSON);
            string response = string.Empty;
            //= serviceRequest.DownloadString(new Uri(url));

            HttpWebRequest request = (HttpWebRequest)
                             WebRequest.Create(new Uri(url));
            request.Method = "POST";
            request.ContentType = "application/json";

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(myJSON);
            writer.Close();

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                response = responseText.ToString();
            }

            if (response.ToLower().Contains("token"))
            {
                System.Console.WriteLine("Token Expired!!!!");
                System.Console.Read();
                return;
            }

            /*
             *  The Part of XML returned from the web service which is important for this test
             *  is as below:
             *  
             *  <Cloth>
             *      <Brand>
             *          Nike
             *      </Brand>
             *      <Color>
             *          Dark Grey Heather &amp; Black
             *      </Color>
             *      <Gender>
             *          1
             *      </Gender>
             *      <ImamgeUrl>http://static.theiconic.com.au/p/nike-0789-147521-1-product.jpg</ImamgeUrl>
             *      <ItemID>14</ItemID>
             *      <Price>40</Price>
             *      <Size>S</Size>
             *      <SizeType>null</SizeType>
             *      <Title>
             *          Nike Fly Shorts 2.0
             *      </Title>
             *  </Cloth>
            */

            //////////////////////////////////////////////////////////
            //                                                        //
            // Code from Alfred to parse XML result from Web Service  //
            //                     Begin                             //
            /////////////////////////////////////////////////////////
            var xDoc = new XmlDocument();
            xDoc.LoadXml(response);
            var innerXml = xDoc.InnerText;
            XDocument source = XDocument.Parse(innerXml);

            var outputfromLinq = from item in source.Descendants(XName.Get("Cloth", source.Root.Name.NamespaceName))
                                 select new
                                 {
                                     Brand = item.Element(XName.Get("Brand", source.Root.Name.NamespaceName)).Value,
                                     Color = item.Element(XName.Get("Color", source.Root.Name.NamespaceName)).Value,
                                     //ItemID = item.Element(XName.Get("ItemID", source.Root.Name.NamespaceName)).Value,
                                     Title = item.Element(XName.Get("Title", source.Root.Name.NamespaceName)).Value,
                                     Gender = item.Element(XName.Get("Gender", source.Root.Name.NamespaceName)).Value,
                                     Price = Double.Parse(item.Element(XName.Get("Price", source.Root.Name.NamespaceName)).Value),
                                     ImgUrl = item.Element(XName.Get("ImamgeUrl", source.Root.Name.NamespaceName)).Value,
                                 };
            //////////////////////////////////////////////////////////
            //                                                        //
            // Code from Alfred to parse XML result from Web Service  //
            //                     End                               //
            /////////////////////////////////////////////////////////
            int x = 0;



            //Code to get rid of the same valued output from Web Service to check actual number of output generated
            var distinctoutput = (from all in outputfromLinq
                                  select all).Distinct();

            foreach (var thisone in distinctoutput)
            {
                x++;
                System.Console.WriteLine("No." + x +", "+ thisone.Title + ", " + thisone.Color + ", " + thisone.Price + ".\n");
            }

            /*********************
            *  Ayush Edit Ends   *
            *********************/
        }
    }
}
