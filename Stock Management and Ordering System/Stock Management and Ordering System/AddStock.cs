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
    public partial class AddStock : Form
    {
        public AddStock()
        {
            InitializeComponent();
        }

        //Code to be performed when the add stock form is opened
        private void AddStock_Load(object sender, EventArgs e)
        {
             //This line of code loads data into the 'stockManagementDataSet.products' table
            this.productsTableAdapter.Fill(this.stockManagementDataSet.products);
        }

        //method to clear the text boxes that have inputs in them
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

        //code to execute when the back button is clicked
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Dispose();

            //open up a new stockManager page
            StockManager stockManager = new StockManager();
            stockManager.Show();
        }

        //Code to be performed when the add button is clicked
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //surround code in a try catch
            try
            {
                //variables to store values when error checking quantity, and price inputs
                int parsedValue;
                float parsedValue2;

                //if any of the text boxes are empty
                if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtQuantity.Text)
                    || string.IsNullOrEmpty(TxtPrice.Text) || string.IsNullOrEmpty(CmbCategory.Text))
                {
                    //display a message 
                    MessageBox.Show("Text boxes cannot be empty");
                }
                //if the input in the quantity box is not a integer display a message
                else if (!int.TryParse(TxtQuantity.Text, out parsedValue))
                {
                    MessageBox.Show("Quantity can only be a number");
                }
                //if the input in the price box is not a number display a message
                else if (!float.TryParse(TxtPrice.Text, out parsedValue2))
                {
                    MessageBox.Show("Price can only be a number");
                }
                //if the text boxes arent null   
                else if (TxtName.Text != null || TxtQuantity.Text != null
                    || TxtPrice.Text != null || CmbCategory.Text != null)
                {
                    //insert the information in the textboxes into the products table
                    //The 2 zeros at the end are for the starting off order size - which is 0 as there are no orders for it yet, and for the max order column which was never used.
                    int id = productsTableAdapter.Insert(TxtName.Text, Int32.Parse(TxtQuantity.Text), (float)Math.Round(float.Parse(TxtPrice.Text) * 100f) / 100f, CmbCategory.Text, 0, 0);
                    //show a mesage that it has been added
                    MessageBox.Show("Stock successfully added");
                    //call the clear method to empty the text boxes
                    ClearTextBoxes();
                }
                //else statement to catch any other possible errors
                else
                {
                    MessageBox.Show("Error");
                }
            }
            //This catch statement will show a message box if the stock entered is already in the database
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("You already have this stock name");
                return;
            }
        }
    }
}
