using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRescue.Models
{
    public class Rescuers
    {
        public int ID_Rescuer { get; set; }
        public string Name_Rescuer { get; set; }
        public string Phone_Number { get; set; }

        public Rescuers() { }

        public Rescuers(int id_rescuer, string name_rescuer, string phone_number)
        {
            ID_Rescuer = id_rescuer;
            Name_Rescuer = name_rescuer;
            Phone_Number = phone_number;
        }

        public override string ToString()
        {
            return $"{ID_Rescuer}, {Name_Rescuer}, {Phone_Number}";
        }
    }
}