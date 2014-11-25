using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWServiceDispatchHelper;

namespace UnitTest_MWServiceDespatchHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IMWXmlPhraseInterface helper = MWXmlPhraseInterfaceFactory.NewInstance())
            {
                System.Console.WriteLine(helper.getRequestInterface("Searching"));
                System.Console.WriteLine(helper.getRequestInterface("Details"));
                System.Console.WriteLine(helper.getRequestInterface("CategoryByBrand"));
                System.Console.WriteLine(helper.getRequestInterface("BrandByCategory"));
                System.Console.WriteLine(helper.getRequestInterface("AutoCompletion"));
                System.Console.WriteLine(helper.getRequestInterface("Compare"));
                System.Console.WriteLine(helper.getRequestInterface("QuickRecommandation"));
            }
            while (true) ;

        }
    }
}
