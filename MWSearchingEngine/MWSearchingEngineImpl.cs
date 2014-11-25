using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MWDataEntity;
using MWDataSerilizationType;
using MWTreap;
using System.Text.RegularExpressions;

namespace MWSearchingEngine
{
    class MWSearchingEngineImpl : IMWSearchingEngineInterface
    {
        public void Dispose()
        {

        }

        private InputToSearchTags inputPhrase = new InputToSearchTags();

        private void AddArguments(string str_temp,
            ref Dictionary<string, IList<string>> searchArgs,
            ref Dictionary<string, IList<string>> not_searchArgs)
        {
            Boolean bNot = str_temp.StartsWith("-");
            string phrase = bNot ? str_temp.Substring(1) : str_temp;

            string strTags = inputPhrase.MatchPhraseWithTag(phrase);

            if (strTags == null)
                return;

            // Change this to mapping
            if (strTags.ToLower() == "color")
                strTags = "colour";

            if (!bNot)
            {
                if (!searchArgs.ContainsKey(strTags))
                {
                    List<string> ls = new List<string>();
                    searchArgs.Add(strTags, ls);
                }

                searchArgs[strTags].Add(phrase);
            }
            else
            {
                if (!not_searchArgs.ContainsKey(strTags))
                {
                    List<string> ls = new List<string>();
                    not_searchArgs.Add(strTags, ls);
                }

                not_searchArgs[strTags].Add(phrase);
            }
        }

        public SearchResult SearchingWithParams(SearchingParams asinput)
        {
            
            int minPrice = 0, maxPrice = 0;
            // price
            if (asinput.price != null)
            {
                minPrice = asinput.price.minPrice;
                maxPrice = asinput.price.maxPrice;
            }

            string strInput = asinput.input;

            IList<Apparel> reVal = new List<Apparel>();

            Dictionary<string, IList<string>> searchArgs
                = new Dictionary<string, IList<string>>();
            Dictionary<string, IList<string>> not_searchArgs
                = new Dictionary<string, IList<string>>();

            List<string> orderBy = new List<string>();
            List<string> orderByDesending = new List<string>();
            if (asinput.sortName != "none" && asinput.sortName != null)
                if (asinput.sortMethod == "Up")
                    orderBy.Add(asinput.sortName);
                else
                    orderByDesending.Add(asinput.sortName);
            else
                orderBy.Add("price");

            string search_error = string.Empty;

            //Process input

            MWTreap.MWTreapFactory.SplitResult sr = MWTreapFactory.SplitResult.no_error;


            using (var tf = MWTreapFactory.NewInstance())
            {
                if (strInput.Trim() != "null")
                    sr = tf.splitUserInput(strInput.Trim(), ref search_error, ref searchArgs, ref not_searchArgs, ref orderBy, ref orderByDesending);

                IList<string> priceList = new List<string>() as IList<string>;
                priceList.Add(minPrice.ToString());
                priceList.Add(maxPrice.ToString());
                tf.addOtherArguments(ref searchArgs, "price", priceList);

                if(asinput.sence != null && asinput.sence != @"")
                { 
                    IList<string> senceList = new List<string>() as IList<string>;
                    senceList.Add(asinput.sence);
                    tf.addOtherArguments(ref searchArgs, "sence", senceList);
                 }

                //add conditions
                if (asinput.conditions != null)
                {
                    foreach (var condition in asinput.conditions)
                    {
                        if (!searchArgs.ContainsKey(condition.type))
                        {
                            List<string> ls = new List<string>();
                            searchArgs.Add(condition.type, ls);
                        }
                        searchArgs[condition.type].Add(condition.val);
                    }
                }

                //add non conditions
                if (asinput.notConditions != null)
                {
                    foreach (var condition in asinput.notConditions)
                    {
                        if (!not_searchArgs.ContainsKey(condition.type))
                        {
                            List<string> ls = new List<string>();
                            not_searchArgs.Add(condition.type, ls);
                        }
                        not_searchArgs[condition.type].Add(condition.val);
                    }
                }
            }

            if (sr != MWTreapFactory.SplitResult.error)
            {
                using (var entity = MWDataEntityFactory.NewInstance())
                {
                    SearchingArgs args = new SearchingArgs();
                    args.args = searchArgs;

                    SearchingArgs not_args = new SearchingArgs();
                    not_args.args = not_searchArgs;

                    //if (searchArgs.Count > 0)
                    reVal = entity.SearchByTagsAndValue(
                        args, not_args,
                        orderBy.ToArray(),
                        orderByDesending.ToArray(),
                        asinput.currentPage);
                }
            }
            PopulateItemLikesDislikes(ref reVal);
            return new SearchResult() { Error = "none", Results = reVal };
        }


        public Boolean UpdateLikeCount(string UserID, UpdateLikeCountParams updateInfo)
        {
            string ItemID = updateInfo.ItemID;
            string userId = UserID;
            using (var entity = MWDataEntityFactory.NewInstance())
            {           
                return entity.UpdateLikeCountDB(ItemID, userId);
                
            }
        }

        public void PopulateItemLikesDislikes(ref IList<Apparel> results)
        {
            IList<Apparel> newResult = new List<Apparel>();
            //if there is some result
            if(results.Count != 0 )
            {
                MWDBEntities mwdb = new MWDBEntities();
                //loop through the items
                foreach (var myApparel in results)
                {
                    var item = (from _item in mwdb.MW_SEARCHABLE_ITEM where _item.searchable_item_id == myApparel.ItemID select _item).FirstOrDefault();
                    myApparel.Likes = item.MW_USER.Count();
                    myApparel.Dislikes = item.MW_USER.Count();
                }
            }
        }

        public IEnumerable<Apparel> AppSearchingDemo_2(IEnumerable<string> strTags)
        {
            string key = @"";
            string value = @"";
            MWTreap.MWTreapFactory.SplitResult sr = MWTreapFactory.SplitResult.no_error;
            using (var t = MWTreapFactory.NewInstance())
            {
                sr = t.AppAVAudioCandiSearch(strTags, ref key, ref value);
            }

            IList<Apparel> reVal = new List<Apparel>();
            if (sr != MWTreapFactory.SplitResult.error)
            {
                Dictionary<string, IList<string>> searchArgs
                    = new Dictionary<string, IList<string>>();

                List<string> ls = new List<string>();
                searchArgs.Add(key, ls);
                searchArgs[key].Add(value); 

                Dictionary<string, IList<string>> not_searchArgs
                    = new Dictionary<string, IList<string>>();

                List<string> orderBy = new List<string>();
                List<string> orderByDesending = new List<string>();
                orderBy.Add("price");

                using (var entity = MWDataEntityFactory.NewInstance())
                {
                    SearchingArgs args = new SearchingArgs();
                    args.args = searchArgs;

                    SearchingArgs not_args = new SearchingArgs();
                    not_args.args = not_searchArgs;

                    reVal = entity.SearchByTagsAndValue(
                        args, not_args,
                        orderBy.ToArray(),
                        orderByDesending.ToArray(), 0, 20);
                }
            }
            PopulateItemLikesDislikes(ref reVal);
            return reVal;
        }

        public IEnumerable<Apparel> AppSearchingDemo(string strInput)
        {
            IList<Apparel> reVal = new List<Apparel>();

            Dictionary<string, IList<string>> searchArgs
                = new Dictionary<string, IList<string>>();
            Dictionary<string, IList<string>> not_searchArgs
                = new Dictionary<string, IList<string>>();

            List<string> orderBy = new List<string>();
            List<string> orderByDesending = new List<string>();
            orderBy.Add("price");

            string search_error = string.Empty;
            //Process input

            MWTreap.MWTreapFactory.SplitResult sr = MWTreapFactory.SplitResult.no_error;
            using (var tf = MWTreapFactory.NewInstance())
            {
                if (strInput.Trim() != "null")
                    sr = tf.splitUserInput(strInput.Trim(), ref search_error, ref searchArgs, ref not_searchArgs, ref orderBy, ref orderByDesending);

                IList<string> priceList = new List<string>() as IList<string>;
                priceList.Add(@"0");
                priceList.Add(@"10000");
                tf.addOtherArguments(ref searchArgs, "price", priceList);
            }

            if (sr != MWTreapFactory.SplitResult.error)
            {
                using (var entity = MWDataEntityFactory.NewInstance())
                {
                    SearchingArgs args = new SearchingArgs();
                    args.args = searchArgs;

                    SearchingArgs not_args = new SearchingArgs();
                    not_args.args = not_searchArgs;

                    //if (searchArgs.Count > 0)
                    reVal = entity.SearchByTagsAndValue(
                        args, not_args,
                        orderBy.ToArray(),
                        orderByDesending.ToArray(), 0, 20);
                }
            }
            PopulateItemLikesDislikes(ref reVal);
            return reVal;
        }

        public IList<string> GetBrand()
        {
            IList<string> reVal = new List<string>();
            IDictionary<string, IList<string>> tags = inputPhrase.Tags;
            foreach (var tag in tags)
            {
                if (tag.Key == "Brand")
                    reVal = tag.Value;
            }

            return reVal;
        }

        public IList<string> GetCategory()
        {
            IList<string> reVal = new List<string>();
            IDictionary<string, IList<string>> tags = inputPhrase.Tags;
            foreach (var tag in tags)
            {
                if (tag.Key == "Category")
                    reVal = tag.Value;
            }

            return reVal;
        }

        public IList<string> GetsubCategory(string parentCategory)
        {
            IList<string> reVal = new List<string>();
            IDictionary<string, IList<string>> tags = inputPhrase.Tags;
            foreach (var tag in tags)
            {
                if (tag.Key.ToLower() == parentCategory.ToLower())
                    reVal = tag.Value;
            }

            return reVal;

        }
        public IList<string> Getscences()
        {
            using (var db = new MWDBEntities())
            {
                var result = (from tag in db.MW_TAG
                              where tag.tag_type == "Occasion"
                              select tag.tag_name).Distinct();
                return result.ToList();
            }
            
        }

        public IList<String> AutoCompletion(string strInput)
        {
            using (var tf = MWTreapFactory.NewInstance())
            {
                return tf.autoComplete(strInput);
            }
        }

        public IEnumerable<string> GetColorCandidata()
        {
            IList<string> reVal = new List<string>();
            IDictionary<string, IList<string>> tags = inputPhrase.Tags;
            foreach (var tag in tags)
            {
                if (tag.Key == "Color")
                    reVal = tag.Value;
            }

            return reVal;
        }
    }
}
