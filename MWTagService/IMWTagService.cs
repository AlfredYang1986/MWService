using System;
using System.Collections.Generic;
using MWDataSerilizationType;
namespace MWTagService
{
    public interface IMWTagService : IDisposable
    {
        bool AddTag(string UserID, TagServiceData inputs);
        bool DelObjTag(string UserID, TagServiceData inputs);
        List<Tags> DelTag(string UserID, TagServiceData inputs);
        bool EditObjTag(string UserID, TagServiceData inputs);
        bool EditTag(TagServiceData inputs);
        bool SaveTags(TagServiceData inputs);
        bool SaveTagsDel(TagServiceData inputs);
        int ImportedCount(TagServiceData inputs);
        int LikedCount(TagServiceData inputs);
        int DislikedCount(TagServiceData inputs);
        int LikeAnItem(string UserID, TagServiceData inputs);
        int DislikeAnItem(string UserID, TagServiceData inputs);
        bool IsTagExisting(string tagName);
        List<Tags> ListTags(string userID,TagServiceData inputs);
        //List<Tags> ListAllTags(string UserID, TagServiceData inputs);
        int GetTagNumber(string UserID, TagServiceData inputs);
        List<ItemInfo> ListItemByTag(string UserID, TagServiceData inputs);
        List<OutfitInfo> ListOutfitByTag(string UserID, TagServiceData inputs);
        bool SetCover(TagServiceData inputs);
        ItemInfo DefaultTagItem(TagServiceData inputs);
    }

    public class TaggingFactory
    {
        static IMWTagService _impl = new MWTagService();

        public static IMWTagService NewInstance()
        {
            return _impl;
        }
    }
}
