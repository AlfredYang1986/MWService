using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MWDataEntity;
using MWDataSerilizationType;

namespace MWDataEntity 
{
    /************************************************************************/
    /* Factory method for the Dll                                           */
    /************************************************************************/
    public class MWDataEntityFactory
    {
        /************************************************************************/
        /* singleton for one Application                                        */
        /************************************************************************/
        static MWDataEntityImpl _impl = new MWDataEntityImpl();

        public static IMWDataEntityInterface NewInstance()
        {
            return _impl;
        }
    }

    public interface IMWDataEntityInterface : IDisposable
    {
        IList<Apparel> SearchByTagsAndValue(
            SearchingArgs searchArgs,
            SearchingArgs notContainArgs,
            string[] orderBy,
            string[] orderByDescending,
            int currentPage,
            int pageCount = 200);

        IList<string> QueryProsibleDataInTag(string tags);

        IList<string> QueryProsibleDataInBrand();

        IList<string> QueryProsibleDataInColor();

        IList<string> QueryProsibleDataInGender();

        IList<string> QueryProsibleDataInSize();

        int getLikeCount(string item_id);

        Boolean UpdateLikeCountDB(string searchableId, string userId);
    }

    public class MWSerilizationTypeFactory
    {

        static public Apparel CreateCloth(MW_SEARCHABLE_ITEM p)
        {
            IEnumerator<MW_UNIQ_ITEM> mw_uniq_item = p.MW_UNIQ_ITEM.GetEnumerator();
            List<string> sizeList=new List<string>();
            while (mw_uniq_item.MoveNext())
            {
                sizeList.Add(mw_uniq_item.Current.MW_SIZE.size_val);
            }

                return new Apparel
                {
                    ItemID = p.searchable_item_id,
                    Brand = p.MW_ABSTRACT_ITEM.MW_BRAND.brand_name,
                    Color = p.MW_COLOUR.colour_name,
                    Size = sizeList,
                    SizeType = p.MW_UNIQ_ITEM.FirstOrDefault().MW_SIZE.size_type,
                    Gender = p.MW_ABSTRACT_ITEM.gender,
                    Price = p.price,
                    ImageURL = p.MW_PICTURE.url,
                    Title = p.title,
                    Category = p.MW_ABSTRACT_ITEM.MW_CATEGORY.category_name,
                    DiscountPrice = (float)p.discount,
                    ItemUrl = p.from_url,
                    Likes = p.MW_USER.Count(),
                    //Dislikes = p.MW_USER.Count(),
                    SourceName = p.MW_APPAREL_SOURCE.source_name
                };
                
        }

        static public ClothDetail CreateClothDetail(MW_SEARCHABLE_ITEM p)
        {
            Apparel c = CreateCloth(p);

            return new ClothDetail
            {
                UpperPrice = p.MW_ABSTRACT_ITEM.upper_price.HasValue ? p.MW_ABSTRACT_ITEM.upper_price.Value :0.0,
                LowerPrice = p.MW_ABSTRACT_ITEM.lower_price.HasValue ? p.MW_ABSTRACT_ITEM.lower_price.Value :0.0,
                Description = "Alfred Yang",
            };
        }
    } 
}
