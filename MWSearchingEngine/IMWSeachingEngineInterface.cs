using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MWDataEntity;
using MWDataSerilizationType;

namespace MWSearchingEngine
{
    /************************************************************************/
    /* Factory method for the Dll                                           */
    /************************************************************************/
    public class MWSearchingEngineFactory 
    {
        /************************************************************************/
        /* singleton for one Application                                        */
        /************************************************************************/
        static IMWSearchingEngineInterface _impl = new MWSearchingEngineImpl();

        public static IMWSearchingEngineInterface NewInstance()
        {
            return _impl;
        }
    }

    public interface IMWSearchingEngineInterface : IDisposable
    {
        IEnumerable<Apparel> AppSearchingDemo(string strTags);
        IEnumerable<Apparel> AppSearchingDemo_2(IEnumerable<string> strTags);

        SearchResult SearchingWithParams(SearchingParams asinput);

        IList<string> GetBrand();
        IList<string> GetCategory();
        IEnumerable<string> GetColorCandidata();
        IList<string> GetsubCategory(string parentCategory);
        IList<string> Getscences();
        IList<String> AutoCompletion(string strInput);
        Boolean UpdateLikeCount(string UserID,UpdateLikeCountParams updateInfo);
        //void PopulateItemTags();
    }

    [DataContract]
    public class BrandComplete
    {
        public BrandComplete(String brand)
        {
            Brand = brand;
        }

        [DataMember]
        public string Brand { get; set; }
    }

    [DataContract]
    public class CategoryComplete
    {
        public CategoryComplete(String category)
        {
            Category = category; 
        }

        [DataMember]
        public string Category { get; set; }
    }
}