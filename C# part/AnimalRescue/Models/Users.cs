using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRescue.Models
{
    public class Users
    {
        public int ID_User { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_Address { get; set; }
        public string Pass { get; set; }
        public string Phone_Number { get; set; }
        public string Token { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }

        public Users() { }
        public Users(int id_user, string first_name, string last_name, string email_address, string pass, string phone_number,
           string token, decimal lat, decimal lng)
        {
            ID_User = id_user;
            First_Name = first_name;
            Last_Name = last_name;
            Email_Address = email_address;
            Pass = pass;
            Phone_Number = phone_number;
            Token = token;
            Lat = lat;
            Lng = lng;
        }
        public Users(int id_user, string first_name, string last_name, string email_address, string pass, string phone_number)
        {
            ID_User = id_user;
            First_Name = first_name;
            Last_Name = last_name;
            Email_Address = email_address;
            Pass = pass;
            Phone_Number = phone_number;
            
        }

        public Users(string token, decimal lat, decimal lng)
        {
            Token = token;
            Lat = lat;
            Lng = lng;
        }

        public override string ToString()
        {
            return $"{ID_User}, {First_Name}, {Last_Name}, {Email_Address}, {Pass}, {Phone_Number}";
        }
    }
}