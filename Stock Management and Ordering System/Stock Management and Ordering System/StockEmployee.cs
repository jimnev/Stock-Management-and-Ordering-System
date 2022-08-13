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
    public partial class StockEmployee : Form
    {
        public StockEmployee()
        {
            InitializeComponent();
        }

        //when the form loads, the data is loaded into the datagridviews
        private void StockEmployee_Load(object sender, EventArgs e)
        {
            this.productsTableAdapter.GetProduce(this.stockManagementDataSet.products);
            this.productsTableAdapter2.GetBakery2(this.stockManagementDataSet3.products);
            this.productsTableAdapter1.GetAmbient2(this.stockManagementDataSet2.products);
            this.productsTableAdapter3.GetButchers2(this.stockManagementDataSet4.products);
        }

        //when the orders button is clicked do this   
        private void BtnOrders_Click(object sender, EventArgs e)
        {
            //Close this form
            this.Dispose();
            //open the employee orders table
            OrdersEmployee ordersEmployee = new OrdersEmployee();
            ordersEmployee.Show();
        }

        //when the generate button is clicked
        //this generates random sales for each product in the database and minuses these sales from the stock quantity
        private void BtnGen_Click(object sender, EventArgs e)
        {
            //make the generate button disappear so it cannot be pressed again straight away
            BtnGen.Visible = false;

            //get the product data using the product table adapter
            StockManagementDataSet.productsDataTable productsTable = productsTableAdapter.GetData();

            //create a variable that holds the number of rows in the products table
            int numRows = productsTable.Rows.Count;

            //create new random data variable
            Random jamesNum = new Random();
            //declare new variable
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

                //these were outputs used in testing to see if the randum numbers were working and assigning to the correct product names
                //Console.WriteLine(rand);
                //Console.WriteLine(name);

                //variable that holds the table column thats casted to int
                int quantity = (int)row["quantity"];
                //using the custom update query to update the quantity, take away the random number that has been generated and places into the orders column from the quantity.
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

            //show the generate button
            BtnGen.Visible = true;
            //hide the next day button
            BtnNextD.Visible = false;
        }
    }
}
