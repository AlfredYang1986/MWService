using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWSearchingEngine;
using MWDataSerilizationType;

namespace UnitTest_MWSearchingEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            IMWSearchingEngineInterface _search =
                MWSearchingEngineFactory.NewInstance();

            IList<Cloth> ls = _search.test();
            foreach (Cloth c in ls)
            {
                System.Console.WriteLine(c.ToString());
            }
        }
    }
}
