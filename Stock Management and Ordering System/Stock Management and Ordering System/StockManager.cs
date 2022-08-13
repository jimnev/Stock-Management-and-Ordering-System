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
    public partial class StockManager : Form
    {
        public StockManager()
        {
            InitializeComponent();
        }

        //when the add user button is clicked do this
        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            //close this form
            this.Dispose();
            //open up the register form
            Register register = new Register();
            register.Show();
        }

        //method to stop the default error message on datagrid
        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //sets the error message to cancel
            e.Cancel = true;
        }

        //When the form is first opened this code will be executed
        private void StockManager_Load(object sender, EventArgs e)
        {
            //stops error message if wrong datatype entered for update stock. One for each datagrid
            //the update will just not work instead of showing an error
            this.DgvProduce.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);
            this.DgvAmbient.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);
            this.DgvBakery.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);
            this.DgvButchers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView2_DataError);

            //This line of code loads data into the datagridviews
            this.productsTableAdapter.GetProduce(this.stockManagementDataSet.products);
            this.productsTableAdapter2.GetBakery2(this.stockManagementDataSet3.products);
            this.productsTableAdapter1.GetAmbient2(this.stockManagementDataSet2.products);
            this.productsTableAdapter3.GetButchers2(this.stockManagementDataSet4.products);
        }

        //when the update button is clicked perform this code
        //This works when the quantity or price is changed in the datagridviews and the update button is clicked just after
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //each data grid has its own table adapter and dataset - these table adapters and data sets are all the same just duplicates for each datagridview
            //the update function is called to update the dataset to what has been entered in the datagridview by the user
            productsTableAdapter.Update(stockManagementDataSet.products);
            productsTableAdapter1.Update(stockManagementDataSet2.products);
            productsTableAdapter2.Update(stockManagementDataSet3.products);
            productsTableAdapter3.Update(stockManagementDataSet4.products);                      
        }         
    
        //when the add stock buton is clicked do this
        private void BtnAddStock_Click(object sender, EventArgs e)
        {
            //close this form
            this.Dispose();
            //open up the add stock form
            AddStock addStock = new AddStock();
            addStock.Show();
        }

        //when the generate button is clicked 
        private void BtnGen_Click(object sender, EventArgs e)
        {
            //make the generate button disappear so it cannot be pressed again until the next day button is pressed
            BtnGen.Visible = false;

            //get the product data using the product table adapter
            StockManagementDataSet.productsDataTable productsTable = productsTableAdapter.GetData();

            //create a variable that holds the number of rows in the products table
            int numRows = productsTable.Rows.Count;

            //create new random data variable
            Random jamesNum = new Random();
            int x = 0;

            //loop i until it reaches numRows minus 1. This is because the index starts at 0 so it will be out of bounds by the time it gets to the last element without the minus 1
            for (int i = 0; i <= numRows - 1; i++)
            {
                //assign the current value of x as the row in the products table and assign it to the variable row
                DataRow row = productsTable.Rows[x];
                //variable that holds the column 'name' that is casted to a string
                string name = row["name"].ToString();
                //variable to store the randum number that is set between 1 and 10
                int rand = jamesNum.Next(1, 10);
                //update the ordersize by putting the random number into it where the name equals the current name on that row
                this.productsTableAdapter.UpdateOrderSize2(rand, name);

                //variables that hold the columns 'quantity' and 'orderSize' that are casted to int
                int quantity = (int)row["quantity"];
                int orderSize = (int)row["orderSize"];
                //using the custom update query to update the quantity, take away the random number that has been generated from the quantity.
                //This emulates the sales
                this.productsTableAdapter.UpdateQuantity(quantity - rand, name);
                //increment x so that next time around in the for loop it moves to the next product
                x++;
            }
            //make the next da button visible now
            BtnNextD.Visible = true;

            //update the datagridviews to show the change in stock levels
            this.productsTableAdapter.GetProduce(this.stockManagementDataSet.products);
            this.productsTableAdapter2.GetBakery2(this.stockManagementDataSet3.products);
            this.productsTableAdapter1.GetAmbient2(this.stockManagementDataSet2.products);
            this.productsTableAdapter3.GetButchers2(this.stockManagementDataSet4.products);
        }

        //when the next day button is pressed
        //this code is the same as before but just updates the stock differently
        private void BtnNextD_Click(object sender, EventArgs e)
        {
            StockManagementDataSet.productsDataTable productsTable = productsTableAdapter.GetData();

            int numRows = productsTable.Rows.Count;
            int x = 0;
            
            for (int i = 0; i <= numRows - 1; i++)
            {
                DataRow row = productsTable.Rows[x];
                string name = row["name"].ToString();               

                int quantity = (int)row["quantity"];
                int orderSize = (int)row["orderSize"];
                //add the order size on to the quantity to replenish the stock
                this.productsTableAdapter.UpdateQuantity(quantity + orderSize, name);
                //update the order size bacvk to 0
                this.productsTableAdapter.UpdateOrderSize2(0, name);
                x++;
            }

            //update the datagridviews to show the change in stock levels
            this.productsTableAdapter.GetProduce(this.stockManagementDataSet.products);
            this.productsTableAdapter2.GetBakery2(this.stockManagementDataSet3.products);
            this.productsTableAdapter1.GetAmbient2(this.stockManagementDataSet2.products);
            this.productsTableAdapter3.GetButchers2(this.stockManagementDataSet4.products);

            //show the generate button so it can be used again for the next days sales
            BtnGen.Visible = true;
            //hide the next day button as this has no function until the new sales are generated
            BtnNextD.Visible = false;
        }

        //when the orders button is clicked do this
        private void BtnOrders_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Dispose();
            //open the employee orders table
            Orders orders = new Orders();
            orders.Show();
        }

        //do this when the sales button is clicked
        private void BtnSales_Click(object sender, EventArgs e)
        {
            //get the product data using the product table adapter
            StockManagementDataSet.productsDataTable productsTable = productsTableAdapter.GetData();

            //declare 3 variables and equal them to 0
            float calc = 0;
            float total = 0;
            int x = 0;

            //create a variable that holds the number of rows in the products table
            int numRows = productsTable.Rows.Count;

            //loop i until it reaches numRows minus 1. This is because the index starts at 0 so it will be out of bounds by the time it gets to the last element without the minus 1
            for (int i = 0; i <= numRows - 1; i++)
            {
                //assign the current value of x as the row in the products table and assign it to the variable row
                DataRow row = productsTable.Rows[x];

                //variable that holds the column 'name' that is casted to a string
                string name = row["name"].ToString();
                //variable that holds the column 'price' that is casted to a float. The ToString() method is needed when casting to float
                float price = float.Parse(row["price"].ToString());
                //variable that holds the column 'orderSize' that is casted to int
                int orderSize = (int)row["orderSize"];
                //assign the current orderSize multiplied by the current price to the calc variable 
                calc = orderSize * price;

                //Each time this loop goes through each product, the calc variable gets added to the total. By the time the for loop ends the total variable will contain all the sales
                total += calc;

                //increment x so that next time around in the for loop it moves to the next product
                x++;
            }

            //In the label on the form, have it  display the euro symbol followed by the 'total' variable amount
            //The number is rounded to 2 decimal places using the Math.Round() function
            LblTotalSales.Text = "€" + Math.Round(total,2).ToString();
        }

        private void DgvProduce_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
