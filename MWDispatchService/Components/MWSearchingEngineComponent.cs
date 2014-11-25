using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using MWDataSerilizationType;
using MWSearchingEngine;
using System.Runtime.Serialization.Json;

namespace MWDispatchService
{
    class MWSearchingEngineComponent : MWRemoteComponent
    {
        private string input;

        public string Input { get { return input; } set { input = value; } }

        public T jsonDes<T>(string data)
        {
            using (var reader = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(reader);
            }

        }
        public string Searching(string args)
        {
            using (var client = MWSearchingEngineFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                SearchingParams paramterInfo = jsonDes<SearchingParams>(deserialised.tagInfo);
                //Apparel[] reVal = client.SearchingWithUserInput(deserialised).Results.ToArray();
                Apparel[] reVal = client.SearchingWithParams(paramterInfo).Results.ToArray();
                return MWAbstractComponent.Serialize<Apparel[]>(reVal);
            }
        }


        public string UpdateLikeCount(string args)
        {
            using (var client = MWSearchingEngineFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                UpdateLikeCountParams paramterInfo = jsonDes<UpdateLikeCountParams>(deserialised.tagInfo);
                string likeCount = client.UpdateLikeCount(deserialised.userID, paramterInfo).ToString();
                return MWAbstractComponent.Serialize<string>(likeCount);
            }
        }

        public string GetInitialMenuItem(string args)//GetBrand()
        {
            //if you need brands
            MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
            string paramterInfo = deserialised.tagInfo;

            if (paramterInfo.ToLower() == "brand")
            {
                using (var client = MWSearchingEngineFactory.NewInstance())
                {
                    string[] reVal = client.GetBrand().ToArray();
                    return MWAbstractComponent.Serialize<string[]>(reVal);
                }
            }

            else if (paramterInfo.ToLower() == "category")
            {
                using (var client = MWSearchingEngineFactory.NewInstance())
                {
                    string[] reVal = client.GetCategory().ToArray();
                    return MWAbstractComponent.Serialize<string[]>(reVal);
                }
            }

            else if (paramterInfo.ToLower().Contains("subcategory"))
            { 
                using (var client = MWSearchingEngineFactory.NewInstance())
                {
                    string[] reVal = client.GetsubCategory(paramterInfo).ToArray();
                    return MWAbstractComponent.Serialize<string[]>(reVal);
                }
            }
            else if (paramterInfo.ToLower() == "scence")
            {
                using (var clien = MWSearchingEngineFactory.NewInstance())
                {
                    string[] reVal = clien.Getscences().ToArray();
                    return MWAbstractComponent.Serialize<string[]>(reVal);
                }
            }
            return null;
        }

        public string AutoCompletion(string args)
        {
            using (var client = MWSearchingEngineFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                string paramterInfo = deserialised.tagInfo;
                string[] reVal = client.AutoCompletion(paramterInfo).ToArray();
                return MWAbstractComponent.Serialize<string[]>(reVal);
            }
        }
    }
}
