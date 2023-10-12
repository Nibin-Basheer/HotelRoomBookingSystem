﻿using System;
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
            SqlCommand sqlcommand = new SqlCommand("LoginUser", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;

            sqlcommand.Parameters.AddWithValue("@Email", login.Email);
            sqlcommand.Parameters.AddWithValue("@Password", login.Password);


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

        public bool PasswordChange(Password password)
        {
            // Retrieve the email from the session
            string userEmail = HttpContext.Current.Session["Email"].ToString();

            if (!string.IsNullOrEmpty(userEmail))
            {
                SqlCommand sqlcommand = new SqlCommand("ChangePassword", sqlconnection);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@Email", userEmail);
                sqlcommand.Parameters.AddWithValue("@Oldpassword", password.Oldpassword);
                sqlcommand.Parameters.AddWithValue("@NewPassword", password.Newpassword);

                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter.DbType = DbType.Int32;
                sqlparameter.ParameterName = "@status";
                sqlparameter.Direction = ParameterDirection.Output;

                sqlcommand.Parameters.Add(sqlparameter);
                sqlconnection.Open();
                sqlcommand.ExecuteNonQuery();
                sqlconnection.Close();
                int output = Convert.ToInt32(sqlparameter.Value);

                if (output > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
       
                return false;
            }
        }
        public bool UserProfile(UserProfile userprofile)
        {
            string userEmail = HttpContext.Current.Session["Email"].ToString();
            SqlCommand sqlcommand = new SqlCommand("UserProfile", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@Email", userEmail);
            sqlconnection.Open();
            SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
            if (sqldatareader.Read())
            {
                userprofile.FirstName = sqldatareader["FirstName"].ToString();
                userprofile.LastName = sqldatareader["LastName"].ToString();
                userprofile.Gender = sqldatareader["Gender"].ToString();
                userprofile.Address = sqldatareader["Address"].ToString();
                userprofile.Phone = sqldatareader["Phone"].ToString();

                return true;
            }
            else
            {
                return false;
            }



        }
        public bool EditProfile(UserProfile userprofile)
        {
            string userEmail = HttpContext.Current.Session["Email"].ToString();
            SqlCommand sqlcommand = new SqlCommand("EditProfile", sqlconnection);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            sqlcommand.Parameters.AddWithValue("@Email", userEmail);
            sqlcommand.Parameters.AddWithValue("@FirstName", userprofile.FirstName);
            sqlcommand.Parameters.AddWithValue("@LastName", userprofile.LastName);
            sqlcommand.Parameters.AddWithValue("@Gender", userprofile.Gender);
            sqlcommand.Parameters.AddWithValue("@Phone", userprofile.Phone);
            sqlcommand.Parameters.AddWithValue("@Address", userprofile.Address);
           

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