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
            try
            {
                SqlCommand sqlcommand = new SqlCommand("AddUsers", sqlconnection);
                sqlcommand.CommandType = CommandType.StoredProcedure;

                //input
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
            catch(Exception ex)
            {
               
                throw new Exception("Error while adding a user.", ex);
            }
        }
        public bool LoginUser(Login login)
        {
            try
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
                if (output ==1)
                {
                    SqlCommand sqlcommand1 = new SqlCommand("GetUserId", sqlconnection);
                    sqlcommand1.CommandType = CommandType.StoredProcedure;

                    sqlcommand1.Parameters.AddWithValue("@Email", login.Email);
                    sqlcommand1.Parameters.AddWithValue("@Password", login.Password);
                    sqlconnection.Open();
                    int value=sqlcommand1.ExecuteNonQuery();
                    sqlconnection.Close();
                    login.UserId = value;
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

        public bool PasswordChange(Password password)
        {
            // Retrieve the email from the session
            string userEmail = HttpContext.Current.Session["Email"].ToString();
            try
            {
                if (!string.IsNullOrEmpty(userEmail))
                {
                    SqlCommand sqlcommand = new SqlCommand("ChangePassword", sqlconnection);
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.Parameters.AddWithValue("@Email", userEmail);
                    sqlcommand.Parameters.AddWithValue("@Oldpassword", password.Oldpassword);
                    sqlcommand.Parameters.AddWithValue("@NewPassword", password.Newpassword);

        
                    sqlconnection.Open();
                    int value=sqlcommand.ExecuteNonQuery();
                    
                    if (value > 0)
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
            catch (Exception ex)
            {
                throw new Exception("Error while changing the password.", ex);
            }
            finally
            {
                sqlconnection.Close();

            }
        }
        public bool UserProfile(UserProfile userprofile)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving the user profile.", ex);
            }
            finally
            {
                sqlconnection.Close();
            }


        }
        public bool EditProfile(UserProfile userprofile)
        {
            
            string userEmail = HttpContext.Current.Session["Email"].ToString();
            try
            {
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
              
                if (value > 0)
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
                throw new Exception("Error while editing the user profile.", ex);
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        public bool AddBooking(Rooms rooms,int id)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                SqlCommand sqlcommand = new SqlCommand("RoomBooking", sqlconnection);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@UserId", userId);
                sqlcommand.Parameters.AddWithValue("@RoomId", id);
                sqlcommand.Parameters.AddWithValue("CheckinDate",rooms.CheckinDate);
                sqlcommand.Parameters.AddWithValue("CheckoutDate",rooms.CheckoutDate);
                sqlcommand.Parameters.AddWithValue("Adult",rooms.Adult);
                sqlcommand.Parameters.AddWithValue("Children",rooms.Children);

                sqlconnection.Open();
                int value = sqlcommand.ExecuteNonQuery();
                if(value>0)
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

                throw new Exception("Error while booking a room", ex);
            }
            finally
            {
                sqlconnection.Close();
            }
        }
        public CombinedModel GetBookingDetails()
        {
            CombinedModel combinedData = new CombinedModel
            {
                Users = new List<UserRegistration>(),
                Rooms = new List<Rooms>(),
                Payments = new List<Payment>()
            };
            string userEmail = HttpContext.Current.Session["Email"].ToString();
            //join query to join 3 tables
            string Query = @"select u.FirstName, u.Email, booking.CheckinDate, booking.CheckoutDate, booking.Adult,
            booking.Children,payment.PaymentDate,payment.PaymentAmount,payment.PaymentMethod
            from Tbl_Users as u join Tbl_RoomBookings as booking on u.UserId = booking.UserId join Tbl_Payments as payment 
            on u.UserId = payment.UserId where u.Email='"+userEmail+"'";

            SqlCommand sqlcommand = new SqlCommand(Query, sqlconnection);
            sqlconnection.Open();
            SqlDataReader sqldatareader = sqlcommand.ExecuteReader();
            while (sqldatareader.Read())
            {
                combinedData.Users.Add(new UserRegistration
                {
                    FirstName = Convert.ToString(sqldatareader["FirstName"]),
                    Email = Convert.ToString(sqldatareader["Email"]),
                });
                combinedData.Rooms.Add(new Rooms
                {
                    CheckinDate = Convert.ToDateTime(sqldatareader["CheckinDate"]),
                    CheckoutDate = Convert.ToDateTime(sqldatareader["CheckoutDate"]),
                    Adult = Convert.ToInt32(sqldatareader["Adult"]),
                    Children = Convert.ToInt32(sqldatareader["children"]),

                });
                combinedData.Payments.Add(new Payment
                {
                    PaymentDate = Convert.ToDateTime(sqldatareader["PaymentDate"]),
                    PaymentAmount = Convert.ToDecimal(sqldatareader["PaymentAmount"]),
                    PaymentMethod = Convert.ToString(sqldatareader["PaymentMethod"])
                });
            }

            return combinedData;

        }
        public bool AddPayment(Payment payment, int id)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                SqlCommand sqlcommand = new SqlCommand("Payment", sqlconnection);
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@UserId", userId);
                sqlcommand.Parameters.AddWithValue("@RoomId", id);
                sqlcommand.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                sqlcommand.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
                sqlcommand.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
             

                sqlconnection.Open();
                int value = sqlcommand.ExecuteNonQuery();
                if (value > 0)
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

                throw new Exception("Error while making payment", ex);
            }
            finally
            {
                sqlconnection.Close();
            }
        }


    }
}