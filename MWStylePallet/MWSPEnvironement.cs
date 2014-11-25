using MWDataSerilizationType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWStylePalette
{
    class MWSPEnvironement
    {
        public string UserId { get; set; }

        public List<Item> Items = new List<Item>();
    }
}
