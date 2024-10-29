using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
            string connectionString = "server = PC721; database = RestaurantManagement1; Integrated Security = true;";

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
    }
}
