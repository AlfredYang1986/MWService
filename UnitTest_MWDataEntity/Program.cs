using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWDataEntity;
using MWDataSerilizationType;

namespace UnitTest_MWDataEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            IMWDataEntityInterface _mwdb =
                MWDataEntityFactory.NewInstance();

            IList<MWDataSerilizationType.Apparel> ls = _mwdb.test();
            foreach (Apparel c in ls)
            {
                System.Console.WriteLine(c.ToString());
            }
            System.Console.Read();
        }
    }
}
