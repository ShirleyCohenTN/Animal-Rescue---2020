using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRescue.Models
{
    public class Veterinarians
    {
        public int ID_Veterinarian { get; set; }
        public string Name_Veterinarian { get; set; }
        public string City { get; set; }
        public string Phone_Number { get; set; }

        public Veterinarians() { }

        public Veterinarians(int id_veterinarian, string name_veterinarian, string city, string phone_number)
        {
            ID_Veterinarian = id_veterinarian;
            Name_Veterinarian = name_veterinarian;
            City = city;
            Phone_Number = phone_number;
        }

        public override string ToString()
        {
            return $"{ID_Veterinarian}, {Name_Veterinarian}, {City}, {Phone_Number}";
        }
    }
}