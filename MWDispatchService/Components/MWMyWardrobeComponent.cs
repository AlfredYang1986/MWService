using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using MWDataSerilizationType;
using MWMyWardrobeService;
using System.Web.Script.Serialization;

namespace MWDispatchService
{
    class MWMyWardrobeComponent : MWRemoteComponent
    {
        public T jsonDes<T>(string data)
        {
            using (var reader = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(reader);
            }

        }

        public string AddItem(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.AddItem(deserialised.userID,wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string ListOutfitItems(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                List<OutfitItem> reVal = client.ListOutfitItems(wardrobeInfo);
                return MWAbstractComponent.Serialize<List<OutfitItem>>(reVal);
            }
        }

        public string AddOutfit(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.AddOutfit(deserialised.userID,wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }
        public string DelItem(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.DelItem(deserialised.userID,wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }
        public string DelOutfit(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {

                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.DelOutfit(deserialised.userID,wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }
        public string EditOutfit(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.EditOutfit(deserialised.userID, wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string DelItemsSave(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.DelItemsSave(deserialised.userID, wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string MoveItemsTo(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                bool reVal = client.MoveItemsTo(deserialised.userID, wardrobeInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string CreateUser(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                //MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                UserInfo userInfo = jsonDes<UserInfo>(args);
                bool reVal = client.CreateUser(userInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string GetUserInfo(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                //UserInfo deserialised = jsonDes<UserInfo>(args["inputs"] as string);
                UserInfo reVal = client.GetUserInfo(deserialised.userID);
                return MWAbstractComponent.Serialize<UserInfo>(reVal);
            }
        }

        public string UpdateUser(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {

                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                UserInfo userInfo = jsonDes<UserInfo>(deserialised.tagInfo);
                bool reVal = client.UpdateUserInfo(userInfo);
                return MWAbstractComponent.Serialize<Boolean>(reVal);
            }
        }

        public string ListAllUserItems(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                List<ItemInfo> reVal = client.ListAllUserItems(deserialised.userID, wardrobeInfo);
                return MWAbstractComponent.Serialize<List<ItemInfo>>(reVal);
            }
        }

        public string ListAllUserOutfits(string args)
        {
            using (var client = MyWardrobeFactory.NewInstance())
            {
                MWTag_WardrobeeServiceJson deserialised = jsonDes<MWTag_WardrobeeServiceJson>(args);
                MyWardrobeData wardrobeInfo = jsonDes<MyWardrobeData>(deserialised.tagInfo);
                List<OutfitInfo> reVal = client.ListAllUserOutfits(deserialised.userID,wardrobeInfo);
                return MWAbstractComponent.Serialize<List<OutfitInfo>>(reVal);
            }
        }
    }
}
