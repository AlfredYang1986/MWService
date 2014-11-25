/************************************************************************/
/* Treap data structure for auto-complete in search                     */
/* Create By Alfred Yang, ByteFusion pty                                */
/* Data: 07-11-13                                                       */
/* In one A330, way back to see my family                               */
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MWTreap
{
    class TreapNode
    {
        private TreapNode _parent = null;
        private TreapNode _left = null;
        private TreapNode _right = null;
        private int _ref = 0;//访问的次数
        private string _label = null;
        private string _category = null;

        public TreapNode Parent { get { return _parent; } set { _parent = value; } }
        public TreapNode LeftChild { get { return _left; } set { _left = value; } }
        public TreapNode RightChild { get { return _right; } set { _right = value; } }
        public int refCount { get { return _ref; } set { _ref = value; } }
        public string Label { get { return _label; } set { _label = value; } }
        public string Category { get { return _category; } set { _category = value; } }

        public void cloneContent(TreapNode other)
        {
            this.Label = other.Label;
            this.refCount = other.refCount;
        }

        public void refIncreasement()
        {
            ++this._ref;
        }

        public int nodeCount()
        {
            return Regex.Split(_label, @"-").Count();
        }
    }
}
