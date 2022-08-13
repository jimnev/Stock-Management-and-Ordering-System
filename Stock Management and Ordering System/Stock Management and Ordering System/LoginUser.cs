using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//To use the table adapters 
using Stock_Management_and_Ordering_System.StockManagementDataSetTableAdapters;

namespace Stock_Management_and_Ordering_System
{
    public partial class LoginUser : Form
    {
        public LoginUser()
        {
            InitializeComponent();
        }

        //when the form is opened in the login data will be loaded
        private void Login_Load(object sender, EventArgs e)
        {
            //This line of code loads data into the 'stockManagementDataSet.login' table
            this.loginTableAdapter.Fill(this.stockManagementDataSet.login);

        }

        //When the login button is clicked
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //if the textboxes are not empty
            if (TxtUsername.Text.Length > 0 && TxtPassword.Text.Length > 0)
            {
                //call the get data query created in the dataset to get all data from the login table where username and password equal the textbox entries
                StockManagementDataSet.loginDataTable loginTable = loginTableAdapter.GetLoginByUsernameAndPassword(TxtUsername.Text, TxtPassword.Text);

                //create a variable that holds the number of rows in the login table
                int numRows = loginTable.Rows.Count;

                //if a row is returned do this
                if (numRows > 0)
                {
                    //assign the first row in the data table to the variable row
                    DataRow row = loginTable.Rows[0];

                    //variables that hold the table columns that are casted into the relevant datatypes
                    int id = (int)row["id"];
                    string fName = row["fName"].ToString();
                    string lName = row["lName"].ToString();
                    string username = row["username"].ToString();
                    string password = row["password"].ToString();
                    string role = row["role"].ToString();

                    //use the constructor to turn the info into an object
                    Login login = new Login
                    {
                        Id = id,
                        FName = fName,
                        LName = lName,
                        Username = username,
                        Password = password,
                        Role = role
                    };

                    //use a switch case statement to decide what page is opened - manager or employee
                    //the switch is on the role column 
                    switch (role)
                    {
                        //if the role is equal to manager 
                        case "manager":
                            //open the manager page and clear the text boxes on the login form
                            StockManager stockManager = new StockManager();
                            stockManager.Show();
                            ClearTextBoxes();
                            //break out of case
                            break;
                        
                        //if the role is employee in the database
                        case "employee":
                            //open thge employee stock form and clear the textboxes
                            StockEmployee stockEmployee = new StockEmployee();
                            stockEmployee.Show();
                            ClearTextBoxes();
                            break;

                        //default case that returns a message box
                        default:
                            MessageBox.Show("Role was invalid");
                            break;
                    }
                }
                //if no rows are returned, meaning the login is not found in database, display a message
                else if (numRows == 0)
                {
                    MessageBox.Show("Login Details Incorrect");
                }
                //else statement to catch any potenbtial errors
                else
                {
                    MessageBox.Show("Error");
                }
            }
            //if the text boxes are empty show a message
            else if (string.IsNullOrEmpty(TxtUsername.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("Text boxes cannot be empty");
            }
            //else to catch any other errors
            else
            {
                MessageBox.Show("Error");
            }
        }

        //if exit is pressed in the tool strip on the top of the page or alt-f4
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //the entire application will close
            Application.Exit();
        }

        //if help is pressed in the tool strip on the top of the page or f1
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //the help page dialog will show
            new Dialog().ShowDialog();
        }

        //Function to clear text boxes
        private void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                // If the control is a textbox
                if (control is TextBox)
                {
                    // Clear the text
                    control.Text = string.Empty;
                }
            }
        }
    }
}
