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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        //when the back button is clicked - this will perform the same function as the next day button function on the stock page
        private void BtnBack_Click(object sender, EventArgs e)
        {
            //call the query to get all the data from the products table
            StockManagementDataSet.productsDataTable productsTable = productsTableAdapter.GetData();

            //variable that stores the number of rows in the products table
            int numRows = productsTable.Rows.Count;
            //set a new varibale equal to 0
            int x = 0;
            //For loop that will increment i to the length numRows minus 1
            //The minus 1 is there because the index of the items in the table start at 0 and therefore it will go out of range if the minus 1 is not there
            for (int i = 0; i <= numRows - 1; i++)
            {
                //set row to the current row in the products table
                DataRow row = productsTable.Rows[x];
                //set variables equal to the columns in the table and cast them appropriately
                string name = row["name"].ToString();
                int quantity = (int)row["quantity"];
                int orderSize = (int)row["orderSize"];
                
                //use the update query to change the quantity so that the order coming in is added on to it. The name column is also required here
                this.productsTableAdapter.UpdateQuantity(quantity + orderSize, name);
                //use the update query created to change the order size back to 0 as this has been added in to the quantity column
                this.productsTableAdapter.UpdateOrderSize2(0, name);
                x++;
            }

            //call the get queries used to get the stock for each department in each datagridview
            this.productsTableAdapter.GetProduce(this.stockManagementDataSet.products);
            this.productsTableAdapter2.GetBakery2(this.stockManagementDataSet3.products);
            this.productsTableAdapter1.GetAmbient2(this.stockManagementDataSet2.products);
            this.productsTableAdapter3.GetButchers2(this.stockManagementDataSet4.products);
           
            //close the form
            this.Dispose();
            //open up a new stock manager form
            StockManager stockManager = new StockManager();
            stockManager.Show();
        }

        //when the form is opened the data will be loaded into the datagridviews. One for each of the four as there is 4 different categories
        private void Orders_Load(object sender, EventArgs e)
        {
            this.productsTableAdapter.GetProduceOrder(this.stockManagementDataSet.products);
            this.productsTableAdapter2.GetBakeryOrder(this.stockManagementDataSet3.products);
            this.productsTableAdapter1.GetAmbientOrder(this.stockManagementDataSet2.products);
            this.productsTableAdapter3.GetButchersOrder(this.stockManagementDataSet4.products);
        }

        private void DgvProduce2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }    
    }
}
