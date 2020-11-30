using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AnimalRescue.Models
{
    public class RescuersDB
    {
         string strCon = ConfigurationManager.ConnectionStrings["Server"].ConnectionString;
        public RescuersDB()
        {

        }

        //LIST ALL
        public  List<Rescuers> GetAllRescuers()
        {
            List<Rescuers> rl = new List<Rescuers>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Rescuers", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Rescuers r = new Rescuers(
                    (int)reader["id_rescuer"],
                    (string)reader["name_rescuer"],
                    (string)reader["phone_number"]
                    );
                rl.Add(r);
            }
            comm.Connection.Close();
            return rl;
        }



       
    }
}