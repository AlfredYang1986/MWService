using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Web;
using MWRemoteAPICall;

namespace MWDispatchService
{
    class MWDispatchHelper
    {
        public void  SetUserId(AuthorizationCheck Request)
        {
                    
                string userId = GetUserId(Request.Token);
                if (userId != null)
                {

                    MWDataSerilizationType.MWTag_WardrobeeServiceJson parameter = new MWDataSerilizationType.MWTag_WardrobeeServiceJson();
                    parameter.userID = userId;
                    parameter.tagInfo = Request.RequestString.Parameters;
                    
                    Request.RequestString.Parameters = new JavaScriptSerializer().Serialize(parameter).ToString(); ;
                }
                        
                else
                    Request.RequestString.Parameters = null;
        }
     
        public string GetUserId(string token)
        {
            using (var remotCall = MWRemoteAPIFactory.Instance())
            {
                dynamic request = new UrlPointParameters();
                request.token = HttpUtility.UrlEncode(token);
                return remotCall.invokes("oauth", "GetUserId", request as EndPointParameters, GetResult) as string;
            }
        }
        public string GetResult(string result)
        {
            return result;
        }
        
        
    }
}
 