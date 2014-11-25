/************************************************************************/
/* Treap data structure for auto-complete in search                     */
/* Create By Alfred Yang, ByteFusion pty                                */
/* Data: 07-11-13                                                       */
/* In one A330, way back to see my family                               */
/************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MWTreap
{
    /************************************************************************/
    /* The Treap is a data struct that have both binary                     */
    /* tree and Max Heap feathers                                           */
    /* Or Treap also can be seen as a priority queue                        */
    /* for the user search query                                            */
    /************************************************************************/
    class Treap
    {
        /************************************************************************/
        /* In order to conquer read-write problem in concurrency service        */
        /* Alfred decided to use copy-and-swap to conquer race condition        */
        /* use _root as the sige node, and all the read happens in left child   */
        /* use _root as the sige node, and all the write happens in left child  */
        /************************************************************************/
        private TreapNode _root = null;

        public TreapNode Root { get { return _root; } set { _root = value; } }

        private Boolean _dirtyBit = false;

        public Treap()
        {
            _root = new TreapNode();
        }

        public void InsertLabel(string strLabel, int refCount, string strCategory)
        {
            TreapNode temp_node = new TreapNode();
            temp_node.Label = strLabel;
            temp_node.refCount = refCount;
            temp_node.Category = strCategory;
            InsertLabel(Root.LeftChild, temp_node);
        }

        /************************************************************************/
        /* The binary tree insertion                                            */
        /* Use the alphabet sorting the treapNode with label                    */
        /* After insertion, use rotation to contruct the heap in the tree       */
        /* because on the service, it is not wise to use recursive              */
        /************************************************************************/
        private void InsertLabel(TreapNode rootNode, TreapNode node_t)
        {
            TreapNode begin = rootNode;
            if (begin == null)
            {
                _root.LeftChild = node_t;
                return;
            }

            TreapNode temp = null;
            while (begin != null)
            {
                if (begin.Label == node_t.Label)
                    break;

                else if (Helper_Functions.Str_Compare(begin.Label, node_t.Label) > 0)
                {
                    temp = begin.LeftChild;
                    if (temp == null)
                    {
                        begin.LeftChild = node_t;
                        begin.LeftChild.Parent = begin;
                        this.HeapConstruct(node_t);
                        break;
                    }
                }

                else
                {
                    temp = begin.RightChild;
                    if (temp == null)
                    {
                        begin.RightChild = node_t;
                        begin.RightChild.Parent = begin;
                        this.HeapConstruct(node_t);
                        break;
                    }
                }
                begin = temp;
            }
        }

        /************************************************************************/
        /* The binary tree remove                                               */
        /* After delete, use rotation to contruct the heap in the tree          */
        /* because on the service, it is not wise to use recursive              */
        /************************************************************************/
        public void RemoveLabel(string strLabel)
        {
            TreapNode temp = searchTreap(Root.LeftChild, strLabel);
            if (temp != null)
            {
                /**
                 * 1. only get left child
                 * 2. only get right child
                 * can directly replace the temp nodes with the node its child
                 */
                if ((temp.LeftChild != null && temp.RightChild == null)
                   || (temp.LeftChild == null && temp.RightChild != null))
                {
                    TreapNode parent = temp.Parent;
                    TreapNode replace = temp.LeftChild ==
                        null ? temp.RightChild : temp.LeftChild;

                    replace.Parent = parent;
                    parent = parent.LeftChild == temp
                        ? parent.LeftChild = replace
                        : parent.RightChild = replace;
                }
                /**
                 * 3. get both left and right children
                 * find the smallest successor, replace the node
                 */
                else if (temp.LeftChild != null && temp.RightChild != null)
                {
                    TreapNode replace = searchTreapMin(temp.RightChild);
                    temp.Label = replace.Label;
                    temp.refCount = replace.refCount;

                    if (replace.RightChild != null)
                        replace.RightChild.Parent = replace.Parent;
                    replace = replace.Parent.LeftChild == replace ?
                        replace.Parent.LeftChild = replace.RightChild :
                        replace.Parent.RightChild = replace.RightChild;
                }
                /**
                 * 4. have neither left nor right children
                 */
                else
                {
                    TreapNode parent = temp.Parent;
                    parent = parent.LeftChild == temp
                            ? parent.LeftChild = null
                            : parent.RightChild = null;
                }
            }
        }

        public void touchLabel(string strLabel)
        {
            TreapNode temp = searchTreap(_root.LeftChild, strLabel);
            if (temp != null)
            {
                touchLabel(temp);
            }
        }

        private void touchLabel(TreapNode node_t)
        {
            lock (this)
            {
                _dirtyBit = true;
                node_t.refIncreasement();
            }
        }

        /************************************************************************/
        /* for get the top n node from the treap                                */
        /* the edit distance keeps the similarity                               */
        /* the property of heap make sure hottest search                        */
        /* use greedy search: hueristic function is edit_distance                */
        /* in order to improve the performance, only calculate first half       */
        /* of the treap                                                         */
        /* paramiters                                                           */
        /*      TreapNode: start Node, as root                                  */
        /*      conpareHandler: edit distance, use delegate for extension       */
        /*      count: the count of string that should return to user interface */
        /*      reVal: the concrate return value                                */
        /************************************************************************/
        public class dis_node
        {
            public int ed { get; set; }
            public double hd { get; set; }
            public int cum { get; set; }
            public TreapNode tn { get; set; }
            public Boolean bNot { get; set; }
        }

        public int searchInTreap_greed(List<string> input, TreapNode node, ref LinkedList<dis_node> result)
        {
            if (node == null)
                return 0;

            if (input.Count() == 0)
                return 0;

            int n = node.nodeCount();

            //LinkedList<dis_node> temp_result = new LinkedList<dis_node>();

            for (int temp = 0; temp < input.Count; ++temp)
            {
                string str_temp = string.Empty;
                //依次取得用户输入的条件
                var query = (from str in input
                             select str).Take(temp + 1);


                foreach (var s in query)
                    str_temp += s + '-';



                str_temp = str_temp.Remove(str_temp.Length - 1);

                Boolean bNot = str_temp.StartsWith("!");
                if (bNot)
                    str_temp = str_temp.Substring(1);

                // if contians !, skip
                if (str_temp.Contains('!')) continue;

                result.AddLast(new dis_node
                {
                    ed = Helper_Functions.Edit_Distance(str_temp.ToLower(), node.Label.ToLower()),
                    hd = Helper_Functions.Hamming(str_temp.ToLower(), node.Label.ToLower()) * 1.0 / str_temp.Length,
                    tn = node,
                    cum = temp + 1,
                    bNot = bNot,
                });
            }

            //var temp_query = (from temp in temp_result
            //                  select temp).OrderBy(x => x.ed).OrderByDescending(x => x.hd).OrderByDescending(x => x.cum);

            //foreach (var t in temp_query)
            //{
            //    System.Console.WriteLine(t.tn.Label);
            //    System.Console.WriteLine(t.hd);
            //    System.Console.WriteLine(t.ed);
            //    System.Console.WriteLine(t.cum);
            //    System.Console.WriteLine();
            //}

            searchInTreap_greed(input, node.LeftChild, ref result);
            searchInTreap_greed(input, node.RightChild, ref result);

            return 0;
        }

        public string[] searchTreapWithPriority(string str, TreapNode node,
            Helper_Functions.delegate_Compare comparehandler,
            Helper_Functions.delegate_Compare comparehandler_hd,
            int count)
        {
            /**
             * 1. use linq to create top 100 popular searching 
             */
            LinkedList<dis_node> list = new LinkedList<dis_node>();

            int dis = comparehandler(str.ToUpper(), node.Label.ToUpper());
            int ham = comparehandler_hd(str.ToUpper(), node.Label.ToUpper());
    
            list.AddLast(new dis_node { ed = dis, hd = ham, tn = node });
            var current = list.First;

            //while (current != null && list.Count < 100)
            while (current != null)
            {
                TreapNode left = current.Value.tn.LeftChild;
                TreapNode right = current.Value.tn.RightChild;

                if (left != null)
                {
                    list.AddLast(new dis_node
                    {
                        ed = comparehandler(str.ToUpper(), left.Label.ToUpper()),
                        hd = comparehandler_hd(str.ToUpper(), left.Label.ToUpper()),
                        tn = left
                    });
                }

                if (right != null)
                {
                    list.AddLast(new dis_node
                    {
                        ed = comparehandler(str.ToUpper(), right.Label.ToUpper()),
                        hd = comparehandler_hd(str.ToUpper(), right.Label.ToUpper()),
                        tn = right
                    });
                }


                current = current.Next;
            }

            /**
             * 2. sort it and return the first top count 
             */
            var query = (from top in list
                         orderby top.ed, top.hd descending
                         select top.tn.Label).Take(count);

            return query.ToArray();
        }


        public TreapNode searchTreap(TreapNode rootNode, TreapNode node_t)
        {
            return searchTreap(rootNode, node_t.Label);
        }

        public TreapNode searchTreap(TreapNode rootNode, string strLable)
        {
            TreapNode begin = rootNode;
            while (begin != null)
            {
                if (begin.Label == strLable)
                    break;

                else if (Helper_Functions.Str_Compare(begin.Label, strLable) > 0)
                    begin = begin.LeftChild;

                else
                    begin = begin.RightChild;
            }

            return begin;
        }

        public TreapNode searchTreapMin(TreapNode rootNode)
        {
            TreapNode temp = rootNode;
            while (temp != null)
            {
                if (temp.LeftChild == null)
                    break;

                temp = temp.LeftChild;
            }

            return temp;
        }

        public TreapNode searchTreapMax(TreapNode rootNode)
        {
            TreapNode temp = rootNode;
            while (temp != null)
            {
                if (temp.RightChild == null)
                    break;

                temp = temp.RightChild;
            }

            return temp;
        }

        /************************************************************************/
        /* In order spanning the string list should be in order                 */
        /************************************************************************/
        public void inOrderSpanning(TreapNode rootNode, ref List<string> ls)
        {
            if (rootNode.LeftChild != null)
                inOrderSpanning(rootNode.LeftChild, ref ls);

            ls.Add(rootNode.Label);

            if (rootNode.RightChild != null)
                inOrderSpanning(rootNode.RightChild, ref ls);
        }

        /************************************************************************/
        /* back traching, from leaf to root                                     */
        /***************** ******************************************************/
        public TreapNode backTraching(TreapNode start, TreapNode end)
        {
            TreapNode temp = start.Parent;
            while (temp != null && temp != end)
            {
                temp = temp.Parent;
            }
            return temp;
        }

        /************************************************************************/
        /* commit users touch with copy and swap technology                     */
        /* make sure when use touch one label shall memorize in a temp struct   */
        /* minimize the lock time                                               */
        /* should be invoke with scheduler in the project                       */
        /* and the scheduler time should greater then searching time            */
        /* otherwise it shall have race condition                               */
        /************************************************************************/
        public void commitUserTouch()
        {
            lock (this)
            {
                if (!_dirtyBit) return;
                _dirtyBit = false;
            }

            /**
             * 1. clone the left tree
             * because all the operators are on the left child of the root
             * so that there are no need lock for this part 
             * and once the treap is construct, there are low chance to and new node
             */
            if (_root.RightChild != null)
            {
                _root.RightChild = null;
            }

            TreapNode newRoot = null;
            preOrderSpanningClone(_root.LeftChild, ref newRoot);
            _root.RightChild = newRoot;

            /**
             * 2. Construct the Max heap
             * build the heap at the assertion time
             */

            /**
             * 3. swap left and right child, with lock 
             * this for the thread safe as copy and swap
             */
            lock (this)
            {
                TreapNode sp_temp = _root.LeftChild;
                _root.LeftChild = _root.RightChild;
                _root.RightChild = sp_temp;
            }
        }

        private void preOrderSpanningClone(TreapNode Node, ref TreapNode parent)
        {
            if (Node == null) return;

            if (parent == null)
            {
                parent = new TreapNode();
                parent.cloneContent(Node);
            }
            else
            {
                TreapNode newNode = new TreapNode();
                newNode.cloneContent(Node);
                InsertLabel(parent, newNode);
                while (parent.Parent != null)
                    parent = parent.Parent;
            }

            preOrderSpanningClone(Node.LeftChild, ref parent);
            preOrderSpanningClone(Node.RightChild, ref parent);
        }

        /************************************************************************/
        /* construct Max heap in tree with rotation                             */
        /************************************************************************/
        private void HeapConstruct(TreapNode HeapNode)
        {
            TreapNode parent = HeapNode.Parent;
            while (parent != null)
            {
                if (parent.refCount < HeapNode.refCount)
                {
                    if (parent.LeftChild == HeapNode)
                        HeapNode = R_rotation(HeapNode);
                    else
                        HeapNode = L_rotation(HeapNode);
                }
                else
                    break;

                parent = HeapNode.Parent;
            }
        }

        /************************************************************************/
        /* Left rotation                                                        */
        /************************************************************************/
        public TreapNode L_rotation(TreapNode node_t)
        {
            TreapNode parent = node_t.Parent;
            if (parent == null)
            {
                if (node_t.RightChild != null)
                {
                    return L_rotation(node_t.RightChild).LeftChild;
                }
                return node_t;
            }

            parent.RightChild = node_t.LeftChild;
            node_t.LeftChild = parent;
            node_t.Parent = parent.Parent;
            parent.Parent = node_t;

            if (node_t.Parent == null)
                _root.LeftChild = node_t;

            return node_t;
        }

        /************************************************************************/
        /* right rotation                                                       */
        /************************************************************************/
        public TreapNode R_rotation(TreapNode node_t)
        {
            TreapNode parent = node_t.Parent;
            if (parent == null)
            {
                if (node_t.LeftChild != null)
                {
                    return R_rotation(node_t.LeftChild).RightChild;
                }
                return node_t;
            }

            parent.LeftChild = node_t.RightChild;
            node_t.RightChild = parent;
            node_t.Parent = parent.Parent;
            parent.Parent = node_t;

            if (node_t.Parent == null)
                _root.LeftChild = node_t;

            return node_t;
        }
    }
}
