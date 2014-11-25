using System;
using System.Collections.Generic;
using System.Linq;


using MWSearchingEngine;
using MWDataSerilizationType;
using System.Xml;
using System.IO;

namespace UnitTest_MWSearchingEngine
{
    class ConditionTesting
    {

        //public XmlDocument doc;
        //public XmlElement root;
        public static IMWSearchingEngineInterface _search = MWSearchingEngineFactory.NewInstance();

        public IList<String> setUserInput = new List<String>() as IList<String>;
        public static IList<Apparel> ls = new List<Apparel>() as IList<Apparel>;
        public static MWDataEntity.MWDBEntities db = new MWDataEntity.MWDBEntities();

        public static FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Result.txt", FileMode.Append);
        public static StreamWriter sw = new StreamWriter(fs);
        static void Main(string[] args)
        {


            XmlDocument doc = new XmlDocument();
            doc.Load(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\color.xml");
            XmlElement root = doc.DocumentElement;
            IList<String> colorTages = new List<String>() as IList<String>;
            IList<String> brandTages = new List<String>() as IList<String>;

            //Add colors to colorTages
            XmlNodeList n2 = doc.SelectNodes("//colors/color");

            foreach (XmlNode n in n2)
            {
                colorTages.Add(n.Attributes.GetNamedItem("value").Value);
            }

            doc.Load(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Brand.xml");
            //Add brands to brandTages
            n2 = doc.SelectNodes("//Brands/Brand");
            foreach (XmlNode n in n2)
            {
                brandTages.Add(n.Attributes.GetNamedItem("value").Value);
            }


              //////////////////////////////
             //      Different Tests     //
            //////////////////////////////

               // TestSingleColor(colorTages);
                //  TestCombinColor(colorTages);
                // TestNaveSingleColor(colorTages);
                //  TestCombinNaveColor(colorTages);
                //  TestBrandColorNegation(colorTages, brandTages);
                // TestSingleBrand(brandTages);
                // TestConbinationBrands(brandTages);
                //TestNegativeBrand(brandTages);
              //  TestConbinationBrandColor(brandTages, colorTages);

              /////////////////////////////
             //       End of Tests      //
            /////////////////////////////

            System.Console.ReadLine();

        }
        private static void TestSingleColor(IList<String> colorTages)
        {

            //bool check = true;
            //int countF = 0, countS = 0, fileId = 1;
            //string result;

            ////method 
            //foreach (String color in colorTages)
            //{
            //    check = true;

            //    //Color == colorfrist(fisrt character) + colorelse(remainging characters)
            //    string colorFrist = color.Substring(0, 1);
            //    string colorElse = color.Substring(1, color.Length - 1);

            //    //Using Web Server test
            //    //for captial "Red"
            //    // ls = _search.SearchingWithUserInput(colorFrist.ToUpper() + colorElse);
            //    //for lowercase "red"
            //    ls = _search.SearchingWithUserInput(color).Results;

            //    //SQL testing with "RED"
            //    var result2 = from a in db.mw_active_item.ToList()
            //                  join b in db.mw_abstract_item on a.abstract_item_id equals b.abstract_item_id
            //                  where b.color.ToUpper().Equals(color.ToUpper())
            //                  select a.active_item_id;

            //    //NUmber of outcomes from web service against sql
            //    if (ls.Count == result2.ToList().Count)
            //    {
            //        //checking if exactly the same results from sql and web service
            //        for (int i = 0; i < ls.Count; i++)
            //        {
            //            if (!ls.ToList()[i].ItemID.Equals(result2.ToList()[i]))
            //            {
            //                check = false;
            //                break;
            //            }
            //        }
            //    }//if not the same number of outcomes
            //    else
            //        check = false;

            //    //Testing success or failure
            //    if (!check)
            //    {

            //        Console.ForegroundColor = ConsoleColor.Cyan;
            //        System.Console.Write(color);
            //        Console.ForegroundColor = ConsoleColor.Blue;
            //        System.Console.Write(" condition searching  ");
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        System.Console.Write("Fail");


            //        //Reporting to Result.txt

            //        //   result = "{Method:SearchingWithUserInput; Input:[" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Fail; }";

            //        result = "{Method:SearchingWithUserInput; Input:[" + color + "]; Output:[ ]; Fail; }";
            //        sw.WriteLine(result);
            //        fileId++;
            //        countF++;
            //    }
            //    else
            //    {
            //        Console.ForegroundColor = ConsoleColor.Cyan;
            //        System.Console.Write(color);
            //        Console.ForegroundColor = ConsoleColor.Blue;
            //        System.Console.Write(" condition searching  ");
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        System.Console.WriteLine("succeed");


            //        result = "{Method:SearchingWithUserInput; Input:[" + color + "]; Output:[ ]; Success; }";

            //        // result = "{Method:SearchingWithUserInput; Input:[" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Success; }";
            //        sw.WriteLine(result);

            //        fileId++;
            //        countS++;
            //    }


            //}
            //sw.Close();
            //fs.Close();
            //System.Console.WriteLine();
            //Console.ForegroundColor = ConsoleColor.White;
            //System.Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

            //Console.ForegroundColor = ConsoleColor.Green;
            //System.Console.Write("success rate is ");
            //Console.ForegroundColor = ConsoleColor.Magenta;
            //System.Console.WriteLine((countS / (countF + countS)) * 100 + "%");



        }


        //private static void TestCombinColor(IList<String> colorTages)
        //{
        //    bool check1 = true;
        //    int countF = 0, countS = 0, fileid = 1;
        //    string result;
        //    int i = 100;
        //    Random runmber = new Random();
        //    int number1, number2;


        //    while (i >= 0)
        //    {

        //        do
        //        {
        //            number1 = runmber.Next(1, 147);
        //            number2 = runmber.Next(1, 147);
        //        } while (number1 == number2);

        //        //method
        //        string colorFrist = colorTages[number1 - 1].Substring(0, 1);
        //        string colorElse = colorTages[number1 - 1].Substring(1, colorTages[number1 - 1].Length - 1);

        //        string colorFrist2 = colorTages[number2 - 1].Substring(0, 1);
        //        string colorElse2 = colorTages[number2 - 1].Substring(1, colorTages[number2 - 1].Length - 1);

        //        string color = colorFrist.ToUpper() + colorElse + " " + colorFrist2.ToUpper() + colorElse2;

        //        //  ls = _search.SearchingWithUserInput(color);

        //        ls = _search.SearchingWithUserInput(colorTages[number1 - 1] + " " + colorTages[number2 - 1]);

        //        var result2 = from a in db.mw_active_item.ToList()
        //                      join b in db.mw_abstract_item on a.abstract_item_id equals b.abstract_item_id
        //                      where b.color.ToUpper().Equals(colorTages[number1 - 1].ToUpper()) ||
        //                            b.color.ToUpper().Equals(colorTages[number2 - 1].ToUpper())
        //                      select a.active_item_id;

        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int j = 0; j < ls.Count; j++)
        //            {
        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check1 = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check1 = false;
        //        if (!check1)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(color);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;

        //            System.Console.WriteLine("Fail");

        //            result = "{Method:SearchingWithUserInput; Input:[" + colorTages[number1 - 1] + "," + colorTages[number2 - 1] + "]; Output:[ ]; Fail; }";
        //            //  result = "{Method:SearchingWithUserInput; Input:[" + colorFrist.ToUpper() + colorElse + "," + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Fail; }";
        //            sw.WriteLine(result);
        //            fileid++;
        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(color);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");

        //            result = "{Method:SearchingWithUserInput; Input:[" + colorTages[number1 - 1] + "," + colorTages[number2 - 1] + "]; Output:[ ]; Success; }";
        //            //  result = "{Method:SearchingWithUserInput; Input:[" + colorFrist.ToUpper() + colorElse + "," + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Success; }";
        //            sw.WriteLine(result);


        //            countS++;
        //        }
        //        i--;

        //    }
        //    sw.Close();
        //    fs.Close();
        //}



        //private static void TestNaveSingleColor(IList<String> colorTages)
        //{
        //    int countF = 0, countS = 0;
        //    bool check1 = false;
        //    string result;
        //    //method 

        //    foreach (String color in colorTages)
        //    {
        //        string colorFrist = color.Substring(0, 1);
        //        string colorElse = color.Substring(1, color.Length - 1);

        //        //  ls = _search.SearchingWithUserInput("-" + color);

        //        ls = _search.SearchingWithUserInput("-" + colorFrist.ToUpper() + colorElse);

        //        var result2 = from a in db.mw_active_item.ToList()
        //                      join b in db.mw_abstract_item on a.abstract_item_id equals b.abstract_item_id
        //                      where (!(b.color.ToUpper().Equals(color.ToUpper())))
        //                      select a.active_item_id;

        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int j = 0; j < ls.Count; j++)
        //            {
        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check1 = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check1 = false;
        //        if (!check1)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(color);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");

        //            result = "{Method:SearchingWithUserInput; Input:[-" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Fail; }";

        //            //   result = "{Method:SearchingWithUserInput; Input:[-"+color+ "]; Output:[ ]; Fail; }";
        //            sw.WriteLine(result);

        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(color);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");

        //            result = "{Method:SearchingWithUserInput; Input:[-" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Success; }";

        //            //   result = "{Method:SearchingWithUserInput; Input:[-" + color + "]; Output:[ ]; Success; }";
        //            sw.WriteLine(result);


        //            countS++;
        //        }


        //    }
        //    sw.Close();
        //    fs.Close();

        //}

        //private static void TestCombinNaveColor(IList<String> colorTages)
        //{
        //    bool check1 = true;
        //    int countF = 0, countS = 0;
        //    string result = null;
        //    int i = 100;
        //    Random runmber = new Random();
        //    int number1, number2;
        //    string input;

        //    System.Console.WriteLine("1.fiste color nagetive");
        //    System.Console.WriteLine("2.last color nagetive");
        //    System.Console.WriteLine("3.both color nagetive");
        //    System.Console.WriteLine("Please select one:");
        //    input = Console.ReadLine();

        //    while (i >= 0)
        //    {
        //        // bool check = false;
        //        do
        //        {
        //            number1 = runmber.Next(1, 147);
        //            number2 = runmber.Next(1, 147);
        //        } while (number1 == number2);

        //        //method
        //        string colorFrist = colorTages[number1 - 1].Substring(0, 1);
        //        string colorElse = colorTages[number1 - 1].Substring(1, colorTages[number1 - 1].Length - 1);

        //        string colorFrist2 = colorTages[number2 - 1].Substring(0, 1);
        //        string colorElse2 = colorTages[number2 - 1].Substring(1, colorTages[number2 - 1].Length - 1);


        //        string upperCaseColor11 = colorFrist.ToUpper() + colorElse + " " + colorFrist2.ToUpper() + colorElse2;
        //        string upperCaseColor01 = "-" + colorFrist.ToUpper() + colorElse + " " + colorFrist2.ToUpper() + colorElse2;
        //        string upperCaseColor10 = colorFrist.ToUpper() + colorElse + " -" + colorFrist2.ToUpper() + colorElse2;
        //        string upperCaseColor00 = "-" + colorFrist.ToUpper() + colorElse + " -" + colorFrist2.ToUpper() + colorElse2;

        //        string lowerCaseColor11 = colorTages[number1 - 1] + colorTages[number2 - 1];
        //        string lowerCaseColor01 = "-" + colorTages[number1 - 1] + " " + colorTages[number2 - 1];
        //        string lowerCaseColor10 = colorTages[number1 - 1] + " -" + colorTages[number2 - 1];
        //        string lowerCaseColor00 = "-" + colorTages[number1 - 1] + " -" + colorTages[number2 - 1];
        //        var result2 = from a in db.mw_active_item.ToList()
        //                      where a.active_item_id.Equals("1")
        //                      select a.abstract_item_id;

        //        switch (input)
        //        {
        //            case "1":

        //                ls = _search.SearchingWithUserInput(upperCaseColor01);

        //                //   ls = _search.SearchingWithUserInput(lowerCaseColor01);
        //                result2 = from a in db.mw_active_item.ToList()
        //                          join b in db.mw_abstract_item on a.abstract_item_id equals b.abstract_item_id
        //                          where (!(b.color.ToUpper().Contains(colorTages[number1 - 1].ToUpper()))) &&
        //                                b.color.ToUpper().Contains(colorTages[number2 - 1].ToUpper())
        //                          select a.active_item_id;
        //                break;
        //            case "2":

        //                ls = _search.SearchingWithUserInput(upperCaseColor10);

        //                // ls = _search.SearchingWithUserInput(lowerCaseColor10);
        //                result2 = from a in db.mw_active_item.ToList()
        //                          join b in db.mw_abstract_item on a.abstract_item_id equals b.abstract_item_id
        //                          where b.color.ToUpper().Contains(colorTages[number1 - 1].ToUpper()) &&
        //                               (!(b.color.ToUpper().Contains(colorTages[number2 - 1].ToUpper())))
        //                          select a.active_item_id;
        //                break;
        //            case "3":

        //                ls = _search.SearchingWithUserInput(upperCaseColor00);

        //                //  ls = _search.SearchingWithUserInput(lowerCaseColor00);
        //                result2 = from a in db.mw_active_item.ToList()
        //                          join b in db.mw_abstract_item on a.abstract_item_id equals b.abstract_item_id
        //                          where (!(b.color.ToUpper().Contains(colorTages[number1 - 1].ToUpper()))) &&
        //                               (!(b.color.ToUpper().Contains(colorTages[number2 - 1].ToUpper())))
        //                          select a.active_item_id;

        //                break;
        //        }

        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int j = 0; j < ls.Count; j++)
        //            {
        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check1 = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check1 = false;
        //        if (!check1)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(upperCaseColor11);
        //            //  System.Console.Write(lowerCaseColor11);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");
        //            switch (input)
        //            {
        //                case "1":

        //                    //   result = "{Method:SearchingWithUserInput; Input:[-" + colorTages[number1 - 1] + ","+colorTages[number2 - 1]+"]; Output:[ ]; Fail; }";

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + colorFrist.ToUpper() + colorElse + "," + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Fail; }";
        //                    break;
        //                case "2":

        //                    //   result = "{Method:SearchingWithUserInput; Input:[" + colorTages[number1 - 1] + ", -"+colorTages[number2 - 1]+"]; Output:[ ]; Fail; }";

        //                    result = "{Method:SearchingWithUserInput; Input:[" + colorFrist.ToUpper() + colorElse + ", -" + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Fail; }";
        //                    break;
        //                case "3":

        //                    //   result = "{Method:SearchingWithUserInput; Input:[-" + colorTages[number1 - 1] + ", -"+colorTages[number2 - 1]+"]; Output:[ ]; Fail; }";

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + colorFrist.ToUpper() + colorElse + ", -" + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Fail; }";
        //                    break;
        //            }

        //            sw.WriteLine(result);
        //            //  fileid++;
        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(upperCaseColor11);
        //            // System.Console.Write(lowerCaseColor11);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");
        //            switch (input)
        //            {
        //                case "1":

        //                    //    result = "{Method:SearchingWithUserInput; Input:[-" + colorTages[number1 - 1] + ","+colorTages[number2 - 1]+"]; Output:[ ]; Success; }";

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + colorFrist.ToUpper() + colorElse + "," + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Success; }";
        //                    break;
        //                case "2":

        //                    //   result = "{Method:SearchingWithUserInput; Input:[" + colorTages[number1 - 1] + ", -"+colorTages[number2 - 1]+"]; Output:[ ]; Success; }";

        //                    result = "{Method:SearchingWithUserInput; Input:[" + colorFrist.ToUpper() + colorElse + ", -" + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Success; }";
        //                    break;
        //                case "3":

        //                    //   result = "{Method:SearchingWithUserInput; Input:[-" + colorTages[number1 - 1] + ", -"+colorTages[number2 - 1]+"]; Output:[ ]; Success; }";

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + colorFrist.ToUpper() + colorElse + ", -" + colorFrist2.ToUpper() + colorElse2 + "]; Output:[ ]; Success; }";
        //                    break;
        //            }

        //            sw.WriteLine(result);


        //            countS++;
        //        }
        //        i--;

        //    }
        //    sw.Close();
        //    fs.Close();
        //}

        //private static void TestBrandColorNegation(IList<String> colorTages, IList<String> brandTages)
        //{
        //    int i = 100;
        //    Random runmber = new Random();
        //    int number1, number2;
        //    string input;
        //    bool check1 = true;
        //    int countF = 0, countS = 0;
        //    string result = null;

        //    System.Console.WriteLine("1.brand nagetive");
        //    System.Console.WriteLine("2 color nagetive");
        //    System.Console.WriteLine("3.both nagetive");
        //    System.Console.WriteLine("Please select one:");
        //    input = Console.ReadLine();


        //    while (i >= 0)
        //    {


        //        number1 = runmber.Next(0, 146);
        //        number2 = runmber.Next(0, 184);


        //        //method
        //        string colorFrist = colorTages[number1].Substring(0, 1);
        //        string colorElse = colorTages[number1].Substring(1, colorTages[number1].Length - 1);
        //        string brandFrist = brandTages[number1].Substring(0, 1);
        //        string brandElse = brandTages[number1].Substring(1, brandTages[number1].Length - 1);


        //        string cloth = brandTages[number2] + " " + colorFrist.ToUpper() + colorElse;

        //        string upperCase01 = "-" + brandTages[number2] + " " + colorFrist.ToUpper() + colorElse;
        //        string upperCase10 = brandTages[number2] + " -" + colorFrist.ToUpper() + colorElse;
        //        string upperCase00 = "-" + brandTages[number2] + " -" + colorFrist.ToUpper() + colorElse;

        //        string lowerCase01 = "-" + brandFrist.ToLower() + brandElse + " " + colorTages[number1];
        //        string lowerCase10 = brandFrist.ToLower() + brandElse + " -" + colorTages[number1];
        //        string lowerCase00 = "-" + brandFrist.ToLower() + brandElse + " -" + colorTages[number1];

        //        var result2 = from a in db.mw_active_item.ToList()
        //                      select a.active_item_id;
        //        switch (input)
        //        {
        //            case "1":

        //                ls = _search.SearchingWithUserInput(upperCase01); //upper case

        //                // ls = _search.SearchingWithUserInput(lowerCase01);

        //                result2 = from a in db.mw_active_item.ToList()
        //                          join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                          join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                          where (!c.brand_name.Equals(brandTages[number2])) &&
        //                                 b.color.ToUpper().Equals(colorTages[number1].ToUpper())
        //                          select a.active_item_id;
        //                break;
        //            case "2":

        //                ls = _search.SearchingWithUserInput(upperCase10);//upper case

        //                //  ls = _search.SearchingWithUserInput(lowerCase10);

        //                result2 = from a in db.mw_active_item.ToList()
        //                          join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                          join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                          where c.brand_name.Equals(brandTages[number2]) &&
        //                                (!b.color.ToUpper().Equals(colorTages[number1].ToUpper()))
        //                          select a.active_item_id;
        //                break;
        //            case "3":

        //                ls = _search.SearchingWithUserInput(upperCase00);//upper case

        //                // ls = _search.SearchingWithUserInput(lowerCase00);

        //                result2 = from a in db.mw_active_item.ToList()
        //                          join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                          join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                          where (!c.brand_name.Equals(brandTages[number2])) &&
        //                                (!b.color.ToUpper().Equals(colorTages[number1].ToUpper()))
        //                          select a.active_item_id;
        //                break;
        //        }


        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int j = 0; j < ls.Count; j++)
        //            {
        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check1 = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check1 = false;
        //        if (!check1)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(cloth);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");
        //            switch (input)
        //            {
        //                case "1":

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + brandTages[number2] + "," + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Fail; }";

        //                    //result = "{Method:SearchingWithUserInput; Input:[-" +brandTages[number2].ToLower()+","+colorTages[number1]+ "]; Output:[ ]; Fail; }";
        //                    break;
        //                case "2":

        //                    result = "{Method:SearchingWithUserInput; Input:[" + brandTages[number2] + ", -" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Fail; }";

        //                    //result = "{Method:SearchingWithUserInput; Input:[" + brandTages[number2].ToLower() + ", -" + colorTages[number1] + "]; Output:[ ]; Fail; }";
        //                    break;
        //                case "3":

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + brandTages[number2] + ", -" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Fail; }";

        //                    // result = "{Method:SearchingWithUserInput; Input:[-" + brandTages[number2].ToLower() + ", -" + colorTages[number1] + "]; Output:[ ]; Fail; }";
        //                    break;
        //            }

        //            sw.WriteLine(result);


        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(cloth);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");
        //            switch (input)
        //            {
        //                case "1":

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + brandTages[number2] + "," + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Success; }";

        //                    //result = "{Method:SearchingWithUserInput; Input:[-" +brandTages[number2].ToLower()+","+colorTages[number1]+ "]; Output:[ ]; Success; }";
        //                    break;
        //                case "2":

        //                    result = "{Method:SearchingWithUserInput; Input:[" + brandTages[number2] + ", -" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Success; }";

        //                    //  result = "{Method:SearchingWithUserInput; Input:[" + brandTages[number2].ToLower() + ", -" + colorTages[number1] + "]; Output:[ ]; Success; }";
        //                    break;
        //                case "3":

        //                    result = "{Method:SearchingWithUserInput; Input:[-" + brandTages[number2] + ", -" + colorFrist.ToUpper() + colorElse + "]; Output:[ ]; Success; }";

        //                    //  result = "{Method:SearchingWithUserInput; Input:[-" + brandTages[number2].ToLower() + ", -" + colorTages[number1] + "]; Output:[ ]; Sucess; }";
        //                    break;
        //            }

        //            sw.WriteLine(result);



        //            countS++;
        //        }

        //        i--;
        //    }

        //}

        //private static void TestSingleBrand(IList<string> brandTages)
        //{
        //    bool check = true;
        //    int countF = 0, countS = 0;
        //    string result, lowerCaseB;
        //    foreach (string brand in brandTages)
        //    {
        //        //lowercae
        //        string colorFrist = brand.Substring(0, 1);
        //        string colorElse = brand.Substring(1, brand.Length - 1);
        //        lowerCaseB = colorFrist.ToLower() + colorElse;

        //        ls = _search.SearchingWithUserInput(brand);

        //        // ls = _search.SearchingWithUserInput(lowerCaseB);

        //        var result2 = from a in db.mw_active_item.ToList()
        //                      join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                      join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                      where c.brand_name.ToUpper() == brand.ToUpper()
        //                      select a.active_item_id;

        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int i = 0; i < ls.Count; i++)
        //            {
        //                if (!ls.ToList()[i].ItemID.Equals(result2.ToList()[i]))
        //                {
        //                    check = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check = false;

        //        if (!check)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brand);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");


        //            result = "{Method:SearchingWithUserInput; Input:[" + brand + "]; Output:[ ]; Fail; }";

        //            //  result = "{Method:SearchingWithUserInput; Input:[" + lowerCaseB+ "]; Output:[ ]; Fail; }";
        //            sw.WriteLine(result);

        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brand);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");


        //            result = "{Method:SearchingWithUserInput; Input:[" + brand + "]; Output:[ ]; Success; }";

        //            // result = "{Method:SearchingWithUserInput; Input:[" +lowerCaseB + "]; Output:[ ]; Success; }";
        //            sw.WriteLine(result);


        //            countS++;
        //        }

        //    }
        //    sw.Close();
        //    fs.Close();
        //}

        //private static void TestConbinationBrands(IList<string> brandTages)
        //{


        //    bool check = true;
        //    int countF = 0, countS = 0;
        //    string result, lowerCaseB, lowerCaseB2;
        //    Random ran = new Random();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int choose1, choose2;
        //        do
        //        {
        //            choose1 = ran.Next(185);
        //            choose2 = ran.Next(185);
        //        } while (choose1 == choose2);
        //        string bFrist = brandTages[choose1].Substring(0, 1);
        //        string bElse = brandTages[choose1].Substring(1, brandTages[choose1].Length - 1);
        //        string bFrist2 = brandTages[choose2].Substring(0, 1);
        //        string bElse2 = brandTages[choose2].Substring(1, brandTages[choose2].Length - 1);
        //        lowerCaseB = bFrist.ToLower() + bElse;
        //        lowerCaseB2 = bFrist2.ToLower() + bElse2;

        //        // ls= _search.SearchingWithUserInput(lowerCaseB + " " + lowerCaseB2);

        //        ls = _search.SearchingWithUserInput(brandTages[choose1] + " " + brandTages[choose2]);



        //        var result2 = from a in db.mw_active_item.ToList()
        //                      join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                      join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                      where c.brand_name.ToUpper() == brandTages[choose1].ToUpper() || c.brand_name.ToUpper() == brandTages[choose2].ToUpper()
        //                      select a.active_item_id;



        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int j = 0; j < ls.Count; j++)
        //            {
        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check = false;
        //        if (!check)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brandTages[choose1] + " " + brandTages[choose2]);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");

        //            // result = "{Method:SearchingWithUserInput; Input:[" + lowerCaseB + ","+lowerCaseB2+"]; Output:[ ]; Fail; }";

        //            result = "{Method:SearchingWithUserInput; Input:[" + brandTages[choose1] + "," + brandTages[choose2] + "]; Output:[ ]; Fail; }";
        //            sw.WriteLine(result);

        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brandTages[choose1] + " " + brandTages[choose2]);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");

        //            // result = "{Method:SearchingWithUserInput; Input:[" + lowerCaseB + "," + lowerCaseB2 + "]; Output:[ ]; Success; }";

        //            result = "{Method:SearchingWithUserInput; Input:[" + brandTages[choose1] + "," + brandTages[choose2] + "]; Output:[ ]; Success; }";
        //            sw.WriteLine(result);



        //            countS++;
        //        }

        //    }
        //    sw.Close();
        //    fs.Close();
        //}

        //private static void TestNegativeBrand(IList<string> brandTages)
        //{

        //    bool check = true;
        //    int countF = 0, countS = 0;
        //    string result, lowerCaseB;
        //    foreach (string brand in brandTages)
        //    {
        //        string Bfrist = brand.Substring(0, 1);
        //        string BElse = brand.Substring(1, brand.Length - 1);
        //        lowerCaseB = Bfrist.ToLower() + BElse;
        //        ls = _search.SearchingWithUserInput("-" + brand);

        //        //  ls = _search.SearchingWithUserInput("-" + lowercaseB);

        //        var result2 = from a in db.mw_active_item.ToList()
        //                      join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                      join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                      where c.brand_name != brand
        //                      select a.active_item_id;
        //        if (ls.Count == result2.ToList().Count)
        //        {
        //            for (int j = 0; j < ls.Count; j++)
        //            {
        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check = false;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            check = false;
        //        if (!check)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brand);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");

        //            //  result = "{Method:SearchingWithUserInput; Input:[-" + lowercaseB +"]; Output:[ ]; Fail; }";
        //            result = "{Method:SearchingWithUserInput; Input:[-" + brand + "]; Output:[ ]; Fail; }";

        //            sw.WriteLine(result);

        //            countF++;
        //        }
        //        else
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brand);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");

        //            // result = "{Method:SearchingWithUserInput; Input:[-" + lowercaseB +  "]; Output:[ ]; Success; }";
        //            result = "{Method:SearchingWithUserInput; Input:[-" + brand + "]; Output:[ ]; Success; }";

        //            sw.WriteLine(result);

        //            countS++;
        //        }
        //    }
        //    sw.Close();
        //    fs.Close();
        //}

        //private static void TestConbinationBrandColor(IList<string> brandTages, IList<string> colorTages)
        //{
        //    bool check = true;
        //    int countF = 0, countS = 0;
        //    string result, lowerCaseB, upperC;
        //    Random ran = new Random();
        //    for (int i = 0; i < 100; i++)
        //    {

        //        int choose1, choose2;

        //        choose1 = ran.Next(185);
        //        choose2 = ran.Next(146);
        //        string colorFrist = colorTages[choose2].Substring(0, 1);
        //        string colorElse = colorTages[choose2].Substring(1, colorTages[choose2].Length - 1);
        //        upperC = colorFrist.ToUpper() + colorElse;
        //        string BFrist = brandTages[choose1].Substring(0, 1);
        //        string BElse = brandTages[choose1].Substring(1, brandTages[choose1].Length - 1);
        //        lowerCaseB = BFrist.ToLower() + BElse;

        //        ls = _search.SearchingWithUserInput(brandTages[choose1] + " " + upperC);

        //        //ls = _search.SearchingWithUserInput(lowerCaseB + " " + colorTages[choose2]);


        //        var result2 = from a in db.mw_active_item.ToList()
        //                      join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
        //                      join c in db.mw_brand.ToList() on b.brand_id equals c.brand_id
        //                      where
        //                      c.brand_name.ToUpper().Equals(brandTages[choose1].ToUpper().ToString()) && b.color.ToUpper().Contains(colorTages[choose2].ToUpper())
        //                      select a.active_item_id;
        //        if (ls.Count == result2.ToList().Count)
        //        {

        //            for (int j = 0; j < ls.Count; j++)
        //            {

        //                if (!ls.ToList()[j].ItemID.Equals(result2.ToList()[j]))
        //                {
        //                    check = false;
        //                    break;
        //                }
        //            }

        //            //          ls.ToList().ForEach(x => result2.ToList().Contains(x.ItemID));

        //        }
        //        else
        //        {
        //            check = false;

        //        }

        //        if (!check)
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brandTages[choose1] + " " + colorTages[choose2]);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            System.Console.WriteLine("Fail");
        //            result = "{Method:SearchingWithUserInput; Input:[" + brandTages[choose1] + "," + upperC + "]; Output:[ ]; Fail; }";
        //            // result = "{Method:SearchingWithUserInput; Input:[" + lowerCaseB + "," + colorTages[choose2] + "]; Output:[ ]; Fail; }";
        //            sw.WriteLine(result);

        //            countF++;
        //        }
        //        else
        //        {

        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //            System.Console.Write(brandTages[choose1] + " " + colorTages[choose2]);
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //            System.Console.Write(" condition searching  ");
        //            Console.ForegroundColor = ConsoleColor.Green;
        //            System.Console.WriteLine("succeed");
        //            result = "{Method:SearchingWithUserInput; Input:[" + brandTages[choose1] + "," + upperC + "]; Output:[ ]; Success; }";
        //            //   result = "{Method:SearchingWithUserInput; Input:[" + lowerCaseB + "," + colorTages[choose2] + "]; Output:[ ]; Success; }";
        //            sw.WriteLine(result);


        //            countS++;


        //        }



        //    }
        //    sw.Close();
        //    fs.Close();
        //}

    }

}
