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
    class MatrixException : System.Exception { }

    class Matrix
    {
        public const int limited = 100;

        private const int x_upper_bound = limited;
        private const int y_upper_bound = limited;

        public int col { get; set; }
        public int row { get; set; }

        //public List<int> _matrix;
        public int[] _matrix;

        public Matrix()
        {
            //_matrix = new List<int>(x_upper_bound * y_upper_bound);
            //_matrix.
            _matrix = new int[x_upper_bound * y_upper_bound];
        }

        public void resize(int x, int y)
        {
            if (x > x_upper_bound || y > y_upper_bound)
                throw new MatrixException();

            col = x;
            row = y;
            //_matrix.Clear();
        }

        public Boolean validUpperBoundary(int x, int y)
        {
            return x < col && y < row;
        }

        public Boolean vaildLowerBoundary(int x, int y)
        {
            return x >= 0 && y >= 0;
        }

        public int setBrustValue(int x, int y, int value)
        {
            if (validUpperBoundary(x, y) && vaildLowerBoundary(x, y))
            {
                _matrix[x + y * col] = value;
                return value;
            }

            throw new MatrixException();
        }

        public int getBrustValue(int x, int y)
        {
            if (!vaildLowerBoundary(x, y))
                return limited;  // 15 * 15 matrix shall not have two string edit_distanc greater than 16;

            else if (!validUpperBoundary(x, y))
                return limited;  // 15 * 15 matrix shall not have two string edit_distanc greater than 16;

            //if (validUpperBoundary(x, y) && vaildLowerBoundary(x, y))
            else
                return _matrix[x + y * col];
        }
    }
}
