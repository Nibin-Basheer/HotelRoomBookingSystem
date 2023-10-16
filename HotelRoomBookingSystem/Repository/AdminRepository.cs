using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HotelRoomBookingSystem.Models;

namespace HotelRoomBookingSystem.Repository
{
    public class AdminRepository
    {
        private SqlConnection sqlconnection;
        public AdminRepository()
        {
            Connection();
        }
        //To handle connection 
        private void Connection()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["GetConnection"].ToString();
            sqlconnection = new SqlConnection(connectionstring);
        }

        public bool AddAdmin(AdminRegistration adminregistration)
        {
            SqlCommand sqlcommand = new SqlCommand("AddAdmin", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@Name", adminregistration.Name);
            sqlcommand.Parameters.AddWithValue("@Email", adminregistration.Email);
            sqlcommand.Parameters.AddWithValue("@Password", adminregistration.Password);
            
            sqlconnection.Open();
            int value = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool LoginAdmin(AdminLogin adminlogin)
        {
            try
            {
                SqlCommand sqlcommand = new SqlCommand("LoginAdmin", sqlconnection);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                sqlcommand.Parameters.AddWithValue("@Email", adminlogin.Email);
                sqlcommand.Parameters.AddWithValue("@Password", adminlogin.Password);


                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter.DbType = DbType.Int32;
                sqlparameter.ParameterName = "@status";
                sqlparameter.Direction = ParameterDirection.Output;


                sqlcommand.Parameters.Add(sqlparameter);
                sqlconnection.Open();
                sqlcommand.ExecuteNonQuery();
                sqlconnection.Close();
                int output = Convert.ToInt32(sqlparameter.Value);
                if (output == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error while logging in.", ex);
            }

        }
        public bool AddNewAdmin(AdminRegistration adminregistration)
        {
            SqlCommand sqlcommand = new SqlCommand("AddAdmin", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@Name", adminregistration.Name);
            sqlcommand.Parameters.AddWithValue("@Email", adminregistration.Email);
            sqlcommand.Parameters.AddWithValue("@Password", adminregistration.Password);

            sqlconnection.Open();
            int value = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<UserRegistration> DisplayAllUser(UserRegistration useregistration)
        {
            List<UserRegistration> userlist = new List<UserRegistration>();
            SqlCommand sqlcommand = new SqlCommand("DisplayAllUser", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlconnection.Open();
            SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
            while(sqldatareader.Read())
            {
                UserRegistration userdata= new UserRegistration
                {
                    FirstName = sqldatareader["FirstName"].ToString(),
                    LastName = sqldatareader["LastName"].ToString(),
                    DateOfBirth = sqldatareader.GetDateTime(sqldatareader.GetOrdinal("Dob")),
                    Gender=sqldatareader["Gender"].ToString(),
                    Phone = sqldatareader["Phone"].ToString(),
                    Email = sqldatareader["Email"].ToString(),
                    Address = sqldatareader["Address"].ToString(),
                    State = sqldatareader["State"].ToString(),
                    City = sqldatareader["City"].ToString(),
                    Password = sqldatareader["Password"].ToString(),

                    
                };
                userlist.Add(userdata);
            }
            return userlist;
            

          

        }
        public bool AddRoom(Rooms rooms)
        {
            SqlCommand sqlcommand = new SqlCommand("AddRoom", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@RoomNumber", rooms.RoomNumber);
            sqlcommand.Parameters.AddWithValue("@RoomDescription", rooms.RoomDescription);
            sqlcommand.Parameters.AddWithValue("@PricePerDay", rooms.PricePerDay);
            sqlcommand.Parameters.AddWithValue("@MaximumCapacity", rooms.MaximumCapacity);
            sqlcommand.Parameters.AddWithValue("@NumberOfBeds", rooms.NumberOfBeds);
            sqlcommand.Parameters.AddWithValue("@RoomFeatures", rooms.Features);
            sqlcommand.Parameters.AddWithValue("@Availablity", rooms.Availablity);
            sqlconnection.Open();
            int value = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}