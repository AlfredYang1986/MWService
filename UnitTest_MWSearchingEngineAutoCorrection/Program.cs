using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWSearchingEngine;
using MWDataSerilizationType;
using System.Xml;

namespace UnitTest_MWSearchingEngineAutoCorrection
{
    class AutoCorrectionTesting
    {
        static void Main(string[] args)
        {
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
