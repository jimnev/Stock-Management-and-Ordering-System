using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Management_and_Ordering_System
{
    //Make public for use with login form
    public class Login
    {
        //paramaterless constructor used for object initialization 
        public Login() { }

        //constructor
        public Login(int id, string fName, string lName, string username, string password, string role)
        {
            Id = id;
            FName = fName;
            LName = lName;
            Username = username;
            Password = password;
            Role = role;
        }

        //getters and setters

        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //to string method for printing
        public override string ToString()
        {
            return $"Id: {Id}, First Name: {FName}, Last Name: {LName}, Username: {Username}, Password: {Password}, Role: {Role}";
        }
    }
}
