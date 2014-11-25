using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWTreap;

namespace UnitTest_MWAnalysisUserInput
{
    class Program
    {
        static void test(string strInput)
        {
            Dictionary<string, IList<string>> searchArgs
                = new Dictionary<string, IList<string>>();
            Dictionary<string, IList<string>> not_searchArgs
                = new Dictionary<string, IList<string>>();

            List<string> orderBy = new List<string>();
            List<string> orderByDesending = new List<string>();

            string search_error = string.Empty;

            MWTreap.MWTreapFactory.SplitResult sr;
            using (var tf = MWTreapFactory.NewInstance())
            {
                sr = tf.splitUserInput(strInput, ref search_error, ref searchArgs, ref not_searchArgs, ref orderBy, ref orderByDesending);
            }

            if (sr != MWTreapFactory.SplitResult.error)
            {
                System.Console.WriteLine(search_error);
            }

            foreach (var arg in searchArgs)
            {
                System.Console.Write(arg.Key + ": ");

                foreach (var v in arg.Value)
                {
                    System.Console.Write(v + " ");
                }
            }
        }

        static void Main(string[] args)
        {
            test(@"jack wi");
            test(@"jackwi");
            test(@"ni k");
            test(@"Fred Perry Laurel Wreath");
            System.Console.ReadLine();
        }
    }
}
