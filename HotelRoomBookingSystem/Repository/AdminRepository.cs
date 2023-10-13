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
    }
}