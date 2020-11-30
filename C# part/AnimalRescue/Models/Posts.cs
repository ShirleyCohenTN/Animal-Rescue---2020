using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRescue.Models
{
    public class Posts
    {
        public int ID_Post { get; set; }
        public int ID_User { get; set; }
        public string Animal_Type { get; set; }
        public string Animal_Address { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Animal_Photo { get; set; }
        public string Animal_Case { get; set; }
        public string Post_Date { get; set; }
        public string Post_Status { get; set; }
        public string City { get; set; }
        public string Phone_Number { get; set; }


        public Posts() { }

        public Posts(int id_post, int id_user, string animal_type, string animal_address,
           decimal longitude, decimal latitude, string animal_photo, string animal_case,
           string post_date, string post_status, string city, string phone_number)
        {
            ID_Post = id_post;
            ID_User = id_user;
            Animal_Type = animal_type;
            Animal_Address = animal_address;
            Longitude = longitude;
            Latitude = latitude;
            Animal_Photo = animal_photo;
            Animal_Case = animal_case;
            Post_Date = post_date;
            Post_Status = post_status;
            City = city;
            Phone_Number = phone_number;
        }

        public override string ToString()
        {
            return $"{ID_Post}, {ID_User}, {Animal_Type}, {Animal_Address}, {Longitude}," +
                $"{Latitude}, {Animal_Photo}, {Animal_Case}, {Post_Date}, {Post_Status}, {City}, {Phone_Number}";
        }
    }
}