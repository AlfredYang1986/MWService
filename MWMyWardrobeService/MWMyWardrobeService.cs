using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWDataSerilizationType;
using MWDataEntity;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MWTagService;

namespace MWMyWardrobeService
{
    public class MWMyWardrobeService : IMWMyWardrobeService
    {
        public void Dispose()
        {
        }

        MWDBEntities mwdb = new MWDBEntities();
        public Boolean AddOutfit(string UserID, MyWardrobeData inputs)
        {
            if (inputs.outfitName == null)
                return false;
            if ((from _outfit in mwdb.MW_OUTFIT where _outfit.name == inputs.outfitName select _outfit).FirstOrDefault() == null)
            {
                var outfit = new MW_OUTFIT() { name = inputs.outfitName };
                mwdb.MW_OUTFIT.Add(outfit);
                mwdb.SaveChanges();
            }

            int outfitId = (from _outfit in mwdb.MW_OUTFIT where _outfit.name == inputs.outfitName select _outfit.outfit_id).FirstOrDefault();

            foreach (var i in inputs.outfitItems)
            {
                var outfitItem = new MW_OUTFIT_ITEM()
                {
                    outfit_id = outfitId,
                    searchable_item_id = i.seaId,
                    //picture_id = (from _pic in mwdb.MW_PICTURE where _pic.url == i.picUrl select _pic.picture_id).FirstOrDefault(),
                    pic_x = i.picX,
                    pic_y = i.picY,
                    angle = i.angle,
                    crop_x = i.cropX,
                    crop_y = i.cropY,
                    crop_width = i.cropWidth,
                    crop_height = i.cropHeight,
                    sizing_factor = i.sizingFactor,
                    z_index = i.zIndex
                };
                mwdb.MW_OUTFIT_ITEM.Add(outfitItem);
            }
            var usrOutfit = new MW_USER_RATE_OUTFIT() { user_id = UserID, outfit_id = outfitId };
            mwdb.MW_USER_RATE_OUTFIT.Add(usrOutfit);

            mwdb.SaveChanges();
            return true;
        }

        public List<OutfitItem> ListOutfitItems(MyWardrobeData inputs)
        {
            using(var mwdb = new MWDBEntities())
            {
                var items = from _items in mwdb.MW_OUTFIT_ITEM where _items.outfit_id == inputs.outfitId select _items;
                List<OutfitItem> outfitItems = new List<OutfitItem>();
                foreach(var item in items)
                {
                    OutfitItem outfitItem = new OutfitItem();
                    outfitItem.outfitId = item.outfit_id;
                    outfitItem.seaId = item.searchable_item_id;
                    outfitItem.picUrl = (from _pic in mwdb.MW_PICTURE
                                         where _pic.picture_id == (from _picId in mwdb.MW_SEARCHABLE_ITEM
                                                                   where _picId.searchable_item_id == item.searchable_item_id
                                                                   select _picId.default_pic).FirstOrDefault()
                                         select _pic.url).FirstOrDefault();
                    outfitItem.picX = item.pic_x;
                    outfitItem.picY = item.pic_y;
                    outfitItem.angle = item.angle;
                    outfitItem.cropX = item.crop_x;
                    outfitItem.cropY = item.crop_y;
                    outfitItem.cropWidth = item.crop_width;
                    outfitItem.cropHeight = item.crop_height;
                    outfitItem.sizingFactor = item.sizing_factor;
                    outfitItem.zIndex = item.z_index;
                    outfitItems.Add(outfitItem);
                }
                return outfitItems;
            }
        }

        public Boolean DelOutfit(string UserID, MyWardrobeData inputs)
        {
            if (inputs.outfitId == 0)
                return false;
            var tmpUsrOutfit = from _tmpUsrOUtfit in mwdb.MW_USER_RATE_OUTFIT where _tmpUsrOUtfit.user_id == UserID && _tmpUsrOUtfit.outfit_id == inputs.outfitId select _tmpUsrOUtfit;
            var tmpOutfit = from _outfit in mwdb.MW_OUTFIT where _outfit.outfit_id == inputs.outfitId select _outfit;
            var tmpOutfitItems = from _outfitItems in mwdb.MW_OUTFIT_ITEM where _outfitItems.outfit_id == inputs.outfitId select _outfitItems;
            foreach (var usrOutfit in tmpUsrOutfit)
            {
                mwdb.MW_USER_RATE_OUTFIT.Remove(usrOutfit);
            }
            foreach (var outfit in tmpOutfit)
            {
                mwdb.MW_OUTFIT.Remove(outfit);
            }
            foreach(var outfitItem in tmpOutfitItems)
            {
                mwdb.MW_OUTFIT_ITEM.Remove(outfitItem);
            }
            mwdb.SaveChanges();
            return true;
        }

        public Boolean EditOutfit(string UserID,MyWardrobeData inputs)
        {
            if (inputs.outfitName == null)
                return false;
            if (DelOutfit(UserID, inputs) && AddOutfit(UserID, inputs))
                return true;
            return false;
        }

        public Boolean AddItem(string UserID, MyWardrobeData inputs)
        {
            if (UserID == null || inputs.itemId == 0)
                return false;

            using (var mwdb = new MWDBEntities())
            {
                var tag_items = (from item_T_U in mwdb.MW_USER_TAG_ITEM
                                 where item_T_U.user_id == UserID &&
                                       item_T_U.tag_id == inputs.tagId &&
                                       item_T_U.searchable_item_id == inputs.itemId
                                 select item_T_U).FirstOrDefault();
                if (tag_items == null)
                {
                    var item = new MW_USER_TAG_ITEM()
                    {
                        searchable_item_id = inputs.itemId,
                        user_id = UserID,
                        tag_id = inputs.tagId,
                        tag_time = DateTime.Now
                    };

                    mwdb.MW_USER_TAG_ITEM.Add(item);
                    mwdb.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public Boolean DelItem(string UserID, MyWardrobeData inputs)
        {
            if (UserID == null || inputs.itemId == 0)
                return false;
            //var tmpUsr = from _tmpUsr in mwdb.MW_USER where _tmpUsr.user_id == inputs.UserId select _tmpUsr;
            //var tmpItem = from _item in mwdb.MW_SEARCHABLE_ITEM where _item.searchable_item_id == inputs.ItemId select _item;
            //foreach (var usr in tmpUsr)
            //{
            //    foreach (var item in tmpItem)
            //    {
            //        usr.MW_SEARCHABLE_ITEM.Remove(item);
            //        mwdb.MW_USER.Attach(usr);
            //        mwdb.Entry(usr).State = EntityState.Modified;
            //        mwdb.SaveChanges();
            //    }
            //}
            var items = from _item in mwdb.MW_USER_TAG_ITEM where _item.searchable_item_id == inputs.itemId && _item.user_id == UserID select _item;
            foreach (var item in items)
            {
                mwdb.MW_USER_TAG_ITEM.Remove(item);
            }
            mwdb.SaveChanges();
            return true;
        }

        public Boolean DelItemsSave(string UserID, MyWardrobeData inputs)
        {
            foreach(var itemId in inputs.itemIds)
            {
                var items = from _item in mwdb.MW_USER_TAG_ITEM where _item.searchable_item_id == itemId && _item.user_id == UserID select _item;
                foreach (var item in items)
                {
                    mwdb.MW_USER_TAG_ITEM.Remove(item);
                }
                mwdb.SaveChanges();
            }
            return true;
        }
        public Boolean MoveItemsTo(string UserID, MyWardrobeData inputs)
        {
            MWTagService.MWTagService tag = new MWTagService.MWTagService();
            TagServiceData preTagInfo = new TagServiceData(){ tagId = inputs.items[0].preTagId,
                                                              itemId = inputs.itemId};
            TagServiceData newTagInfo = new TagServiceData(){ tagId = inputs.tagId,
                                                              itemId = inputs.itemId};
            if (tag.DelObjTag(UserID, preTagInfo))
            {
                if (tag.AddTagToItem(UserID, newTagInfo))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        //public Boolean MoveItemsTo(string UserID, MyWardrobeData inputs)
        //{
        //    foreach (var _item in inputs.items)
        //    {
        //        MWTagService.MWTagService tagS = new MWTagService.MWTagService();
        //        TagServiceData tagD = new TagServiceData()
        //        {
        //            itemId = _item.itemId,
        //            tagId = inputs.tagId,
        //            userId = UserID,
        //            preTagId = _item.preTagId
        //        };
        //        //if (tagS.DelObjTag(inputs) && tagS.AddTag(inputs))
        //        if (!(tagS.EditObjTag(UserID, tagD)))
        //            return false;
        //    }
        //    return true;
        //}

        public List<ItemInfo> ListAllUserItems(string UserID, MyWardrobeData inputs)
        {
            var itemRes = from _item in mwdb.MW_USER_TAG_ITEM where _item.user_id == UserID select _item;

            List<ItemInfo> items = new List<ItemInfo>();
            foreach (var item in itemRes)
            {
                MW_SEARCHABLE_ITEM seaItem = (from _item in mwdb.MW_SEARCHABLE_ITEM where _item.searchable_item_id == item.searchable_item_id select _item).FirstOrDefault();
                ItemInfo tempItem = new ItemInfo()
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
                    tagId = item.tag_id,
                    LikeCount = seaItem.MW_USER.Count,
                    DislikeCount = seaItem.MW_USER.Count
                };
                items.Add(tempItem);
            }
            return items;

        }

        public List<OutfitInfo> ListAllUserOutfits(string UserID, MyWardrobeData inputs)
        {
            IQueryable<int> outfitRes = mwdb.MW_USER_TAG_OUTFIT.Where(i => i.user_id == UserID).Select(i => i.outfit_id);

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

        public Boolean CreateUser(UserInfo usrInfo)
        {
            var user = new MW_USER()
            {
                user_id = usrInfo.userID,
                enable_SSN_login = usrInfo.enSSNLogin,
                email = usrInfo.email,
                birthday = usrInfo.birthday,
                gender = usrInfo.gender,
                name = usrInfo.name,
                credit = usrInfo.credit,
                experience = usrInfo.experience,
                occupation = usrInfo.occupation,
                nationality = usrInfo.nationality,
                location = usrInfo.location,
                profile_picture = usrInfo.profile_picture
            };
            mwdb.MW_USER.Add(user);

            mwdb.SaveChanges();

            return true;
        }

        public string TranlateEmail2UserId(string email)
        {
            return null;
        }
        
        /*
         * Ayush implemented fetch for users
         */
        public UserInfo GetUserInfo(string userId)
        {
            UserInfo userInfo = new UserInfo();
            if (userId != null)
                try
                {

                    using (var db = new MWDBEntities())
                    {
                        //var query = (from u in db.MW_USER
                        //             where u.email.ToLower().Equals(email.ToLower()) || u.name.ToLower().Equals(email.ToLower())
                        //             select u).First();

                        var query = (from u in db.MW_USER
                                     where u.user_id.ToLower().Equals(userId.ToLower())
                                     select u).FirstOrDefault();
                        if (query != null)
                        {

                            userInfo.DateTimeStr = query.birthday.ToString();
                            userInfo.gender = query.gender;
                            userInfo.name = query.email;
                            userInfo.credit = query.credit;
                            userInfo.experience = query.experience;
                            userInfo.occupation = query.occupation;
                            userInfo.nationality = query.nationality;
                            userInfo.location = query.location;
                            userInfo.profile_picture = query.profile_picture;

                        }
                        return userInfo;
                    }
                }
                catch (Exception e)
                {
                    //error?
                    return null;
                }
            else
                return null;
        }

       

        public Boolean UpdateUserInfo(UserInfo newUserInfo)
        {
            UserInfo userInfo = new UserInfo();
            Boolean failed = false;

            using (var db = new MWDBEntities())
            {
                //var query = (from u in db.MW_USER
                //             where u.email.ToLower().Equals(userInfo.email.ToLower())
                //             select u).First();
                //var query = db.MW_USER.FirstOrDefault(u => u.email.Equals(userInfo.email));
                var query = db.MW_USER.FirstOrDefault(u => u.email == newUserInfo.email);

                if (query != null)
                {
                    try
                    {
                        if (
                            newUserInfo.DateTimeStr != query.birthday.ToString()
                            )
                            query.birthday = DateTime.Parse(newUserInfo.DateTimeStr);
                        
                        if (newUserInfo.gender != query.gender )
                            query.gender = newUserInfo.gender;
                        
                        if (newUserInfo.name != query.name )
                            query.name = newUserInfo.name;
                        
                        if (newUserInfo.credit != query.credit)
                            query.credit = newUserInfo.credit;
                        
                        if (newUserInfo.experience != query.experience)
                            query.experience = newUserInfo.experience;
                        
                        if (newUserInfo.occupation != query.occupation)
                            query.occupation = newUserInfo.occupation;
                        
                        if (newUserInfo.nationality != query.nationality)
                            query.nationality = newUserInfo.nationality;
                        
                        if (newUserInfo.location != query.location)
                            query.location = newUserInfo.location;
                        
                        if (newUserInfo.profile_picture != query.profile_picture)
                            query.profile_picture = newUserInfo.profile_picture;
                        db.SaveChanges();
                    }

                    catch (Exception e)
                    {
                        //error
                        return failed;
                    }
                    return !failed;
                }
                return failed;

            }
        }
    }
}
