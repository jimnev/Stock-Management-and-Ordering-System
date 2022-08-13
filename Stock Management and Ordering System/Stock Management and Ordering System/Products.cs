using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_and_Ordering_System
{
    //Make public
    public class Products
    {
        //paramaterless constructor used for object initialization
        public Products() { }

        //constructor
        public Products(string name, int quantity, float price, string category, int orderSize, int maxStock)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Category = category;
            OrderSize = orderSize;
            MaxStock = maxStock;
        }

        //getters and setters
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public int OrderSize { get; set; }
        public int MaxStock { get; set; }

        //to string method for printing
        public override string ToString()
        {
            return $"Name: {Name}, Quantity: {Quantity}, Price: {Price}, Category: {Category}, Order Size: {OrderSize}, Max Stock: {MaxStock}";
        }
    }
}
