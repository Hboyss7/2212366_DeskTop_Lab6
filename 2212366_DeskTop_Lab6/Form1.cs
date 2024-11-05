using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _2212366_DeskTop_Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "server = PC727; database = RestaurantManagement1; Integrated Security = true;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            string query = "SELECT ID, NAME, TYPE FROM Category";

            sqlCommand.CommandText = query; 

            sqlConnection.Open();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            this.DisplayCategory(sqlDataReader);

            sqlConnection.Close();

            
        }

        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();

            while (reader.Read()) 
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());

                lvCategory.Items.Add(item);

                item.SubItems.Add(reader["Name"].ToString());

                item.SubItems.Add(reader["Type"].ToString());

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "server = PC727; database = RestaurantManagement1; Integrated Security = true;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" + "VALUES (N' " + txtName.Text + " ' , " + txtType.Text + ")";

            sqlConnection.Open();

            int numofRowsEffected = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            if (numofRowsEffected  == 1)
            {
                MessageBox.Show("Thêm món ăn thành công");

                this.btnLoad.PerformClick();

                txtName.Text = "";
                txtType.Text = "";
            } 
            
            else
            {
                MessageBox.Show("ĐÃ CÓ LỖI");
            }    

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server = PC727; database = RestaurantManagement1; Integrated Security = true;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "UPDATE Category SET Name = N'" + txtName.Text + 
                                            "', [TYPE] = " + txtType.Text + 
                                            " WHERE ID = " + txtID.Text;

            sqlConnection.Open();

            int numofRowsEffected = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            if (numofRowsEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];

                item.SubItems[1].Text = txtName.Text;
                item.SubItems[2].Text = txtType.Text;

                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Cập nhật món ăn thành công");
            }

            else
            {
                MessageBox.Show("ĐÃ CÓ LỖI");
            }
        }

        private void lvCategory_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvCategory.SelectedItems[0];

            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[1].Text == "0" ? "Thuc uong" : "Do an";

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "server = PC727; database = RestaurantManagement1; Integrated Security = true;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "DELETE FROM Category " + "WHERE ID = " + txtID.Text;

            sqlConnection.Open();

            int numofRowsEffected = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            if (numofRowsEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];

                lvCategory.Items.Remove(item);

                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Xóa món ăn thành công");
            }

            else
            {
                MessageBox.Show("ĐÃ CÓ LỖI");
            }
        }
    }
}
