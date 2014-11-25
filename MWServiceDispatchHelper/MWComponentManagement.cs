/**
 * Chains of responsibility
 * Create by Alfred Yang
 * 31-8-2013
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWServiceDispatchHelper
{
    class MWComponentManagement
    {
        private List<MWAbstrctComponent> _list = new List<MWAbstrctComponent>();

        public void PushComponent(MWAbstrctComponent component)
        {
            _list.Add(component);
        }

        public override string ToString()
        {
            string reVal = null;
            foreach (var v in _list)
                reVal += v.ToString() + "\n";

            return reVal;
        }

        public IList<MWAbstrctComponent> ComponentList { get { return _list; } }
    }
}
