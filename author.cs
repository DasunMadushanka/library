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
    public partial class author : Form
    {
        public author()
        {
            InitializeComponent();
            load();
        }

        SqlConnection con = new SqlConnection(@"Data source=DESKTOP-NG0KRQP; Initial Catalog= plibrary;User ID=pathraka;Password=path;");
        SqlCommand cmd;
        SqlDataReader dr;
        string sql;
        bool mode = true;
        string id;

        private void author_Load(object sender, EventArgs e)
        {

        }

        public void load()
        {
            sql = "select * from author";
            cmd = new SqlCommand(sql, con);
            con.Open();

            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2],dr[3]);
            }
            con.Close();
        }
        public void getid(string id)
        {
            sql = "select * from author where id = '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtname.Text = dr[1].ToString();
                txtaddress.Text = dr[2].ToString();
                txtphone.Text = dr[3].ToString();

            }
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string address = txtaddress.Text;
            string phone = txtphone.Text;

         

            if (mode == true)
            {
                sql = "insert into author(name,address,phone)values(@name,@address,@phone)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Author Added!");
                txtname.Clear();
                txtaddress.Clear();
                txtphone.Clear();

                txtname.Focus();
                con.Close();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update author set name=@name, address=@address, phone=@phone where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                 cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("author Updated!");
                txtname.Clear();
                txtaddress.Clear();
                txtphone.Clear();
                //    txtStatus.SelectedIndex = -1;
                txtname.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);
            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                sql = "Delete from author where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Author Deleted!");
                txtname.Clear();
               // txtStatus.SelectedIndex = -1;
                txtname.Focus();
                con.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
    }

