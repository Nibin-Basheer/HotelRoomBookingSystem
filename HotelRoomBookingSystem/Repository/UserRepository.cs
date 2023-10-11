using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using HotelRoomBookingSystem.Models;
using System.Configuration;

namespace HotelRoomBookingSystem.Repository
{
    public class UserRepository
    {
        private SqlConnection sqlconnection;
        public UserRepository()
        {
            Connection();
        }
        //To handle connection 
        private void Connection()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["GetConnection"].ToString();
            sqlconnection = new SqlConnection(connectionstring);
        }

        public bool AddUser(UserRegistration userregistration)
        {
            SqlCommand sqlcommand = new SqlCommand("AddUsers", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@FirstName", userregistration.FirstName);
            sqlcommand.Parameters.AddWithValue("@LastName", userregistration.LastName);
            sqlcommand.Parameters.AddWithValue("@Dob", userregistration.DateOfBirth);
            sqlcommand.Parameters.AddWithValue("@Gender", userregistration.Gender);
            sqlcommand.Parameters.AddWithValue("@Phone", userregistration.Phone);
            sqlcommand.Parameters.AddWithValue("@Email", userregistration.Email);
            sqlcommand.Parameters.AddWithValue("@Address", userregistration.Address);
            sqlcommand.Parameters.AddWithValue("@State", userregistration.State);
            sqlcommand.Parameters.AddWithValue("@City", userregistration.City);
            sqlcommand.Parameters.AddWithValue("@Password", userregistration.Password);

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
        public bool LoginUser(Login login)
        {
            SqlCommand sqlcommand = new SqlCommand("",sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;

            sqlcommand.Parameters.AddWithValue("@Email", login.Email);
            sqlcommand.Parameters.AddWithValue("@Password", login.password);


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
}