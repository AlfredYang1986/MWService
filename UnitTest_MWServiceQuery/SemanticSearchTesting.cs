using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MWDispatchService;
//Ayush Added Part Begin
using System.Xml;
using System.IO;
using System.Xml.Linq;
using MWDataEntity;

using System.Linq.Expressions;
using System.Data.Entity;

using MWSearchingEngine;
using MWDataSerilizationType;

//Ayush Added Part End


namespace UnitTest_MWServiceQuery
{
    class SemanticSearchTesting
    {
        
        
        static void Main(string[] args)
        {
            /* Condition Testing */
/*
            IMWSearchingEngineInterface _search =
                MWSearchingEngineFactory.NewInstance();
            string strPath = Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory) + @"\Tags.xml";
            XmlDocument _doc = new XmlDocument();
            _doc.Load(strPath);
            TestBrands(_search, _doc);
            TestColours(_search, _doc);
            TestBrandNotInXML(_search, _doc);//Test user inputs not in the XML file
            TestBrandDiffCases(_search, _doc);//User entering with different letter cases
            System.Console.ReadKey();

            /* End of Condition Testing */
            
            /* Semantic Testing */

            //small red dress -> small Navy Shorts -> small Black Shorts
            System.Console.WriteLine("web service: I want a small Black Shorts");
            WebServiceTester wst = new WebServiceTester();
            wst.TestString("I want a small Black Shorts");

            System.Console.WriteLine("SQL: I want a small Black Shorts");
            SQLTester sql = new SQLTester();
            sql.TestSQLColourSize("Black", "S", "Shorts");


            //I want blue pants -> blue Shorts
            //-> I want Blue Shorts
            System.Console.WriteLine("web service: I want Blue Shorts");
            wst.TestString("I want Blue Shorts");
            System.Console.WriteLine("SQL: I want Blue Shorts");
            sql.TestSQLColour("Blue", "Shorts");

            //I want a size 12 tartan top

            System.Console.WriteLine("web service: I want a size 12 Tartan Shorts");
            wst.TestString("I want a size 12 Tartan Shorts");
            System.Console.WriteLine("SQL: I want a size 12 tartan top");
            sql.TestSQLColourSize("Tartan", "12", "Shorts");

            //I totally want a pair of blue pants, but not those trashy navy ones, I want like the cutest baby blue pair of pants ever
            System.Console.WriteLine("web service: blue pants");
            wst.TestString("I totally want a pair of Blue Shorts, but not those trashy Navy ones, I want like the cutest Baby Blue pair of Shorts ever");
            System.Console.WriteLine("SQL: blue pants");
            sql.TestSQLColourNOT("Blue", "Navy", "Shorts");

            /* End of Semantic Testing */
        }

        static void TestBrands(IMWSearchingEngineInterface _search, XmlDocument _doc)
        {
            List<string> fileText = new List<string>();//storing outputs data
            IList<string> brandTags = new List<string>() as IList<string>;//storing brands in XML file

            XmlNodeList nl = _doc.SelectNodes("//Tags/Brands/Brand");

            foreach (XmlNode n in nl)
            {
                brandTags.Add(n.Attributes.GetNamedItem("value").Value);
            }
            foreach (String brand in brandTags)
            {
                int stringLength;
                if (brand.Length % 2 == 0)
                    stringLength = brand.Length / 2;
                else
                    stringLength = brand.Length / 2 + 1;//the length of users' inputs

                string temp = brand.Substring(0, stringLength);

                IList<string> test = _search.AutoCompletion(temp);//calling the function

                System.Console.WriteLine("INPUT :" + temp);
                System.Console.WriteLine("SUPPOSE TO BE :" + brand);
                System.Console.Write("OUTPUT :");

                int flag = 6;//parameter to decide if the test is PASS, WARNING or FAIL. 1 for PASS, 0 for WARNING and all others for FAIL.
                string result = null;//variable to store the test results

                foreach (string t in test)
                {
                    if (test[0].ToString().Equals(brand))
                        flag = 1;
                    else if (t.Equals(brand))
                        flag = 0;
                    System.Console.Write(String.Format("{0, -15}", t.ToString()));
                    result += (t.ToString() + ", ");
                }

                result = result.Substring(0, result.Length - 2);//elimite the last comma

                if (flag == 1)
                {
                    System.Console.WriteLine("PASS");
                    fileText.Add("{Method: auto; input: " + temp + "; desire: " + brand + "; output: [" + result + "]; PASS;}");
                }
                else if (flag == 0)
                {
                    System.Console.WriteLine("WARNING");
                    fileText.Add("{Method: auto; input: " + temp + "; desire: " + brand + "; output: [" + result + "]; WARNING;}");

                }
                else
                {
                    System.Console.WriteLine("FAIL");
                    fileText.Add("{Method: auto; input: " + temp + "; desire: " + brand + "; output: [" + result + "]; FAIL;}");
                }
                System.Console.WriteLine();
                System.Console.WriteLine();

                System.IO.File.WriteAllLines(Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory) + @"\brands.txt", fileText);
            }

        }

        //Same procedures as above
        static void TestColours(IMWSearchingEngineInterface _search, XmlDocument _doc)
        {
            List<string> fileText = new List<string>();
            IList<string> colorTags = new List<string>() as IList<string>;

            XmlNodeList nl = _doc.SelectNodes("//Tags/Colors/Color");

            foreach (XmlNode n in nl)
            {
                colorTags.Add(n.Attributes.GetNamedItem("value").Value);
            }
            foreach (String color in colorTags)
            {
                int stringLength;
                if (color.Length % 2 == 0)
                    stringLength = color.Length / 2;
                else
                    stringLength = color.Length / 2 + 1;
                string tmpcolor = color.Replace("%2f", "|");
                tmpcolor = color.Replace("+", " ");
                string temp = tmpcolor.Substring(0, stringLength);
                IList<string> test = _search.AutoCompletion(temp);
                System.Console.WriteLine("INPUT :" + temp);
                System.Console.WriteLine("SUPPOSE TO BE :" + tmpcolor);
                System.Console.Write("OUTPUT :");

                int flag = 6;
                string result = null;

                foreach (string t in test)
                {
                    if (test[0].ToString().Equals(tmpcolor))
                        flag = 1;
                    else if (t.Equals(tmpcolor))
                        flag = 0;
                    System.Console.Write(String.Format("{0, -15}", t.ToString()));
                    result += (t.ToString() + ", ");
                }

                result = result.Substring(0, result.Length - 2);

                if (flag == 1)
                {
                    System.Console.WriteLine("PASS");
                    fileText.Add("{Method: auto; input: " + temp + "; desire: " + tmpcolor + "; output: [" + result + "]; PASS;}");
                }
                else if (flag == 0)
                {
                    System.Console.WriteLine("WARNING");
                    fileText.Add("{Method: auto; input: " + temp + "; desire: " + tmpcolor + "; output: [" + result + "]; WARNING;}");
                }
                else
                {
                    System.Console.WriteLine("FAIL");
                    fileText.Add("{Method: auto; input: " + temp + "; desire: " + tmpcolor + "; output: [" + result + "]; FAIL;}");
                }

                System.Console.WriteLine();
                System.Console.WriteLine();

                System.IO.File.WriteAllLines(Environment.GetFolderPath(
            Environment.SpecialFolder.DesktopDirectory) + @"\colors.txt", fileText);
            }
        }

        static void TestBrandDiffCases(IMWSearchingEngineInterface _search, XmlDocument _doc)
        {
            System.Console.WriteLine("INPUT :Ni");
            System.Console.WriteLine("SUPPOSE TO BE :Nike");
            IList<string> test = _search.AutoCompletion("Ni");
            System.Console.WriteLine("OUTPUT :");
            foreach (string t in test)
            {
                System.Console.WriteLine("  " + String.Format("{0, -15}", t.ToString()));
            }

            System.Console.WriteLine();
            System.Console.WriteLine("INPUT :ni");
            System.Console.WriteLine("SUPPOSE TO BE :Nike");
            test = _search.AutoCompletion("ni");
            System.Console.WriteLine("OUTPUT :");
            foreach (string t in test)
            {
                System.Console.WriteLine("  " + String.Format("{0, -15}", t.ToString()));
            }
        }

        static void TestBrandNotInXML(IMWSearchingEngineInterface _search, XmlDocument _doc)
        {
            System.Console.WriteLine("INPUT :za");
            System.Console.WriteLine("SUPPOSE TO BE :zara");
            IList<string> test = _search.AutoCompletion("za");
            System.Console.WriteLine("OUTPUT :");
            foreach (string t in test)
            {
                System.Console.WriteLine("  " + String.Format("{0, -15}", t.ToString()));
            }
        }
    }


}
