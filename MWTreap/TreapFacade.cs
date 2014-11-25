using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;
using System.Web.Hosting;

namespace MWTreap
{
    class TreapFacade : IMWTreapInterface
    {
        public void Dispose()
        {

        }

        private string strPath = Path.Combine(HostingEnvironment.MapPath("~/Config"), @"\Tags.xml");
        private Treap _treap = new Treap();
        private XDocument _doc;

        /************************************************************************/
        /* constructor                                                          */
        /*      1. Load Xml file                                                */
        /*      2. Construct treap using Xml values                             */ 
        /************************************************************************/
        public TreapFacade()
        {
            _doc = XDocument.Load(strPath);
            constructTreap();
        }

        private void constructTreap()
        {
            var queryBrands = from el in _doc.Descendants("Brand")
                        select new
                        {
                            value = el.Attribute("value").Value,
                            refCount = 0,
                            category = @"Brand",
                        };

            var queryColors = from el in _doc.Descendants("Color")
                        select new
                        {
                            value = el.Attribute("value").Value,
                            refCount = 0,
                            category = @"Color",
                        };
            var queryStyles = from el in _doc.Descendants("Style")
                              select new
                              {
                                  value = el.Attribute("value").Value,
                                  refCount = 0,
                                  category = @"Style",
                              };
            var querySizes = from el in _doc.Descendants("Size")
                              select new
                              {
                                  value = el.Attribute("value").Value,
                                  refCount = 0,
                                  category = @"Size",
                              };
            var queryCategory = from el in _doc.Descendants("Category")
                                select new
                                {
                                    value = el.Attribute("value").Value,
                                    refCount = 0,
                                    category = @"Category",
                                };      

            foreach (var v in queryBrands)
            {
                _treap.InsertLabel(v.value, v.refCount, v.category);
            }

            foreach (var v in queryColors)
            {
                _treap.InsertLabel(v.value, v.refCount, v.category);
            }
            foreach (var v in queryStyles)
            {
                _treap.InsertLabel(v.value, v.refCount, v.category);
            }
            foreach (var v in querySizes)
            {
                _treap.InsertLabel(v.value, v.refCount, v.category);
            }
            foreach (var v in queryCategory)
            {
                _treap.InsertLabel(v.value, v.refCount, v.category);
            }
           
        }

        public string[] autoComplete(string strInput)
        {
            return _treap.searchTreapWithPriority(strInput, 
                _treap.Root.LeftChild, Helper_Functions.Edit_Distance, 
                Helper_Functions.Hamming, 5);
        }

        public string nodifyError(string strInput)
        {
            return _treap.searchTreapWithPriority(strInput,
                _treap.Root.LeftChild, Helper_Functions.Edit_Distance, 
                Helper_Functions.Hamming, 1).First();
        }
        
        /************************************************************************/
        /* greedy algorithm                                                     */
        /************************************************************************/
        public MWTreap.MWTreapFactory.SplitResult splitUserInput(string strInput, ref string strErrors
            , ref Dictionary<string, IList<string>> searchArgs
            , ref Dictionary<string, IList<string>> not_searchArgs
            , ref List<string> orderBy
            , ref List<string> orderByDesending)
        {
            MWTreap.MWTreapFactory.SplitResult sr = MWTreap.MWTreapFactory.SplitResult.error;
            // 1. replace '-' with ' '
            strInput = strInput.Replace('-', ' ');
            // 2. split into words
            List<string> str_array = Regex.Split(strInput.Trim(), @"\s+").ToList();
            // 3. use greedy to guess only get the first
            LinkedList<MWTreap.Treap.dis_node> result = new LinkedList<MWTreap.Treap.dis_node>();
            int skip_step = 0;
            while (str_array.Count > 0)
            {
                result.Clear();
                skip_step = 0;
                _treap.searchInTreap_greed(str_array, _treap.Root.LeftChild, ref result);
                // 4. use hd, and ed to judument
                sr = addArguments(result, ref strErrors, ref searchArgs, ref not_searchArgs, ref skip_step);
                str_array.RemoveRange(0, skip_step);
            }

            // 5. add errors
            return sr;
        }
        public void addOtherArguments(ref Dictionary<string, IList<string>> args
                                      , string category
                                      , IList<string> values)
        {
            foreach(string value in values)
                addSearchArguments(ref args,category,value);

        }
      
        private void addSearchArguments(ref Dictionary<string, IList<string>> args
                                      , string category
                                      , string value)
        {
            if (!args.ContainsKey(category))
            {
                List<string> ls = new List<string>();
                args.Add(category, ls);
            }
            args[category].Add(value);
        }
        
        private MWTreap.MWTreapFactory.SplitResult addArguments(LinkedList<MWTreap.Treap.dis_node> result
                                                              , ref string strErrors
                                                              , ref Dictionary<string, IList<string>> searchArgs
                                                              , ref Dictionary<string, IList<string>> not_searchArgs
                                                              , ref int skip_step)
        {
            MWTreap.MWTreapFactory.SplitResult sr = MWTreap.MWTreapFactory.SplitResult.error;

            var good_guess_r =  from guess in result
                                orderby guess.hd descending, guess.ed, guess.cum descending
                                select guess;
            
            var good_guess = good_guess_r.FirstOrDefault();
                

          if (good_guess == null)
              return sr;

            if ( good_guess.hd == 0)
                {
                    sr = sr != MWTreapFactory.SplitResult.warning
                            ? MWTreap.MWTreapFactory.SplitResult.no_error
                            : MWTreapFactory.SplitResult.warning;

                    skip_step = good_guess.cum;
                }

                else if (good_guess.ed <= good_guess.tn.Label.Length / 2)
                {
                    sr = MWTreap.MWTreapFactory.SplitResult.warning;
                    strErrors = "good guess";

                    skip_step = good_guess.cum;
                }
                else
                {
                    skip_step = 1;
                }

                if (good_guess.bNot)
                    addSearchArguments(ref not_searchArgs, good_guess.tn.Category, good_guess.tn.Label);
                else
                    addSearchArguments(ref searchArgs, good_guess.tn.Category, good_guess.tn.Label);
           
            return sr;
        }

        public MWTreapFactory.SplitResult AppAVAudioCandiSearch(IEnumerable<string> can, ref string key, ref string value)
        {
            List<TreapNode> reVal = new List<TreapNode>();
            foreach (var c in can)
            {
                IEnumerable<string> str_array = Regex.Split(c.Trim(), @"\s+").ToList();
                TreapNode val = null;
                foreach (var str in str_array)
                {
                    TreapNode tmp = this._treap.searchTreap(this._treap.Root.LeftChild, str);
                    if ((tmp != null) && ((val == null) || (tmp.Label.Count() < val.Label.Count())))
                        val = tmp;
                }
                if (val != null) reVal.Add(val);
            }
    
            if (reVal.Count == 0) return MWTreapFactory.SplitResult.error;
            else
            {
                key = reVal.FirstOrDefault().Category;
                value = reVal.FirstOrDefault().Label;
                return MWTreapFactory.SplitResult.no_error;
            }
        }
    }
}
