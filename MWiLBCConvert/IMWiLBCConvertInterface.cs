using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWiLBCConvert
{
    /************************************************************************/
    /* Factory method for the Dll                                           */
    /************************************************************************/
    public class MWiLBCConvertFactory
    {
        /************************************************************************/
        /* singleton for one Application                                        */
        /************************************************************************/
        static MWiLBCConvertImpl _impl = new MWiLBCConvertImpl();

        public static IMWiLBCConvertInterface NewInstance()
        {
            return _impl;
        }
    }

    public interface IMWiLBCConvertInterface : IDisposable
    {
        Boolean caf2wav(string sourceName, string desName);
    }
}
