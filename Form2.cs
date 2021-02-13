using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 category = new Form1();
            category.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            author category = new author();
            category.Show();
        }

        private void Publisher_Click(object sender, EventArgs e)
        {
            Publisher category = new Publisher();
            category.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Book bookcat = new Book();
            bookcat.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Member m = new Member();
            m.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LendBook l = new LendBook();
            l.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            returnbook ret = new returnbook();
            ret.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Login l = new Login();
            l.Show();

        }
    }
}
