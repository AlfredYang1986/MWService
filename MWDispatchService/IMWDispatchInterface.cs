using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MWDataSerilizationType;

namespace MWDispatchService
{
    [ServiceContract]
    public interface IMWDispatchInterface
    {
        [OperationContract]
        [FaultContract(typeof(string))]
        [WebInvoke(Method = "POST"
            , UriTemplate = "request"
            , ResponseFormat = WebMessageFormat.Xml
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.Wrapped)]
        string request(AuthorizationCheck Request);

        [OperationContract]
        [WebGet(UriTemplate = "File/{fileName}/{fileExtension}")]
        Stream DownloadFile(string fileName, string fileExtension);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UploadFile?fileName={fileName}")]
        Boolean UploadCustomFile(string fileName, Stream stream);

        [OperationContract]
        [WebGet(UriTemplate = "SearchDemo?input={input}"
            , ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Apparel> AppSearchDemo(string input);

        [OperationContract]
        [WebInvoke(Method="POST"
            , UriTemplate = "AppSearchDemoWithAVAudio?fileName={fileName}"
            , ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Apparel> AppSearchDemoWithAVAudio(string fileName, Stream stream);
    }
}