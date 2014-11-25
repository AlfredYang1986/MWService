using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MWDispatchService;
using MWDataSerilizationType;
using System.Web.Script.Serialization;
using System.Xml;

namespace UnitTest_MWStylePalletShareRequestService
{
    class Program
    {
        static void Main(string[] args)
        {
            callSearchService();

            //string ayuid = callShareReqService("ayush", "susan");
            ////callShareReqService("ayush", "susan");
            ////callShareReqService("ayush", "susan");
            ////callShareReqService("ayush", "susan");

            //callAcceptReqService("susan",ayuid);

            ////callShareReqService("ayush", "susan");
            
            //string id = callShareReqService("viki", "someone");
            //callAcceptReqService("someone", id);

            ////id = callShareReqService();
            //callAcceptReqService("some", ayuid);

            //string data = callGetEnvService("susan");
            //Console.WriteLine(data + "\n");

            //Console.Read();
            //data = callGetEnvService("someone");
            //Console.WriteLine(data + "\n");

            Console.Read();

        }

        public static void callAcceptReqService(string user, string iden)
        {
            string url = String.Format("http://localhost:1247/Service.svc/request");

            WebClient serviceRequest = new WebClient();

            string token = "4u8Rsdcz0UjUHYzZjwCyBOmACZjs+EJ+";
            string myJSON = "";

            MWAcceptRequestJSON myRequest = new MWAcceptRequestJSON
            {
                UserId = user,
                ID = iden
            };

            myRequest.Items = new List<Item>();
            myRequest.Items.Add(new Item
            {
                ItemId = "item1",
                Xposition = "x",
                Yposition = "y",
                Top = "T",
                Left = "L",
                Width = "w",
                Height = "H"
            });
            string acceptrequest = string.Empty;

            try
            {
                AuthorizationCheck authRequest = new AuthorizationCheck();
                authRequest.Token = token;
                MWRequestPhraseJSON requestString = new MWRequestPhraseJSON();
                requestString.MessageName = "AcceptRequest";
                requestString.Parameters = new List<MWParameterJSON>();

                acceptrequest = new JavaScriptSerializer().Serialize(myRequest).ToString();
                requestString.Parameters.Add(new MWParameterJSON
                {
                    ParameterName = "acceptrequest",
                    ParameterType = "string",
                    ParameterValue = acceptrequest
                });
                authRequest.RequestString = requestString;

                myJSON = new JavaScriptSerializer().Serialize(authRequest).ToString();
                // Wrapped the json as it threw an error otherwise
                //Double checked the Interface of Dispatch Service..It was supposed to be wrapped
                myJSON = "{ \"Request\":" + myJSON + "}";


            }
            catch (Exception e)
            {
                Console.Write("");
            }
            serviceRequest.Headers[HttpRequestHeader.ContentType] = "application/json";
            string response = string.Empty;

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
        }


        public static string callShareReqService(string user, string friend)
        {
            string url = String.Format("http://localhost:1247/Service.svc/request");

            WebClient serviceRequest = new WebClient();

            string token = "4u8Rsdcz0UjUHYzZjwCyBOmACZjs+EJ+";
            string myJSON = "";

            MWShareRequestJSON myRequest = new MWShareRequestJSON
            {
                UserId = user,
                FriendId = friend
            };

            myRequest.Items = new List<Item>();
            myRequest.Items.Add(new Item
            {
                ItemId = "item1",
                Xposition = "x",
                Yposition = "y",
                Top = "T",
                Left = "L",
                Width = "w",
                Height = "H"
            });
            string sharerequest = string.Empty;

            try
            {
                AuthorizationCheck authRequest = new AuthorizationCheck();
                authRequest.Token = token;
                MWRequestPhraseJSON requestString = new MWRequestPhraseJSON();
                requestString.MessageName = "ShareRequest";
                requestString.Parameters = new List<MWParameterJSON>();

                sharerequest = new JavaScriptSerializer().Serialize(myRequest).ToString();
                requestString.Parameters.Add(new MWParameterJSON
                {
                    ParameterName = "sharerequest",
                    ParameterType = "string",
                    ParameterValue = sharerequest
                });
                authRequest.RequestString = requestString;

                myJSON = new JavaScriptSerializer().Serialize(authRequest).ToString();
                myJSON = "{ \"Request\":" + myJSON + "}";
                //myJSON = "{ \"Request\":"
                //                                    + " { \"Token\": \"" + token + "\","
                //                                    + " \"RequestString\": "
                //                                    + "  { \"MessageName\": \"ShareRequest\" "
                //                                    + "    \"Parameters\": \"[ "
                //                                    + "                         {\"ParameterName\": \"sharerequest\" "
                //                                    + "                          \"ParameterType\": \"string\" "
                //                                    + "                           \"ParameterValue\": \"" + sharerequest + "\"  ]\"  "
                //                                    + "  } "
                //                                    + " } "
                //                             + "}";
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
            var xDoc = new XmlDocument();
            /*
             * <requestResponse xmlns="http://tempuri.org/">
             *      <requestResult>
             *          079c0466-e8d9-6654-99fe-0872fd32d8f6
             *      </requestResult>
             * </requestResponse>
             */
            xDoc.LoadXml(response);
            //XmlNode result = xDoc.SelectSingleNode("requestResult");
            //find id
            string id = xDoc.FirstChild.InnerText;
            return id;

        }

        public static string callGetEnvService(string user)
        {
            string url = String.Format("http://localhost:1247/Service.svc/request");

            WebClient serviceRequest = new WebClient();

            string token = "4u8Rsdcz0UjUHYzZjwCyBOmACZjs+EJ+";
            string myJSON = "";

            try
            {
                AuthorizationCheck authRequest = new AuthorizationCheck();
                authRequest.Token = token;
                MWRequestPhraseJSON requestString = new MWRequestPhraseJSON();
                requestString.MessageName = "GetPreviousEnvironment";
                requestString.Parameters = new List<MWParameterJSON>();

                requestString.Parameters.Add(new MWParameterJSON
                {
                    ParameterName = "getenv",
                    ParameterType = "string",
                    ParameterValue = user
                });
                authRequest.RequestString = requestString;

                myJSON = new JavaScriptSerializer().Serialize(authRequest).ToString();
                myJSON = "{ \"Request\":" + myJSON + "}";
                //myJSON = "{ \"Request\":"
                //                                    + " { \"Token\": \"" + token + "\","
                //                                    + " \"RequestString\": "
                //                                    + "  { \"MessageName\": \"ShareRequest\" "
                //                                    + "    \"Parameters\": \"[ "
                //                                    + "                         {\"ParameterName\": \"sharerequest\" "
                //                                    + "                          \"ParameterType\": \"string\" "
                //                                    + "                           \"ParameterValue\": \"" + sharerequest + "\"  ]\"  "
                //                                    + "  } "
                //                                    + " } "
                //                             + "}";
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
            var xDoc = new XmlDocument();
            /*
             * <requestResponse xmlns="http://tempuri.org/">
             *      <requestResult>
             *          079c0466-e8d9-6654-99fe-0872fd32d8f6
             *      </requestResult>
             * </requestResponse>
             */
            xDoc.LoadXml(response);
            //XmlNode result = xDoc.SelectSingleNode("requestResult");
            //find id
            string id = xDoc.FirstChild.InnerText;
            return id;

        }

        public static string callSearchService()
        {
            string url = String.Format("http://localhost:1247/Service.svc/request");

            WebClient serviceRequest = new WebClient();

            string token = "4u8Rsdcz0UjUHYzZjwCyBOmACZjs+EJ+";
            string myJSON = "";

            try
            {
                AuthorizationCheck authRequest = new AuthorizationCheck();
                authRequest.Token = token;
                MWRequestPhraseJSON requestString = new MWRequestPhraseJSON();
                requestString.MessageName = "Searching";
                requestString.Parameters = new List<MWParameterJSON>();
                MWSearchingwithPrice mySearchRequest = new MWSearchingwithPrice();
                mySearchRequest.price = new MWPrice
                {
                    maxPrice = 500,
                    minPrice = 10
                };
                mySearchRequest.Query = "";
                string reque = new JavaScriptSerializer().Serialize(mySearchRequest).ToString();
                requestString.Parameters.Add(new MWParameterJSON
                {
                    ParameterName = "input",
                    ParameterType = "string",
                    ParameterValue = reque
                });
                authRequest.RequestString = requestString;

                myJSON = new JavaScriptSerializer().Serialize(authRequest).ToString();
                myJSON = "{ \"Request\":" + myJSON + "}";
                //myJSON = "{ \"Request\":"
                //                                    + " { \"Token\": \"" + token + "\","
                //                                    + " \"RequestString\": "
                //                                    + "  { \"MessageName\": \"ShareRequest\" "
                //                                    + "    \"Parameters\": \"[ "
                //                                    + "                         {\"ParameterName\": \"sharerequest\" "
                //                                    + "                          \"ParameterType\": \"string\" "
                //                                    + "                           \"ParameterValue\": \"" + sharerequest + "\"  ]\"  "
                //                                    + "  } "
                //                                    + " } "
                //                             + "}";
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
            var xDoc = new XmlDocument();
            /*
             * <requestResponse xmlns="http://tempuri.org/">
             *      <requestResult>
             *          079c0466-e8d9-6654-99fe-0872fd32d8f6
             *      </requestResult>
             * </requestResponse>
             */
            xDoc.LoadXml(response);
            //XmlNode result = xDoc.SelectSingleNode("requestResult");
            //find id
            string id = xDoc.FirstChild.InnerText;
            return id;

        }

    }
}
