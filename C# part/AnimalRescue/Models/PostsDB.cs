using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace AnimalRescue.Models
{
    public class PostsDB
    {

         string strCon = ConfigurationManager.ConnectionStrings["Server"].ConnectionString;
        // string strCon = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        public PostsDB()
        {
      
        }


        //ALL POSTS
        public  List<Posts> GetAllPosts()
        {
            List<Posts> pl = new List<Posts>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand("SELECT * FROM Posts order by id_post desc", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Posts p = new Posts(
                    (int)reader["id_post"],
                    (int)reader["id_user"],
                    (string)reader["animal_type"],
                    (string)reader["animal_address"],
                    (decimal)reader["longitude"],
                    (decimal)reader["latitude"],
                    (string)reader["animal_photo"],
                    (string)reader["animal_case"],
                    (string)reader["post_date"],
                    (string)reader["post_status"],
                    (string)reader["city"],
                    (string)reader["phone_number"]
                    );
                pl.Add(p);
            }
            comm.Connection.Close();
            return pl;
        }



        //BY ID_USER(the one who published it)
        //LIST OF POSTS WHO WEERE CREATED BY A SPESIFIC USER
        public  List<Posts> GetAllPostsByUser(int id_user)
        {
            List<Posts> pl = new List<Posts>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Posts " +
                $" WHERE id_user='{id_user}' ", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Posts p = new Posts(
                     (int)reader["id_post"],
                    (int)reader["id_user"],
                    (string)reader["animal_type"],
                    (string)reader["animal_address"],
                    (decimal)reader["longitude"],
                    (decimal)reader["latitude"],
                    (string)reader["animal_photo"],
                    (string)reader["animal_case"],
                    (string)reader["post_date"],
                    (string)reader["post_status"],
                    (string)reader["city"],
                    (string)reader["phone_number"]
                    );
                pl.Add(p);
            }
            comm.Connection.Close();
            return pl;
        }



        //GET POST BY ID_POST
        public  Posts GetPostByID(int id_post)
        {
            Posts p = new Posts();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Posts " +
                $" WHERE id_post='{id_post}' ", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                p = new Posts(
                    (int)reader["id_post"],
                    (int)reader["id_user"],
                    (string)reader["animal_type"],
                    (string)reader["animal_address"],
                    (decimal)reader["longitude"],
                    (decimal)reader["latitude"],
                    (string)reader["animal_photo"],
                    (string)reader["animal_case"],
                    (string)reader["post_date"],
                    (string)reader["post_status"],
                    (string)reader["city"],
                    (string)reader["phone_number"]
                   );
            }
            comm.Connection.Close();
            return p;
        }





        //LIST OF POSTS BY CITY NAME
        public  List<Posts> GetPostsByCity(string city)
        {
            List<Posts> pl = new List<Posts>();
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(
                $" SELECT * FROM Posts " +
                $" WHERE Cast(city AS nvarchar(50) ) = N'{city}' order by id_post desc", con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Posts p = new Posts(
                   (int)reader["id_post"],
                    (int)reader["id_user"],
                    (string)reader["animal_type"],
                    (string)reader["animal_address"],
                    (decimal)reader["longitude"],
                    (decimal)reader["latitude"],
                    (string)reader["animal_photo"],
                    (string)reader["animal_case"],
                    (string)reader["post_date"],
                    (string)reader["post_status"],
                    (string)reader["city"],
                    (string)reader["phone_number"]
                    );
                pl.Add(p);
            }
            comm.Connection.Close();
            return pl;
        }



        //INSERT POST
        public  int InsertPostToDb(Posts val)
        {

            string strComm =
                 $" INSERT INTO Posts(id_user, animal_type, animal_address, longitude, latitude, animal_photo, animal_case, post_date, post_status, city, phone_number) VALUES(" +
                 $" {val.ID_User}," +
                 $" N'{val.Animal_Type}'," +
                 $" N'{val.Animal_Address}'," +
                 $" {val.Longitude}," +
                 $" {val.Latitude}," +
                 $" N'{val.Animal_Photo}'," +
                 $" N'{val.Animal_Case}'," +
                 $" N'{val.Post_Date}'," +
                 $" N'{val.Post_Status}'," +
                 $" N'{val.City}'," +
                 $" N'{val.Phone_Number}'); ";

            strComm +=
                " SELECT SCOPE_IDENTITY() AS[SCOPE_IDENTITY]; ";

            return ExcReaderInsertPost(strComm);
        }



        public  int ExcReaderInsertPost(string comm2Run)
        {
            int PostID = -10;
            SqlConnection con = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(comm2Run, con);
            comm.Connection.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                PostID = int.Parse(reader["SCOPE_IDENTITY"].ToString());
            }
            comm.Connection.Close();
            return PostID;
        }



        //DELETE POST BY ID
        //ONLY THE CREATOR OF THE POST POST CAN DELETE HIS OWN POSTING
        public  int DeletePostByID(int id_post)
        {
            string strComm =
                    $" DELETE Posts " +
                    $" WHERE id_post={id_post}";

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



        //UPDATE POST - the status + date 
        public  int UpdatePost(Posts p)
        {
            string strComm =
                  $" UPDATE Posts SET " +
                  $" post_date='{p.Post_Date}' , " +
                  $" post_status='{p.Post_Status}' , " +
                  $" phone_number='{p.Phone_Number}' " +
                  $" WHERE id_post={p.ID_Post}";

            return ExcNonQ(strComm);
        }


    }
}