using MWDataSerilizationType;
using System;
using System.Collections.Generic;

namespace MWMyWardrobeService
{
    public interface IMWMyWardrobeService : IDisposable
    {
        bool AddItem(string UserID, MyWardrobeData inputs);
        bool AddOutfit(string UserID, MyWardrobeData inputs);
        List<OutfitItem> ListOutfitItems(MyWardrobeData inputs);
        bool DelItem(string UserID, MyWardrobeData inputs);
        bool DelOutfit(string UserID, MyWardrobeData inputs);
        bool EditOutfit(string UserID, MyWardrobeData inputs);
        bool DelItemsSave(string UserID, MyWardrobeData inputs);
        bool MoveItemsTo(string UserID, MyWardrobeData inputs);
        List<ItemInfo> ListAllUserItems(string UserID, MyWardrobeData inputs);
        List<OutfitInfo> ListAllUserOutfits(string UserID, MyWardrobeData inputs);
        /*
         * Ayush updated code below
         */
        Boolean CreateUser(UserInfo usrInfo);
        //bool CreateUser(MWDataSerilizationType.UserInfo inputs);
        UserInfo GetUserInfo(string userId);

        Boolean UpdateUserInfo(UserInfo userInfo);
    }

    public class MyWardrobeFactory
    {
        static IMWMyWardrobeService _impl = new MWMyWardrobeService();

        public static IMWMyWardrobeService NewInstance()
        {
            return _impl;
        }
    }
}
