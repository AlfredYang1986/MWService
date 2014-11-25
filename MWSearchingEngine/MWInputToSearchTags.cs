using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Web.Hosting;

namespace MWSearchingEngine
{
    #region
    class InputToSearchTags
    {
        IDictionary<string, IList<string>> tagsMaping
            = new Dictionary<string, IList<string>>()
            as IDictionary<string, IList<string>>;

        private string strPath = Path.Combine(HostingEnvironment.MapPath("~/Config"), @"\Tags.xml");
        private XmlDocument _doc;
        private XmlElement _root;

        public IDictionary<string, IList<string>> Tags
        {
            get { return tagsMaping; }
        }
        // Constructor
        public InputToSearchTags() 
        {
            try
            {
                _doc = new XmlDocument();
                _doc.Load(strPath);
                _root = _doc.DocumentElement;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            InitTagsList(); 
        }

        private void InitTagsList()
        {
            // all of this shall be locating in the one XML when the server
            // start and load it now it is hard code.
            IList<string> sizeTages = new List<string>() as IList<string>;
            InitSizeTags(sizeTages);
            tagsMaping.Add("Size", sizeTages);

            // gender
            IList<string> genderTags = new List<string>() as IList<string>;
            InitGenderTags(genderTags);
            tagsMaping.Add("Gender", genderTags);

            // Brand
            IList<string> brandTags = new List<string>() as IList<string>;
            InitBrandTags(brandTags);
            tagsMaping.Add("Brand", brandTags);

            // Color
            IList<string> colorTages = new List<string>() as IList<string>;
            InitColorTags(colorTages);
            tagsMaping.Add("Color", colorTages);

            // Categories
            IList<string> categoryTags = new List<string>() as IList<string>;
            InitCategoryTags(categoryTags);
            tagsMaping.Add("Category", categoryTags);

            
            //foreach(var category in categoryTags)
            //{
            //    IList<string> subCategoryTags = new List<string>() as IList<string>;
            //    InitSubCategoryTags(subCategoryTags,category);
            //    tagsMaping.Add(category+"SubCategory", subCategoryTags);
            //}

            
            
        }

        private void InitColorTags(IList<string> colorTages)
        {
            XmlNodeList nl = _doc.SelectNodes("//Tags/Colors/Color");

            foreach (XmlNode n in nl)
            {
                colorTages.Add(n.Attributes.GetNamedItem("value").Value);
            }
        }

        private void InitSizeTags(IList<string> sizeTags)
        {
            XmlNodeList nl = _doc.SelectNodes("//Tags/Sizes/Size");

            foreach (XmlNode n in nl)
            {
                sizeTags.Add(n.Attributes.GetNamedItem("value").Value);
            }
        }

        private void InitGenderTags(IList<string> genderTags)
        {
            XmlNodeList nl = _doc.SelectNodes("//Tags/Genders/Gender");

            foreach (XmlNode n in nl)
            {
                genderTags.Add(n.Attributes.GetNamedItem("value").Value);
            }
        }

        private void InitBrandTags(IList<string> brandTags)
        {
            XmlNodeList nl = _doc.SelectNodes("//Tags/Brands/Brand");

            foreach (XmlNode n in nl)
            {
                brandTags.Add(n.Attributes.GetNamedItem("value").Value);
            }
        }

        private void InitCategoryTags(IList<string> categoryTags)
        {
            XmlNodeList nl = _doc.SelectNodes("//Tags/Categories/Category");

            //foreach (XmlNode n in nl)
            //{
            //    if(n.Attributes.GetNamedItem("parent").Value.Equals("true"))
            //      categoryTags.Add(n.Attributes.GetNamedItem("value").Value);
            //}

            foreach (XmlNode n in nl)
            {
                    categoryTags.Add(n.Attributes.GetNamedItem("value").Value);
            }
        }
        private void InitSubCategoryTags( IList<string> subCategoryTags,string category)
        {
           // IList<string> subCategory = new List<string>() as IList<string>;
            XmlNodeList nl = _doc.SelectNodes("//Tags/Categories/Category");

            foreach (XmlNode n in nl)
            {
                if (n.Attributes.GetNamedItem("parent").Value.Equals(category))
                {
                    subCategoryTags.Add(n.Attributes.GetNamedItem("value").Value);
                }
            }
        }

       
        public string[] InputPhrases(string input)
        {
            return Regex.Split(input, @"\s+|\+");
          
        }

        public string MatchPhraseWithTag(string phrase)
        {
            string firstLetterValue = phrase.Substring(0, 1);
            string elseValue = phrase.Substring(1, phrase.Count() - 1);
            phrase = firstLetterValue.ToUpper() + elseValue;
         //   IList<string> valueList;
            foreach (var pair in tagsMaping)
            {
                if (pair.Key.ToLower() == "color")
                    foreach (string value in pair.Value)
                    {
                        if (value.ToUpper().Contains(phrase.ToUpper()))
                            return pair.Key;
                    }
                else
                    foreach (string value in pair.Value)
                    {
                        if (value.Equals(phrase))
                            return pair.Key;
                    }
            }

                return null;
            
        }

        public Boolean MatchTags(string tag)
        {
            foreach (var pair in tagsMaping)
            {
                if (pair.Key.Equals(tag))
                    return true;
            }
            return false;
        }
    }
    #endregion
}