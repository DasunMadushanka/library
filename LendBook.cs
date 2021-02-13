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
    public partial class LendBook : Form
    {
        public LendBook()
        {
            InitializeComponent();
            book();
        }
        SqlConnection con = new SqlConnection(@"Data source=DESKTOP-NG0KRQP; Initial Catalog= plibrary;User ID=pathraka;Password=path;");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        string sql;
        bool mode = true;
        string id;


        public void book()
        {
            string query = "select * from books";
            cmd = new SqlCommand(query, con);
            con.Open();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            txtBook.DataSource = ds.Tables[0];
            txtBook.DisplayMember = "bname";
            txtBook.ValueMember = "id";
            con.Close();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtmid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                cmd = new SqlCommand("select * from member where id='"+ txtmid.Text+"'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtmname.Text = dr["name"].ToString();
                    
                }
                else
                {
                    MessageBox.Show("Member ID Not Found !");
                }

                con.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string mid = txtmid.Text;
            string mname = txtmname.Text;
            string book = txtBook.Text;
            string issuedate = txtidate.Value.ToString("yyyy-MM-dd");
            string returndate = txtrdate.Value.ToString("yyyy-MM-dd");

            sql = "insert into issuebook(memberid,book,issuedate,returndate)values(@memeberid,@book,@issuedate,@returndate)";
            con.Open();
            cmd = new SqlCommand(sql,con);
            cmd.Parameters.AddWithValue("@memeberid", mid);
            cmd.Parameters.AddWithValue("@book", book);
            cmd.Parameters.AddWithValue("@issuedate", issuedate);
            cmd.Parameters.AddWithValue("@returndate", returndate);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Book Issued !!!");
            con.Close();
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
