using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.SqlClient;

namespace UnitTest_MWServiceQuery
{
    class SQLTester
    {
        //////////////////////////////////////////////////////////////////
        //                                                              //
        //  Get results for the query string from DB w/o web service    //
        //                                                              //
        //////////////////////////////////////////////////////////////////


        //get Context
        public static MWDataEntity.MWDBEntities db = new MWDataEntity.MWDBEntities();


        public void TestSQLColour(string colour, string item)
        {       

            /**************************
             *  By Color  and category*
             *************************/


            //var dressByColor = from a in db.mw_abstract_item.ToList() 
            //                   join b in db.mw_category.ToList() on a.category_id equals b.category_id
            //                   where a.color.ToString().Contains(colour) && b.category_name == item 
            //                   select a;

            //int numberOfLinqResults = 0;

            //foreach (var dress in dressByColor)
            //{
            //    numberOfLinqResults++;
            //    System.Console.WriteLine("No." + numberOfLinqResults + " " + dress.color + ": " + dress.title + ": " + dress.brand_id);
            //}

            //System.Console.Read();

        }

        public void TestSQLColourNOT(string colour, string NOT_colour, string item)
        {

            //get Context


            /******************************************
             *  By Color  and category and Negation  *
             ****************************************/


            //var dressByColor = from a in db.mw_abstract_item.ToList()
            //                   join b in db.mw_category.ToList() on a.category_id equals b.category_id
            //                   where a.color.ToString().Contains(colour) && b.category_name == item && !a.color.ToString().Contains(NOT_colour)
            //                   select a;

            //int numberOfLinqResults = 0;

            //foreach (var dress in dressByColor)
            //{
            //    numberOfLinqResults++;
            //    System.Console.WriteLine("No." + numberOfLinqResults + " " + dress.color + ": " + dress.title + ": " + dress.brand_id);
            //}

            //System.Console.Read();
            //System.Console.Read();
            //System.Console.Read();

       
        }

        public void TestSQLColourSize(string colour, string size, string item)
        {
 
            /*************************************
             *  By Color  and category and size  *
             *************************************/


            //var dressByColor = from a in db.mw_active_item.ToList()
            //                   join b in db.mw_abstract_item.ToList() on a.abstract_item_id equals b.abstract_item_id
            //                   join c in db.mw_category.ToList() on b.category_id equals c.category_id
            //                   where b.color.ToString().Contains(colour) && c.category_name == item && a.size_val == size
            //                   select b;

            //int numberOfLinqResults = 0;

            //foreach (var dress in dressByColor)
            //{
            //    numberOfLinqResults++;
            //    System.Console.WriteLine("No." +numberOfLinqResults + " " + dress.color + ": " + dress.title + ": " + dress.brand_id);
            //}

            //System.Console.Read();

        }
    }
}
