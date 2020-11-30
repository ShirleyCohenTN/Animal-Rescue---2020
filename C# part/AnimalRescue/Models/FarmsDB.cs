using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;



namespace AnimalRescue.Models
{
    public class FarmsDB
    {
         string strCon = ConfigurationManager.ConnectionStrings["Server"].ConnectionString;

        public FarmsDB()
        {

        }


        //ALL FARMS
        public  List<Farms> GetAllFarms()
        {
            List<Farms> fl = new List<Farms>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Farms", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Farms f = new Farms(
                    (int)reader["id_farm"],
                    (string)reader["farm_name"],
                    (string)reader["farm_address"],
                    (string)reader["farm_photo"],
                    (string)reader["website_link"],
                    (string)reader["phone_number"]
                    );
                fl.Add(f);
            }
            comm.Connection.Close();
            return fl;
        }
    }
}