using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AnimalRescue.Models
{
    public class VeterinariansDB
    {
         string strCon = ConfigurationManager.ConnectionStrings["Server"].ConnectionString;
        public VeterinariansDB()
        {

        }


        //LIST ALL
        public  List<Veterinarians> GetAllVeterinarians()
        {
            List<Veterinarians> vl = new List<Veterinarians>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Veterinarians", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Veterinarians v = new Veterinarians(
                    (int)reader["id_veterinarian"],
                    (string)reader["name_veterinarian"],
                    (string)reader["city"],
                    (string)reader["phone_number"]
                    );
                vl.Add(v);
            }
            comm.Connection.Close();
            return vl;
        }



        //LIST BY CITY NAME
        public  List<Veterinarians> GetVeterinariansByCity(string city)
        {
            List<Veterinarians> vl = new List<Veterinarians>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Veterinarians " +
                $" WHERE Cast(city AS nvarchar(50) ) = N'{city}' ", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Veterinarians v = new Veterinarians(
                    (int)reader["id_veterinarian"],
                    (string)reader["name_veterinarian"],
                    (string)reader["city"],
                    (string)reader["phone_number"]
                    );
                vl.Add(v);
            }
            comm.Connection.Close();
            return vl;
        }

    }
}