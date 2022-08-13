using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Management_and_Ordering_System
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        //when the register button is clicked do this
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            //wrap the code in a try catch so any errors can be caught
            try
            {
                //if an of the text boxes are empty show a message to the user
                if (string.IsNullOrEmpty(TxtFirst.Text) || string.IsNullOrEmpty(TxtLast.Text) || string.IsNullOrEmpty(TxtUser.Text)
                    || string.IsNullOrEmpty(TxtPass.Text) || string.IsNullOrEmpty(CmbRole.Text))
                {
                    MessageBox.Show("Text boxes cannot be empty");
                }
                //if the are not empty
                else if (TxtFirst.Text != null || TxtLast.Text != null || TxtUser.Text != null
                    || TxtPass.Text != null || CmbRole.Text != null)
                {
                    //insert into login table the values entered
                    int id = loginTableAdapter.Insert(TxtFirst.Text, TxtLast.Text, TxtUser.Text, TxtPass.Text, CmbRole.Text);

                    //Pop up a message saying the insert was successful
                    MessageBox.Show("Account has been created successfully");

                    //clear the textboxes when this is successful so another user can be inserted without having to delete them manually
                    ClearTextBoxes();
                }
                //else statement to show an error if anything manages to get through
                else
                {
                    MessageBox.Show("Error");
                }
            }
            //catch that will show if the user is alread on the system
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("A user already has this username. Try again");
                return;
            }
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

        private void Register_Load(object sender, EventArgs e)
        {
            //This line of code loads data into the 'stockManagementDataSet.login' table
            this.loginTableAdapter.Fill(this.stockManagementDataSet.login);

        }

        //when the back button is clicked do this
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Dispose();
            //open the stock manager page
            StockManager stockManager = new StockManager();
            stockManager.Show();
        }
    }
}
