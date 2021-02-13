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
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
            category();
            author();
            publisher();
            load();
        }

        SqlConnection con = new SqlConnection(@"Data source=DESKTOP-NG0KRQP; Initial Catalog= plibrary;User ID=pathraka;Password=path;");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        string sql;
        bool mode = true;
        string id;

        public void category()
        {
            string query = "select * from category";
            cmd = new SqlCommand(query,con);
            con.Open();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            txtCategory.DataSource = ds.Tables[0];
            txtCategory.DisplayMember = "catname";
            txtCategory.ValueMember = "id";
            con.Close();
        }

        public void author()
        {
            string query = "select * from author";
            cmd = new SqlCommand(query, con);
            con.Open();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            txtAuthor.DataSource = ds.Tables[0];
            txtAuthor.DisplayMember = "name";
            txtAuthor.ValueMember = "id";
            con.Close();
        }

        public void publisher()
        {
            string query = "select * from publisher";
            cmd = new SqlCommand(query, con);
            con.Open();
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            txtPublisher.DataSource = ds.Tables[0];
            txtPublisher.DisplayMember = "name";
            txtPublisher.ValueMember = "id";
            con.Close();
        }


        public void load()
        {
            sql = "select b.id,b.bname,c.catname,a.name,p.name,b.contents,b.page,b.edition from books b JOIN category c ON b.cat_id = c.id JOIN author a ON b.author_id = a.id JOIN publisher p ON b.p_id = p.id ";
            cmd = new SqlCommand(sql, con);
            con.Open();

            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7]);
            }
            con.Close();
        }

        public void getid(string id)
        {
            sql = "select * from books where id = '" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtname.Text = dr[1].ToString();
                txtCategory.Text = dr[2].ToString();
                txtAuthor.Text = dr[3].ToString();
                txtPublisher.Text = dr[4].ToString();
                txtContents.Text = dr[5].ToString();
                txtPages.Text = dr[6].ToString();
                txtEdition.Text = dr[7].ToString();

            }
            con.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string category = txtCategory.SelectedValue.ToString();
            string author = txtAuthor.SelectedValue.ToString();
            string publisher = txtPublisher.SelectedValue.ToString();
            string content = txtContents.Text;
            string pages = txtPages.Text;
            string edition = txtEdition.Text;


            if (mode == true)
            {
                sql = "insert into books(bname,cat_id,author_id,p_id,contents,page, edition)values(@bname,@cat_id,@author_id,@p_id,@contents,@page,@edition)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@bname", name);
                cmd.Parameters.AddWithValue("@cat_id", category);
                cmd.Parameters.AddWithValue("@author_id", author);
                cmd.Parameters.AddWithValue("@p_id", publisher);
                cmd.Parameters.AddWithValue("@contents", content);
                cmd.Parameters.AddWithValue("@page", pages);
                cmd.Parameters.AddWithValue("@edition", edition);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Added!");
               

                txtname.Focus();


            }
            else
            {

                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update books set name=@bname, cat_id=@cat_id, author_id=@author_id, p_id=@p_id,contents=@contents,page=@page, edition=@edition  where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@bname", name);
                cmd.Parameters.AddWithValue("@cat_id", category);
                cmd.Parameters.AddWithValue("@author_id", author);
                cmd.Parameters.AddWithValue("@p_id", publisher);
                cmd.Parameters.AddWithValue("@contents", content);
                cmd.Parameters.AddWithValue("@page", pages);
                cmd.Parameters.AddWithValue("@edition", edition);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Publisher Updated!");
                txtname.Clear();
                txtContents.Clear();
                txtPages.Clear();
                txtEdition.Clear();
                //    txtStatus.SelectedIndex = -1;
                txtname.Focus();
            }
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

                sql = "Delete from books where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Deleted!");
                txtname.Clear();
                // txtStatus.SelectedIndex = -1;
                txtname.Focus();
                con.Close();
            }
        }
    }
}
