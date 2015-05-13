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

namespace EmployeeInfoApp
{
    public partial class EmployeeInfoUI : Form
    {
        public EmployeeInfoUI()
        {
            InitializeComponent();
        }
        string connectionstring = @"SERVER=PC-301-25\SQLEXPRESS;Database=EmployeeIntroDB;integrated security=true";
        private void saveButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string email = emailTextBox.Text;
            int salary = Convert.ToInt32(salaryTextBox.Text);

            if (name != "" || address != "" || email != "")
            {
                if (salary>0)
                {
                    SqlConnection aSqlConnection = new SqlConnection(connectionstring);
                    string query = "INSERT INTO EmployeeInfoTable VALUES ('" + name + "','" + address + "','" + email + "','" + salary + "')";
                    SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
                    aSqlConnection.Open();
                    int rowAffected = aSqlCommand.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        MessageBox.Show("Done");
                    }
                    aSqlConnection.Close();

                    nameTextBox.Clear();
                    addressTextBox.Clear();
                    emailTextBox.Clear();
                    salaryTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Enter valid salary!");
                }
                
                
            }
            else
            {
                MessageBox.Show("Please Enter All items!!!");
            }

        }
        private void showButton_Click(object sender, EventArgs e)
        {
            SqlConnection aSqlConnection = new SqlConnection(connectionstring);
            aSqlConnection.Open();
            string query = "SELECT * FROM EmployeeInfoTable";

            SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader reader = aSqlCommand.ExecuteReader();
            showDataGridView.Rows.Clear();
            while(reader.Read())
            {
                showDataGridView.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(),
                    reader[4].ToString());
            }
            aSqlConnection.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int deleteID = Convert.ToInt32(deleteIDTextBox.Text);
            SqlConnection aSqlConnection = new SqlConnection(connectionstring);
            aSqlConnection.Open();

            string query = "DELETE FROM EmployeeInfoTable WHERE id = '"+deleteID+"'";
            SqlCommand aSqlCommand = new SqlCommand(query, aSqlConnection);
            int rowAffected = aSqlCommand.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                MessageBox.Show("Deleted!");
            }
            aSqlConnection.Close();
            deleteIDTextBox.Clear();

        }
    }
}
