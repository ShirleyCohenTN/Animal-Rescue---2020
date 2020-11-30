using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AnimalRescue.Models
{
    public class UsersDB
    {
        string strCon = ConfigurationManager.ConnectionStrings["Server"].ConnectionString;
        // string strCon = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        public UsersDB()
        {

        }


        //ALL USERS
        public List<Users> GetAllUsers()
        {
            List<Users> ul = new List<Users>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Users", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Users u = new Users(
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["phone_number"],
                    (string)reader["token"],
                    (decimal)reader["lat"],
                    (decimal)reader["lng"]
                    );
                ul.Add(u);
            }
            comm.Connection.Close();
            return ul;
        }



        //BY EMAIL AND PASS
        public Users GetUserByEmailAndPassword(string email_address, string pass)
        {
            Users u = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Users " +
                $" WHERE email_address='{email_address}' AND pass='{pass}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                u = new Users(
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["phone_number"],
                    (string)reader["token"],
                    (decimal)reader["lat"],
                    (decimal)reader["lng"]
                    );
            }
            comm.Connection.Close();
            return u;
        }


        //BY EMAIL
        public Users GetUserByEmail(string email_address)
        {
            Users u = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Users " +
                $" WHERE email_address='{email_address}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                u = new Users(
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["phone_number"],
                    (string)reader["token"],
                    (decimal)reader["lat"],
                    (decimal)reader["lng"]
                    );
            }
            comm.Connection.Close();
            return u;
        }




        //BY ID
        public Users GetUserByID(int id)
        {
            Users u = null;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Users " +
                $" WHERE id_user='{id}'", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                u = new Users(
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["phone_number"],
                    (string)reader["token"],
                    (decimal)reader["lat"],
                    (decimal)reader["lng"]
                    );
            }
            comm.Connection.Close();
            return u;
        }

        //INSERT USER
        public  int InsertUserToDb(Users val)
        {
            //in case thers already a user with such email
            if (GetUserByEmail(val.Email_Address) != null) return -1;

            string strComm =
                 $" INSERT INTO Users(first_name, last_name, email_address, pass, phone_number) VALUES(" +
                 $" N'{val.First_Name}'," +
                 $" N'{val.Last_Name}'," +
                 $" N'{val.Email_Address}'," +
                 $" N'{val.Pass}'," +
                 $" N'{val.Phone_Number}'); ";

            strComm +=
                " SELECT SCOPE_IDENTITY() AS[SCOPE_IDENTITY]; ";

            return ExcReaderInsertUser(strComm);
        }



        public  int ExcReaderInsertUser(string comm2Run)
        {
            int UserID = -1;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                UserID = int.Parse(reader["SCOPE_IDENTITY"].ToString());
            }
            comm.Connection.Close();
            return UserID;
        }





        //DELETE BY ID
        public  int DeleteUserByID(int id)
        {
            string strComm =
                    $" DELETE Users " +
                    $" WHERE id_user={id}";

            return ExcNonQ(strComm);
        }





        //UPDATE User - token + lat + lng 
        public  int UpdateUser(Users p)
        {
            string strComm =
                  $" UPDATE Users SET " +
                  $" token='{p.Token}' , " +
                  $" lat='{p.Lat}' , " +
                  $" lng='{p.Lng}' " +
                  $" WHERE id_user={p.ID_User}";

            return ExcNonQ(strComm);
        }


        private  int ExcNonQ(string comm2Run)
        {
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();
            return res;
        }


        public  List<Users> ExcReader(string comm2Run)
        {
            List<Users> ul = new List<Users>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Users u = new Users(
                    (int)reader["id_user"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email_address"],
                    (string)reader["pass"],
                    (string)reader["phone_number"],
                    (string)reader["token"],
                    (decimal)reader["lat"],
                    (decimal)reader["lng"]
                    );
                ul.Add(u);
            }
            comm.Connection.Close();
            return ul;
        }
    }
}
