using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTest_MWServiceQuery
{
    public partial class Form1 : Form
    {
        public Form1(string boo)
        {
            InitializeComponent();
            InitOutBox(boo);
        }

        public void InitOutBox(string str) {
            richTextBox1.Text = str.ToString();
        }

        private void OutputTextBlock_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
