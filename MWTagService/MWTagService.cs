using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWDataSerilizationType;
using MWDataEntity;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace MWTagService
{
    public class MWTagService : IMWTagService
    {
        public void Dispose()
        {

        }

        //Adding tag to a specific object, if the tag is not exist, create it first
        public Boolean AddTag(string UserID,TagServiceData inputs)
        {
            /**
             * 1. create tag--add tag in to MW_TAG 
             * 2. connect tage with user  MW_USER_TAG  user_id & tag_id
             * 3. return true if success
             */
            using (var mwdb = new MWDBEntities())
            {
                
                Boolean reVal = false;
                //if (!IsTagExisting(inputs.tagId)) 
                if (inputs.tagName != null && inputs.tagType != null && inputs.picId != 0 && UserID != null && inputs.itemId != 0)
                {
                    var new_tag = new MW_TAG() { tag_name = inputs.tagName,
                                                 tag_type = inputs.tagType, 
                                                 default_picture = inputs.picId 
                                                };
                    mwdb.MW_TAG.Add(new_tag);
                 
                    var bind_user = (from u in mwdb.MW_USER
                                     where u.user_id == UserID
                                     select u).FirstOrDefault();

                    //var serch_item = (from s in mwdb.MW_SEARCHABLE_ITEM
                    //                  where s.searchable_item_id == inputs.itemId
                    //                  select s).FirstOrDefault();

                    if (bind_user != null )//&& serch_item != null)
                    {
                        var user_tag = new MW_USER_TAG() {
                                            MW_TAG = new_tag, 
                                            MW_USER = bind_user,
                                            create_time = DateTime.Now};
                       
                        //var uti = new MW_USER_TAG_ITEM(){
                        //                                    MW_USER_TAG = user_tag,
                        //                                    MW_SEARCHABLE_ITEM = serch_item,
                        //                                   tag_time = DateTime.Now
                        //                                };
                        //(System.DateTime?)DateTime.Now};//new DataColumn(DateTime.Now.ToString(), typeof(DateTime))

                        mwdb.MW_USER_TAG.Add(user_tag);
                        //mwdb.MW_USER_TAG_ITEM.Add(uti);
                        mwdb.SaveChanges();
                        reVal = true;
                    }
                }
                return reVal;
            }
        }

        public Boolean AddTagToItem(string UserID,TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var uti = new MW_USER_TAG_ITEM() 
                { 
                    searchable_item_id = inputs.itemId,
                    user_id = UserID,
                    tag_id = inputs.tagId,
                    tag_time = DateTime.Now
                };
                mwdb.MW_USER_TAG_ITEM.Add(uti);
                mwdb.SaveChanges();
                return true;
            }
        }

        //Delete a tag in database
        public List<Tags> DelTag(string UserID, TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                if (inputs.tagId == 0)
                    return null;
                else
                {
                    var user_Tag = (from user_tag_set in mwdb.MW_USER_TAG
                                    where user_tag_set.tag_id == inputs.tagId
                                    select user_tag_set).FirstOrDefault();

                    //var itemTagTemp = from _itemTagTemp in mwdb.MW_USER_TAG_ITEM where _itemTagTemp.tag_id == inputs.tagId select _itemTagTemp;
                    //foreach (var ItemTag in itemTagTemp)
                    //{
                    //    mwdb.MW_USER_TAG_ITEM.Remove(ItemTag);
                    //}
                    //var OutfitTagTemp = from _OutfitTagTemp in mwdb.MW_USER_TAG_OUTFIT where _OutfitTagTemp.tag_id == inputs.tagId select _OutfitTagTemp;
                    //foreach (var OutfitTag in OutfitTagTemp)
                    //{
                    //    mwdb.MW_USER_TAG_OUTFIT.Remove(OutfitTag);
                    //}
                    //var tagTemp = from _tagTemp in mwdb.MW_TAG where _tagTemp.tag_id == inputs.tagId select _tagTemp;
                    //foreach (var tag in tagTemp)
                    //{
                    //    mwdb.MW_TAG.Remove(tag);
                    //}
                    var user_tag_items = from u_item in mwdb.MW_USER_TAG_ITEM
                                         where u_item.user_id == UserID &&
                                               u_item.tag_id == inputs.tagId
                                         select u_item;
                    if (user_tag_items != null)
                    {
                        foreach (var user_item in user_tag_items)
                        {
                            mwdb.MW_USER_TAG_ITEM.Remove(user_item);
                        }
                    }
                    mwdb.MW_USER_TAG.Remove(user_Tag);
                    mwdb.SaveChanges();
                    return ListTags(UserID, inputs);

                }
            }

        }
        public Boolean DelObjTag(string UserID,TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                if (inputs.itemId != 0)
                {
                    var itemTagTemp = (from _itemTagTemp in mwdb.MW_USER_TAG_ITEM
                                      where _itemTagTemp.tag_id == inputs.tagId &&
                                            _itemTagTemp.user_id == UserID && 
                                            _itemTagTemp.searchable_item_id == inputs.itemId
                                      select _itemTagTemp).FirstOrDefault();
                   // foreach (var obj in itemTagTemp)
                    //{
                    mwdb.MW_USER_TAG_ITEM.Remove(itemTagTemp);
                    //}
                    mwdb.SaveChanges();
                    return true;
                }
                else if (inputs.outfitId != 0)
                {
                    var outfitTagTemp = from _OutfitTagTemp in mwdb.MW_USER_TAG_OUTFIT
                                        where _OutfitTagTemp.tag_id == inputs.preTagId && _OutfitTagTemp.user_id == UserID && _OutfitTagTemp.outfit_id == inputs.outfitId
                                        select _OutfitTagTemp;
                    foreach (var obj in outfitTagTemp)
                    {
                        mwdb.MW_USER_TAG_OUTFIT.Remove(obj);
                    }
                    mwdb.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public Boolean EditTag(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                if (inputs.tagName == null)
                    return false;
                else
                {

                    var query = (from tag in mwdb.MW_TAG
                                 where tag.tag_id == inputs.tagId
                                 select tag).FirstOrDefault();

                    query.tag_name = inputs.tagName;
                    mwdb.SaveChanges();

                    return true;
                }
            }
        }

        public Boolean SaveTags(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {

                foreach (var sTag in inputs.sTags)
                {
                    var query = (from tag in mwdb.MW_TAG
                                 where tag.tag_id == sTag.tagId
                                 select tag).FirstOrDefault();

                    query.tag_name = sTag.tagName;
                    mwdb.SaveChanges();
                }
                return true;
            }
        }

        public Boolean SaveTagsDel(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                foreach (var sTag in inputs.sTags)
                {
                    var itemTag = (from _itemTagTemp in mwdb.MW_USER_TAG_ITEM where _itemTagTemp.tag_id == sTag.tagId select _itemTagTemp).FirstOrDefault();
                    if (itemTag != null)
                    {
                        mwdb.MW_USER_TAG_ITEM.Remove(itemTag);
                    }
                    var OutfitTag = (from _OutfitTagTemp in mwdb.MW_USER_TAG_OUTFIT where _OutfitTagTemp.tag_id == sTag.tagId select _OutfitTagTemp).FirstOrDefault();
                    if (OutfitTag != null)
                    {
                        mwdb.MW_USER_TAG_OUTFIT.Remove(OutfitTag);
                    }
                    var tag = (from _tagTemp in mwdb.MW_TAG where _tagTemp.tag_id == sTag.tagId select _tagTemp).FirstOrDefault();
                    if (tag != null)
                        mwdb.MW_TAG.Remove(tag);
                    else
                        return false;
                    mwdb.SaveChanges();
                }
                return true;
            }
        }

        public Boolean EditObjTag(string UserID,TagServiceData inputs)
        {
            if (inputs.tagId == 0)
                return false;
            else
            {
                if (DelObjTag(UserID, inputs) && AddTagToItem(UserID, inputs))
                    return true;
                else
                    return false;
            }
        }
       
        public List<Tags> ListTags(string userID, TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                if (userID == null)
                    return null;
                List<Tags> returnTags = new List<Tags>();

                var tagsInfo = from tag in mwdb.MW_TAG
                               join p in mwdb.MW_PICTURE
                               on tag.default_picture equals p.picture_id
                               join user_tag in mwdb.MW_USER_TAG
                               on tag.tag_id equals user_tag.tag_id
                               where user_tag.user_id == userID
                               select new { tagInfo = tag, pic_url = p.url };

                foreach (var tag in tagsInfo.Distinct())
                {
                    Tags signleTag = new Tags(){ tagId = tag.tagInfo.tag_id,
                                                 tagName = tag.tagInfo.tag_name,
                                                 tagType = tag.tagInfo.tag_type,
                                                 tagPicUrl = tag.pic_url
                    };
                        
                    returnTags.Add(signleTag);
                }
                return returnTags;
            }
        }   

        public int GetTagNumber(string UserID,TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                List<Tags> returnTags = new List<Tags>();
                IQueryable<int> tempItemTagIds = mwdb.MW_USER_TAG_ITEM.Where(i => i.user_id == UserID).Select(i => i.tag_id);

                foreach (var itemTagId in tempItemTagIds.Distinct())
                {
                    var tempTag = from _tag in mwdb.MW_TAG where _tag.tag_id == itemTagId select _tag;
                    var picUrl = from url in mwdb.MW_PICTURE where url.picture_id == tempTag.FirstOrDefault().default_picture select url.url;
                    Tags itemTag = new Tags() { tagId = tempTag.FirstOrDefault().tag_id, tagName = tempTag.FirstOrDefault().tag_name, tagType = tempTag.FirstOrDefault().tag_type, tagPicUrl = picUrl.FirstOrDefault() };
                    returnTags.Add(itemTag);
                }

                IQueryable<int> tempOutfitTagIds = mwdb.MW_USER_TAG_OUTFIT.Where(i => i.user_id == UserID).Select(i => i.tag_id);
                foreach (var outfitTagId in tempOutfitTagIds.Distinct())
                {
                    var tempTag = from _tag in mwdb.MW_TAG where _tag.tag_id == outfitTagId select _tag;
                    var picUrl = from url in mwdb.MW_PICTURE where url.picture_id == tempTag.FirstOrDefault().default_picture select url.url;
                    Tags itemTag = new Tags() { tagId = tempTag.FirstOrDefault().tag_id, tagName = tempTag.FirstOrDefault().tag_name, tagType = tempTag.FirstOrDefault().tag_type, tagPicUrl = picUrl.FirstOrDefault() };
                    returnTags.Add(itemTag);
                }

                return returnTags.Count();
            }
        }
        public List<ItemInfo> ListItemByTag(string UserID, TagServiceData inputs)
        {
            if (inputs.tagName != null)
                return null;

            using (var mwdb = new MWDBEntities())
            {

                var itemResults = from search_item in mwdb.MW_SEARCHABLE_ITEM
                            where search_item.MW_USER_TAG_ITEM
                            .Any(x => x.user_id == UserID &&
                                      x.tag_id == inputs.tagId)
                            select search_item;


                List<ItemInfo> items = new List<ItemInfo>();
                foreach (var seaItem in itemResults)
                {               
                    ItemInfo tempItem = new ItemInfo()
                    {
                        seaItemId = seaItem.searchable_item_id,
                        brand = seaItem.MW_ABSTRACT_ITEM.MW_BRAND.brand_name,
                        category = seaItem.MW_ABSTRACT_ITEM.MW_CATEGORY.category_name,
                        fromUrl = seaItem.from_url,
                        addedTime = seaItem.MW_USER_TAG_ITEM
                                    .Where(x => x.searchable_item_id == seaItem.searchable_item_id)
                                    .FirstOrDefault().tag_time.ToString(),
                        price = seaItem.price,
                        title = seaItem.title,
                        itemPicUrl = seaItem.MW_PICTURE.url,
                        LikeCount = seaItem.MW_USER.Count,
                    };
                    items.Add(tempItem);
                }
                return items;
            }
        }

        public List<OutfitInfo> ListOutfitByTag(string UserID, TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                IQueryable<int> outfitRes = mwdb.MW_USER_TAG_OUTFIT.Where(i => i.tag_id == inputs.tagId && i.user_id == UserID).Select(i => i.outfit_id);

                List<OutfitInfo> result = new List<OutfitInfo>();
                foreach (var _outfitId in outfitRes)
                {
                    var outfitTime = from _outfitTime in mwdb.MW_USER_TAG_OUTFIT where _outfitTime.outfit_id == _outfitId select _outfitTime.tag_time;
                    var outfit = from _outfit in mwdb.MW_OUTFIT where _outfit.outfit_id == _outfitId select _outfit;
                    OutfitInfo tmpOutfit = new OutfitInfo() { outfitId = outfit.FirstOrDefault().outfit_id, outfitName = outfit.FirstOrDefault().name, outfitTime = outfitTime.FirstOrDefault().ToString() };
                    result.Add(tmpOutfit);
                }
                return result;
            }
        }

        public int ImportedCount(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var items = from _items in mwdb.MW_USER_TAG_ITEM
                            where _items.searchable_item_id == inputs.itemId 
                            select _items;
                return items.Count();
            }
        }

        public int LikeAnItem(string UserID, TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var usr = (from _usr in mwdb.MW_USER where _usr.user_id == UserID select _usr).FirstOrDefault();
                var item = (from _item in mwdb.MW_SEARCHABLE_ITEM where _item.searchable_item_id == inputs.itemId select _item).FirstOrDefault();
                usr.MW_SEARCHABLE_ITEM.Add(item);
                mwdb.MW_USER.Attach(usr);
                mwdb.Entry(usr).State = EntityState.Modified;
                mwdb.SaveChanges();
                return item.MW_USER.Count();
            }
        }
        public int LikedCount(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var item = (from _item in mwdb.MW_SEARCHABLE_ITEM 
                            where _item.searchable_item_id == inputs.itemId 
                            select _item).FirstOrDefault();
                return item.MW_USER.Count();
            }
        }

        public int DislikeAnItem(string UserID, TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var usr = (from _usr in mwdb.MW_USER where _usr.user_id == UserID select _usr).FirstOrDefault();
                var item = (from _item in mwdb.MW_SEARCHABLE_ITEM where _item.searchable_item_id == inputs.itemId select _item).FirstOrDefault();
                usr.MW_SEARCHABLE_ITEM.Add(item);
                mwdb.MW_USER.Attach(usr);
                mwdb.Entry(usr).State = EntityState.Modified;
                mwdb.SaveChanges();
                return item.MW_USER.Count();
            }
        }
        public int DislikedCount(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var item = (from _item in mwdb.MW_SEARCHABLE_ITEM 
                            where _item.searchable_item_id == inputs.itemId 
                            select _item).FirstOrDefault();
                return item.MW_USER.Count();
            }
        }

        public Boolean IsTagExisting(string tagName)
        {
            using (var mwdb = new MWDBEntities())
            {
                var tags = mwdb.MW_TAG.Select(t => t.tag_name);
                foreach (string _tag in tags)
                {
                    if (tagName == _tag)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public Boolean SetCover(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {
                var picId = (from pic in mwdb.MW_PICTURE where pic.url == inputs.coverUrl select pic.picture_id).FirstOrDefault();
                var query = (from tag in mwdb.MW_TAG where tag.tag_id == inputs.tagId select tag).FirstOrDefault();
                query.default_picture = picId;
                mwdb.SaveChanges();
                return true;
            }
        }

        public ItemInfo DefaultTagItem(TagServiceData inputs)
        {
            using (var mwdb = new MWDBEntities())
            {

                var picId = (from pic in mwdb.MW_PICTURE where pic.url == inputs.coverUrl select pic.picture_id).FirstOrDefault();
                var seaItem = (from _item in mwdb.MW_SEARCHABLE_ITEM where _item.default_pic == picId select _item).FirstOrDefault();
                ItemInfo item = new ItemInfo()
                {
                    seaItemId = seaItem.searchable_item_id,
                    brand = (from _brand in mwdb.MW_BRAND
                             where _brand.brand_id ==
                             ((from _absItem in mwdb.MW_ABSTRACT_ITEM
                               where _absItem.abstract_item_id == seaItem.abstract_item_id
                               select _absItem.brand_id).FirstOrDefault())
                             select _brand.brand_name).FirstOrDefault(),
                    category = (from _category in mwdb.MW_CATEGORY
                                where _category.category_id ==
                                ((from _absItem in mwdb.MW_ABSTRACT_ITEM
                                  where _absItem.abstract_item_id == seaItem.abstract_item_id
                                  select _absItem.category_id).FirstOrDefault())
                                select _category.category_name).FirstOrDefault(),
                    fromUrl = seaItem.from_url,
                    addedTime = (from _tag in mwdb.MW_USER_TAG_ITEM where _tag.searchable_item_id == seaItem.searchable_item_id select _tag.tag_time).FirstOrDefault().ToString(),
                    price = seaItem.price,
                    title = seaItem.title,
                    itemPicUrl = (from _pic in mwdb.MW_PICTURE where _pic.picture_id == seaItem.default_pic select _pic.url).FirstOrDefault(),
                    LikeCount = seaItem.MW_USER.Count(),
                    DislikeCount = seaItem.MW_USER.Count()
                };
                return item;
            }
        }
    }
}
