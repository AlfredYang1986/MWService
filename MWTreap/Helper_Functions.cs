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
using System.Threading.Tasks;

namespace MWTreap
{
    class Helper_Functions
    {
        public delegate int delegate_Compare(string str1, string str2);

        /************************************************************************/
        /* The compare function for the treap                                   */
        /* The arguments are the TreapNodes                                     */
        /* if lhs < rhs return -1                                               */
        /* if lhs > rhs return  1                                               */
        /* if lhs == rhs return 0                                               */
        /************************************************************************/
        public static int TP_Compare_for_Construction(TreapNode lhs, TreapNode rhs)
        {
            return 0;
        }

        public static int min_three(int a, int b, int c)
        {
            return System.Math.Min(System.Math.Min(a, b), c);
        }

        /************************************************************************/
        /* The Edit Distance for the two string                                 */
        /* if the length of str1 is n and the length of str2 is m               */
        /* the time and space complexity both are n * m                         */
        /************************************************************************/
        public static int Edit_Distance(string str1, string str2)
        {
            Matrix m = new Matrix();
            return Edit_Distance(str1, str2, ref m);
        }

        private static int Edit_Distance(string str1, string str2, ref Matrix m)
        {
            int col = str1.Length;
            int row = str2.Length;

            m.resize(col, row);

            for (int row_index = 0; row_index < row; ++row_index)
            {
                char c1 = str2[row_index];
                for (int col_index = 0; col_index < col; ++col_index )
                {
                    char c2 = str1[col_index];

                    int val = 0;
                    if (c1 == c2)
                    {
                        if (m.vaildLowerBoundary(col_index - 1, row_index - 1))
                        {
                            val = m.setBrustValue(col_index, row_index,
                                m.getBrustValue(col_index - 1, row_index - 1) % Matrix.limited);
                        }
                        else
                        {
                            val = m.setBrustValue(col_index, row_index,
                                System.Math.Min(m.getBrustValue(col_index - 1, row_index)
                                , m.getBrustValue(col_index, row_index - 1)) % Matrix.limited);
                        }

                    }
                    else
                    {
                        // 15 * 15 matrix shall not have two string edit_distanc greater than 15;
                        //int left = m.getBrustValue(col_index - 1, row_index);
                        //int up = m.getBrustValue(col_index, row_index - 1);
                        //int left_up = m.getBrustValue(col_index - 1, row_index - 1);
                        val = min_three(m.getBrustValue(col_index - 1, row_index),
                                m.getBrustValue(col_index, row_index - 1),
                                m.getBrustValue(col_index - 1, row_index - 1)) % Matrix.limited + 1;
                        m.setBrustValue(col_index, row_index, val);
                    }
                }
            }

            return m.getBrustValue(col - 1, row - 1);
        }

        /************************************************************************/
        /* The Hamming_Distance for the two string                              */
        /* if the length of str1 is n and the length of str2 is m               */
        /* the time complexity is min(n, m)                                     */
        /************************************************************************/
        public static int Hamming_Distance(string str1, string str2)
        {
            int count = System.Math.Min(str1.Length, str2.Length);
            int max_len = System.Math.Max(str1.Length, str2.Length);

            int reVal = max_len - count;
            for (int index = 0; index < count; ++index)
            {
                if (str1[index] != str2[index])
                    ++reVal;
            }

            return reVal;
        }

        /************************************************************************/
        /* The Hamming_Distance for the two string                              */
        /* if the length of str1 is n and the length of str2 is m               */
        /* the time complexity is min(n, m)                                     */
        /************************************************************************/
        public static int Hamming(string str1, string str2)
        {
            int count = System.Math.Min(str1.Length, str2.Length);
            int max_len = System.Math.Max(str1.Length, str2.Length);

            int reVal = 0;
            for (int index = 0; index < count; ++index)
            {
                if (str1[index] == str2[index])
                    ++reVal;
            }

            return reVal;
        }

        /************************************************************************/
        /* The alphabet order for string str1 and string str2                   */
        /* if str1 < str2, return -1                                            */
        /* if str1 > str2, return 1                                             */
        /* if str1 == str2, return 0                                            */
        /************************************************************************/
        public static int Str_Compare(string str1, string str2)
        {
            return str1.CompareTo(str2);
        }
    }
}
