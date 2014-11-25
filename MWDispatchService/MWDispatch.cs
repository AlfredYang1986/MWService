using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using MWRemoteAPICall;
using System.IO;
using System.ServiceModel.Web;
using System.Web.Hosting;
//using MWiLBCConvert;
using MWDataSerilizationType;
using MWSearchingEngine;
using MWDispatchService.SpeechObject;

namespace MWDispatchService
{
    public class MWDispatch : IMWDispatchInterface
    {
        public string request(AuthorizationCheck Request)
        {
            string reVal = null;
            if (Request.RequestString.MessageName != "CreateUser")
            {
                MWDispatchHelper dispatchHelper = new MWDispatchHelper();
                dispatchHelper.SetUserId(Request);
            }

            if(Request.RequestString.Parameters != null)
            { 
                MWRequestPhrase rp = new MWRequestPhrase();
                MWRequestStruct rs = new MWRequestStruct();
                if (rp.phraseMessageName(Request.RequestString, ref rs)
                    && rp.phraseMessageServer(ref rs)
                    && rp.phraseMessageArgs(Request.RequestString, ref rs))
                    reVal = rs.invokes();

                return reVal;
            }
            else
            {
                return null;
            }
        }

        public Stream DownloadFile(string fileName, string fileExtension)
        {
            string downloadFilePath = Path.Combine(HostingEnvironment.MapPath("~/Downloads"), fileName + "." + fileExtension);

            String headerInfo = "attachment; filename=" + fileName + "." + fileExtension;
            WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";

            System.Console.WriteLine(File.OpenRead(downloadFilePath));
            return File.OpenRead(downloadFilePath);
        }

        public Boolean UploadCustomFile(string fileName, Stream stream)
        {
            string FilePath = Path.Combine(HostingEnvironment.MapPath("~/Uploads"), fileName);

            int length = 0;
        
            using (FileStream writer = new FileStream(FilePath, FileMode.Create))
            {
                int readCount;
                var buffer = new byte[8192];
                while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    writer.Write(buffer, 0, readCount);
                    length += readCount;
                }
            }
            return true;
        }

        public IEnumerable<Apparel> AppSearchDemo(string input)
        {
            using (var s = MWSearchingEngineFactory.NewInstance())
            {
                return s.AppSearchingDemo(input);
            }
        }

        public IEnumerable<Apparel> AppSearchDemoWithAVAudio(string fileName, Stream stream)
        {
            string FilePath = Path.Combine(HostingEnvironment.MapPath("~/Uploads"), fileName);

            //if (UploadCustomFile(fileName, stream))
            {
                if (fileName.EndsWith(@".wav"))
                {
                    IEnumerable<string> reVal = new SpeechObject.SpRecognition().RecognizeFromWAVStream(stream);
                    using (var se = MWSearchingEngineFactory.NewInstance())
                    {
                        return se.AppSearchingDemo_2(reVal);
                    }
                }
            }
            return null;
        }
    }
}