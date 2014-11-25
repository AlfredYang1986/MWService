using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWStylePalette
{
    public class MWErrorMessageGenerator
    {
        public static string UserIdInvalid 
        { 
            get
            {
                return "{ \"Error\":\"UserIdInvalid\" }";
            }
            
        }

        public static string TokenInvalid
        {
            get
            {
                //generate JSON invalidity message
                return "{ \"Error\":\"TokenInvalid\" }";
            }
        }

    }
}
