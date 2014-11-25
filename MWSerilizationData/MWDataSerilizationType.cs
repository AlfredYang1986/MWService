using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace MWDataSerilizationType
{
    [DataContract]
    public class BaseSearchResult { }

    [DataContract]
    public class SearchResult 
    {
        // search result
        private IList<Apparel> results;
        // search errors, can be detected with user input 
        private string errors;

        
        [DataMember]
        public IList<Apparel> Results
        {
            get { return results; }
            set { results = value; }
        }
        
        [DataMember]
        public string Error
        {
            get { return errors; }
            set { errors = value; }
        }
    }
	
    [DataContract]
    public class Apparel 
    {
        private int nID;
        private string strBrand;
        private string strColor;
        private List<string> l_size = new List<string>();
        private string strSizeType;
        private string strGender;
        private Double nPrice;
        private string strImageUrl;
        private string strTitle;
        private string category;
        private float discountPrice;
        private string itemUrl;
        private int likeCount;
        private int disLikeCount;
        private string sourceName;

        [DataMember]
         public string SourceName
        {
            get { return sourceName; }
            set { sourceName = value; }
        }

        [DataMember]
        public int Likes 
        {
            get { return likeCount;}
            set { likeCount = value;}
        }

        [DataMember]
        public int Dislikes 
        {
            get { return disLikeCount; }
            set { disLikeCount = value; }
        }

        [DataMember]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        [DataMember]
        public float DiscountPrice
        {
            get { return discountPrice; }
            set { discountPrice = value; }
        }

        [DataMember]
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }

        [DataMember]
        public string Brand
        {
            get { return strBrand; }
            set { strBrand = value; }
        }

        [DataMember]
        public string Color
        {
            get { return strColor; }
            set { strColor = value; }
        }

        [DataMember]
        public List<string> Size
        {
            get { return l_size; }
            set { l_size = value; }
        }

        [DataMember]
        public string Gender
        {
            get { return strGender; }
            set { strGender = value; }
        }

        [DataMember]
        public int ItemID 
        {
            get { return nID; }
            set { nID = value; }
        }

        [DataMember]
        public string SizeType
        {
            get { return strSizeType; }
            set { strSizeType = value; }
        }

        [DataMember]
        public Double Price
        {
            get { return nPrice; }
            set { nPrice = value; }
        }

        [DataMember]
        public string ImageURL
        {
            get { return strImageUrl; }
            set { strImageUrl = value; }
        }

        [DataMember]
        public string Title
        {
            get { return strTitle; }
            set { strTitle = value; }
        }

        public override string ToString()
        {
            return "Brand: " + strBrand +
                "\nColor: " + strColor +
                "\nSource: " + strImageUrl +
                "\nTitle: " + strTitle + "\n";
        }
    }

    [DataContract]
    public class ClothDetail
    {
        private Double  upper_Price;
        private Double  lower_Price;
        private string strDescribtion;

        private Apparel c;

        [DataMember]
        public Apparel Cloth 
        {
            get { return c; }
            set { c = value; }
        }

        [DataMember]
        public Double UpperPrice
        {
            get { return upper_Price; }
            set { upper_Price = value; }
        }

        [DataMember]
        public Double LowerPrice
        {
            get { return lower_Price; }
            set { lower_Price = value; }
        }
        
        [DataMember]
        public string Description
        {
            get { return strDescribtion; }
            set { strDescribtion = value; }
        }
    } 

    [DataContract]
    public class TagData 
    {
        private string strTag;

        public string Tag
        {
            get { return strTag; }
            set { strTag = value;}
        }
    }

    [DataContract]
    public class SearchingParams
    {
        [DataMember]
        public string input { get; set; }
        [DataMember]
        public IEnumerable<SearchCondition> conditions { get; set; }
        [DataMember]
        public IEnumerable<SearchCondition> notConditions { get; set; }
        [DataMember]
        public MWPrice price { get; set; }
        [DataMember]
        public int currentPage { get; set; }
        [DataMember]
        public string sortName { get; set; }
        [DataMember]
        public string sortMethod { get; set; }
        [DataMember]
        public string sence { get; set; }
    }

    [DataContract]
    public class UpdateLikeCountParams
    {
        [DataMember]
        public string ItemID { get; set; }

        [DataMember]
        public string userId { get; set; }
    }

    [DataContract]
    public class SearchCondition
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string val { get; set; }
    }
    [DataContract]
    public class SearchingArgs
    {
        [DataMember]
        public Dictionary<string, IList<string>> args { get; set; }
    }

	[DataContract]
    public class MWSearchingwithPrice
    {
        [DataMember]
        public MWPrice price { get; set; }
        [DataMember]
        public string Query { get; set; }
    }
	
	[DataContract]
   public class MWPrice
    {
        [DataMember]
        public int maxPrice { get; set; }
        [DataMember]
        public int minPrice { get; set; }
    }
	
    [DataContract]
    public class Item
    {
        [DataMember]
        public string ItemId { get; set; }
        [DataMember]
        public string Xposition { get; set; }
        [DataMember]
        public string Yposition { get; set; }
        [DataMember]
        public string Top { get; set; }
        [DataMember]
        public string Left { get; set; }
        [DataMember]
        public string Width { get; set; }
        [DataMember]
        public string Height { get; set; }
    }

    [DataContract]
    public class MWAcceptRequestJSON
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public List<Item> Items;
    }

    [DataContract]
    public class MWShareRequestJSON
    {
        [DataMember]
        public string UserId;
        [DataMember]
        public string FriendId;
        [DataMember]
        public List<Item> Items;
    }
    [DataContract]
    public class MWSPEnvironmentJSON
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public List<Item> Items;
    }
   // public class MW_TW_Base { }
	
	[DataContract]
    public class TagServiceData //: MW_TW_Base
    {
        [DataMember]
        public string userId { get; set; }

        [DataMember]
        public int tagId { get; set; }

        [DataMember]
        public string tagName { get; set; }

        [DataMember]
        public string tagType { get; set; }

        [DataMember]
        public int itemId { get; set; }

        [DataMember]
        public int outfitId { get; set; }

        [DataMember]
        public int picId { get; set; }

        [DataMember]
        public string timeAdded { get; set; }

        [DataMember]
        public int pageIndex { get; set; }
        [DataMember]
        public List<Tags> sTags { get; set; }
        [DataMember]
        public int tagsLength { get; set; }

        [DataMember]
        public string coverUrl { get; set; }
        [DataMember]
        public int preTagId { get; set; }
    }

    public class SaveTags
    {
        [DataMember]
        public List<int> tagIds { get; set; }
        [DataMember]
        public List<string> tagNames { get; set; }
    }

    [DataContract]
    public class Tags
    {
        [DataMember]
        public int tagId { get; set; }
        [DataMember]
        public string tagName { get; set; }
        [DataMember]
        public string tagType { get; set; }
        [DataMember]
        public string tagPicUrl { get; set; }
    }

    [DataContract]
    public class ItemInfo
    {
        [DataMember]
        public int seaItemId { get; set; }
        [DataMember]
        public string brand { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string fromUrl { get; set; }
        [DataMember]
        public string addedTime { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string itemPicUrl { get; set; }
        [DataMember]
        public int tagId { get; set; }
        [DataMember]
        public int LikeCount { get; set; }
        [DataMember]
        public int DislikeCount { get; set; }
    }

    [DataContract]
    public class OutfitInfo
    {
        [DataMember]
        public int outfitId { get; set; }
        [DataMember]
        public string outfitName { get; set; }

        [DataMember]
        public string outfitTime { get; set; }
    }

    [DataContract]
    public class MyWardrobeData
    {
        [DataMember]
        public string userId { get; set; }

        [DataMember]
        public int outfitId { get; set; }

        [DataMember]
        public string outfitName { get; set; }

        [DataMember]
        public List<OutfitItem> outfitItems { get; set; }

        [DataMember]
        public int itemId { get; set; }

        [DataMember]
        public int tagId { get; set; }

        [DataMember]
        public List<int> itemIds { get; set; }

        [DataMember]
        public List<itemBriefInfo> items { get; set; }

        [DataMember]
        public int likeCount { get; set; }

        [DataMember]
        public int disLikeCount { get; set; }


    }

    [DataContract]
    public class itemBriefInfo
    {
        [DataMember]
        public int itemId { get; set; }
        [DataMember]
        public int preTagId { get; set; }
    }

    [DataContract]
    public class OutfitItem
    {
        [DataMember]
        public int outfitId { get; set; }

        [DataMember]
        public int seaId { get; set; }

        [DataMember]
        public Nullable<int> picId { get; set; }

        [DataMember]
        public string picUrl { get; set; }

        [DataMember]
        public int picX { get; set; }

        [DataMember]
        public int picY { get; set; }

        [DataMember]
        public Nullable<float> angle { get; set; }

        [DataMember]
        public Nullable<float> cropX { get; set; }

        [DataMember]
        public Nullable<float> cropY { get; set; }

        [DataMember]
        public Nullable<float> cropWidth { get; set; }
        [DataMember]
        public Nullable<float> cropHeight { get; set; }

        [DataMember]
        public Nullable<float> sizingFactor { get; set; }

        [DataMember]
        public int zIndex { get; set; }

    }

    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string userID { get; set; }

        [DataMember]
        public bool enSSNLogin { get; set; }

        [DataMember]
        public string email { get; set; }

        //[DataMember]
        //public Nullable<System.DateTime> birthday { get; set; }
        public Nullable<System.DateTime> birthday { get { return DateTime.Parse(DateTimeStr); } }
        [DataMember]
        public string DateTimeStr { get; set; }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public Nullable<int> credit { get; set; }

        [DataMember]
        public Nullable<int> experience { get; set; }

        [DataMember]
        public string occupation { get; set; }

        [DataMember]
        public string nationality { get; set; }

        [DataMember]
        public string location { get; set; }

        [DataMember]
        public Nullable<int> profile_picture { get; set; }
    }

    [DataContract]
    public class MWTag_WardrobeeServiceJson {
        [DataMember]
        public string userID { get; set; }
        [DataMember]
        public string tagInfo { get; set; }
    }
}
