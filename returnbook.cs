using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace library
{
    public partial class returnbook : Form
    {
        public returnbook()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data source=DESKTOP-NG0KRQP; Initial Catalog= plibrary;User ID=pathraka;Password=path;");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        string sql;
        bool mode = true;
        string id;


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                cmd = new SqlCommand("select book,issuedate,returndate,DATEDIFF(dd,returndate,GETDATE())as elap from issuebook where memberid = '"+ txtmid.Text +"'",con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtbname.Text = dr["book"].ToString();
                    txtrdate.Text = dr["returndate"].ToString();
                    string elap = dr["elap"].ToString();

                    int elapp = int.Parse(elap);
                    
                
                    if (elapp > 0)
                    {
                        txtelap.Text = elap;
                        int fine = elapp * 50;
                        txtfine.Text = fine.ToString();

                    }
                    else{
                        txtelap.Text = "0";
                        txtfine.Text = "0";
                    }
                    
                }
                else
                {
                    MessageBox.Show("Member ID Not Found !");
                }

                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void returnbook_Load(object sender, EventArgs e)
        {

        }

        private void txtbname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtrdate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtelap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtfine_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string mid = txtmid.Text;
            string book = txtbname.Text;
            string rdate = txtrdate.Text;
            string elap = txtelap.Text;
            string fine = txtfine.Text;

            sql = "insert into returnbook(memberid,book,returndate,elap,fine)values(@memeberid,@book,@returndate,@elap,@fine)";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@memeberid", mid);
            cmd.Parameters.AddWithValue("@book", book);
            cmd.Parameters.AddWithValue("@returndate", rdate);
            cmd.Parameters.AddWithValue("@elap", elap);
            cmd.Parameters.AddWithValue("@fine", fine);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Book Return successfully !!!");
            con.Close();

        }
    }
    }

