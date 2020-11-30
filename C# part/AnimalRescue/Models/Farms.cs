using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRescue.Models
{
    public class Farms
    {
        public int ID_Farm { get; set; }
        public string Farm_Name { get; set; }
        public string Farm_Address { get; set; }
        public string Farm_Photo { get; set; }
        public string Website_Link { get; set; }
        public string Phone_Number { get; set; }

        public Farms() { }

        public Farms(int id_farm, string farm_name, string farm_address, 
            string farm_photo, string website_link, string phone_number)
        {
            ID_Farm = id_farm;
            Farm_Name = farm_name;
            Farm_Address = farm_address;
            Farm_Photo = farm_photo;
            Website_Link = website_link;
            Phone_Number = phone_number;
        }


        public override string ToString()
        {
            return $"{ID_Farm}, {Farm_Name}, {Farm_Address}, {Farm_Photo}, {Website_Link}," +
                $" {Phone_Number}";
        }

    }
}