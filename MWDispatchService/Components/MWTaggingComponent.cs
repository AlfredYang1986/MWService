using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWTagService;
using MWDataSerilizationType;
using System.Xml;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MWDispatchService
{
    class MWTaggingComponent : MWRemoteComponent
    {
        public T jsonDes<T>(string data)
        {
            using (var reader = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(reader);
            }

        }
        public string AddTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo); 
                bool reVal = client.AddTag(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);             
            }                
        }

        public string DelObjTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                bool reVal = client.DelObjTag(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string DelTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                List<Tags> reVal = client.DelTag(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<List<Tags>>(reVal);
            }
        }

        public string EditObjTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                bool reVal = client.EditObjTag(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string EditTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                bool reVal = client.EditTag(tagInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string SaveTags(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                bool reVal = client.SaveTags(tagInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string SaveTagsDel(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                bool reVal = client.SaveTagsDel(tagInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string ListItemByTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                List<ItemInfo> reVal = client.ListItemByTag(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<List<ItemInfo>>(reVal);
            }
        }

        public string ListOutfitByTag(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                List<OutfitInfo> reVal = client.ListOutfitByTag(deserialised.userID,tagInfo);
                return MWAbstractComponent.Serialize<List<OutfitInfo>>(reVal);
            }
        }

        public string ListTags(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                //TagServiceData deserialised = jsonDes<TagServiceData>(deserialised1);
                List<Tags> reVal = client.ListTags(deserialised.userID,tagInfo);
                return MWAbstractComponent.Serialize<List<Tags>>(reVal);
            }
        }

        //public string ListAllTags(string args)
        //{
        //    using (var client = TaggingFactory.NewInstance())
        //    {
        //        MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
        //        TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo); 
        //        List<Tags> reVal = client.ListAllTags(deserialised.userID, tagInfo);
        //        return MWAbstractComponent.Serialize<List<Tags>>(reVal);
        //    }
        //}

        public string GetTagNumber(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo); 
                int reVal = client.GetTagNumber(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<int>(reVal);
            }
        }

        public string ImportedCount(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                int reVal = client.ImportedCount(tagInfo);
                return MWAbstractComponent.Serialize<int>(reVal);
            }
        }

        public string LikedCount(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                int reVal = client.LikedCount(tagInfo);
                return MWAbstractComponent.Serialize<int>(reVal);
            }
        }

        public string DislikedCount(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                int reVal = client.DislikedCount(tagInfo);
                return MWAbstractComponent.Serialize<int>(reVal);
            }
        }

        public string LikeAnItem(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo); 
                int reVal = client.LikeAnItem(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<int>(reVal);
            }
        }

        public string DislikeAnItem(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo); 
                int reVal = client.DislikeAnItem(deserialised.userID, tagInfo);
                return MWAbstractComponent.Serialize<int>(reVal);
            }
        }

        public string SetCover(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                bool reVal = client.SetCover(tagInfo);
                return MWAbstractComponent.Serialize<bool>(reVal);
            }
        }

        public string DefaultTagItem(string args)
        {
            using (var client = TaggingFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                TagServiceData tagInfo = jsonDes<TagServiceData>(deserialised.tagInfo);
                ItemInfo reVal = client.DefaultTagItem(tagInfo);
                return MWAbstractComponent.Serialize<ItemInfo>(reVal);
            }
        }
    }
}
