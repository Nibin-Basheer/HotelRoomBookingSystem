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
            try {
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
            finally
            {
                sqlconnection.Close();
            }





        }
        public bool AddRoom(Rooms rooms)
        {
            SqlCommand sqlcommand = new SqlCommand("AddRoom", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@RoomNumber", rooms.RoomNumber);
            sqlcommand.Parameters.AddWithValue("@RoomImage", rooms.RoomImage);
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
        public List<Rooms> GetAllRooms()
        {
            try {
            List<Rooms> roomList = new List<Rooms>();


            SqlCommand sqlcommand = new SqlCommand("GetRooms",sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(sqlcommand);
            DataTable datatable = new DataTable();
            sqlconnection.Open();
            sqldataadapter.Fill(datatable);
            sqlconnection.Close();


                foreach (DataRow datarow in datatable.Rows)
                {
                    roomList.Add(
                        new Rooms {

                            
                            RoomNumber = Convert.ToString(datarow["RoomNumber"]),
                            RoomImage = Convert.ToString(datarow["RoomImage"]),
                            
                            RoomDescription = Convert.ToString(datarow["RoomDescription"]),
                            PricePerDay = Convert.ToDecimal(datarow["PricePerDay"]),
                            MaximumCapacity = Convert.ToInt32(datarow["MaximumCapacity"]),
                            NumberOfBeds = Convert.ToInt32(datarow["NumberOfBeds"]),
                            Features = Convert.ToString(datarow["RoomFeatures"]),
                            Availablity = Convert.ToString(datarow["Availablity"]),



                        }
                        );
                }
                return roomList;
            
            
            }
            finally
            {
                sqlconnection.Close();
            }




        }

        public bool UpdateRoom(Rooms rooms, int Id)
        {
            SqlCommand sqlcommand = new SqlCommand("UpdateRoom",sqlconnection);
            
            sqlcommand.CommandType = CommandType.StoredProcedure;

            sqlcommand.Parameters.AddWithValue("@RoomId", Id);
            sqlcommand.Parameters.AddWithValue("@RoomNumber", rooms.RoomNumber);
            sqlcommand.Parameters.AddWithValue("@RoomDescription", rooms.RoomDescription);
            sqlcommand.Parameters.AddWithValue("@PricePerDay", rooms.PricePerDay);
            sqlcommand.Parameters.AddWithValue("@MaximumCapacity", rooms.MaximumCapacity);
            sqlcommand.Parameters.AddWithValue("@NumberOfBeds", rooms.NumberOfBeds);
            sqlcommand.Parameters.AddWithValue("@RoomFeatures", rooms.Features);
            sqlcommand.Parameters.AddWithValue("@Availablity", rooms.Availablity);


            sqlconnection.Open();
            int i = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (i > 0)
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