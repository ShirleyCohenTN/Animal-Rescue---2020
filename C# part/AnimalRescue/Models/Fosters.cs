using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRescue.Models
{
    public class Fosters
    {
        public int ID_Foster { get; set; }
        public int ID_User { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone_Number { get; set; }
        public string City { get; set; }
        public string Info { get; set; }

        public Fosters() { }

        public Fosters(int id_foster, int id_user, string first_name, string last_name, string phone_number, string city, string info)
        {
            ID_Foster = id_foster;
            ID_User = id_user;
            First_Name = first_name;
            Last_Name = last_name;
            Phone_Number = phone_number;
            City = city;
            Info = info;
        }

        public override string ToString()
        {
            return $"{ID_Foster}, {ID_User}, {First_Name}, {Last_Name}, {Phone_Number}, {City}, {Info}";
        }
    }
}