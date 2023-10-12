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
    }
}