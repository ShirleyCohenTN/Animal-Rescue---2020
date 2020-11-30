using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AnimalRescue.Models
{
    public class FostersDB
    {
        string strCon = ConfigurationManager.ConnectionStrings["Server"].ConnectionString;
        public FostersDB()
        {

        }



        //ALL FOSTERS
        public List<Fosters> GetAllFosters()
        {
            List<Fosters> fl = new List<Fosters>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Fosters", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Fosters f = new Fosters(
                    (int)reader["id_foster"],
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["phone_number"],
                    (string)reader["city"],
                    (string)reader["info"]
                    );
                fl.Add(f);
            }
            comm.Connection.Close();
            return fl;
        }



        //BY ID_USER (the one who published it)
        //LIST OF FOSTERS WHO WEERE CREATED BY A SPESIFIC USER
        //the person who uses the app can publish himself or his friends as fosters
        public List<Fosters> GetAllFostersByUser(int id_user)
        {
            List<Fosters> fl = new List<Fosters>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Fosters " +
                $" WHERE id_user='{id_user}' ", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Fosters f = new Fosters(
                   (int)reader["id_foster"],
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["phone_number"],
                    (string)reader["city"],
                    (string)reader["info"]
                    );
                fl.Add(f);
            }
            comm.Connection.Close();
            return fl;
        }




        //GET FOSTER BY ID_FOSTER
        public Fosters GetFostersByID(int id_foster)
        {
            Fosters f = new Fosters();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Fosters " +
                $" WHERE id_foster='{id_foster}' ", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                f = new Fosters(
                  (int)reader["id_foster"],
                   (int)reader["id_user"],
                   (string)reader["first_name"],
                   (string)reader["last_name"],
                   (string)reader["phone_number"],
                   (string)reader["city"],
                   (string)reader["info"]
                   );
            }
            comm.Connection.Close();
            return f;
        }




        //LIST BY CITY NAME
        public List<Fosters> GetFostersByCity(string city)
        {
            List<Fosters> fl = new List<Fosters>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Fosters " +
                $" WHERE Cast(city AS nvarchar(50) ) = N'{city}' ", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Fosters f = new Fosters(
                   (int)reader["id_foster"],
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["phone_number"],
                    (string)reader["city"],
                    (string)reader["info"]
                    );
                fl.Add(f);
            }
            comm.Connection.Close();
            return fl;
        }



        //INSERT FOSTER
        public int InsertFosterToDb(Fosters val)
        {

            string strComm =
                 $" INSERT INTO Fosters(id_user, first_name, last_name, phone_number, city, info) VALUES(" +
                 $" {val.ID_User}," +
                 $" N'{val.First_Name}'," +
                 $" N'{val.Last_Name}'," +
                 $" N'{val.Phone_Number}'," +
                 $" N'{val.City}'," +
                 $" N'{val.Info}'); ";

            strComm +=
                " SELECT SCOPE_IDENTITY() AS[SCOPE_IDENTITY]; ";

            return ExcReaderInsertFoster(strComm);
        }



        public int ExcReaderInsertFoster(string comm2Run)
        {
            int FosterID = -1;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                FosterID = int.Parse(reader["SCOPE_IDENTITY"].ToString());
            }
            comm.Connection.Close();
            return FosterID;
        }



        //DELETE FOSTER BY ID
        //ONLY THE CREATOR OF THE FOSTER POST CAN DELETE HIS OWN POSTING
        public int DeleteFosterByID(int id_foster)
        {
            string strComm =
                    $" DELETE Fosters " +
                    $" WHERE id_foster={id_foster}";

            return ExcNonQ(strComm);
        }



        private int ExcNonQ(string comm2Run)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            return res;
        }



        //UPDATE FOSTER POST
        public int UpdateFosters(Fosters f)
        {
            string strComm =
                  $" UPDATE Fosters SET " +
                  $" first_name='{f.First_Name}' , " +
                  $" last_name='{f.Last_Name}' , " +
                  $" phone_number='{f.Phone_Number}' , " +
                  $" city='{f.City}' , " +
                  $" info='{f.Info}' " +
                  $" WHERE id_foster={f.ID_Foster}";

            return ExcNonQ(strComm);
        }
    }
}