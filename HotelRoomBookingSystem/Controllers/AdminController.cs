using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelRoomBookingSystem.Models;
using HotelRoomBookingSystem.Repository;


namespace HotelRoomBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        AdminRepository repository = new AdminRepository();
        /// <summary>
        /// Returns a view for admin registration.
        /// </summary>
        /// <returns>ActionResult for admin registration view.</returns>
        public ActionResult AdminRegister()
        {
            AdminRegistration adminregistration = new AdminRegistration();
            return View(adminregistration);
        }
        /// <summary>
        /// Handles the form submission for admin registration.
        /// </summary>
        /// <param name="adminregistration">An instance of AdminRegistration containing admin data.</param>
        /// <returns>Returns a view for admin registration, possibly with a success message.</returns>
        public ActionResult AdminRegisterClick(AdminRegistration adminregistration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.AddAdmin(adminregistration);
                    adminregistration.Message = "Admin Registration Successfully Completed";
                    return View("AdminRegister", adminregistration);
                }

                return View("AdminRegister", adminregistration);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
               
            }
        }
        /// <summary>
        /// Returns the view for admin login.
        /// </summary>
        /// <returns>ActionResult for the admin login view.</returns>
        public ActionResult AdminLogin()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of admin login credentials.
        /// </summary>
        /// <param name="adminlogin">An instance of AdminLogin containing login credentials.</param>
        /// <returns>
        /// If login is valid, redirects to the admin home page; otherwise, displays an error message on the login page.
        /// </returns>
        public ActionResult AdminLoginClick(AdminLogin adminlogin)
        {
            if (ModelState.IsValid)
            {
                bool isValidLogin = repository.LoginAdmin(adminlogin);


                if (isValidLogin)
                {
                    Session["Email"] = adminlogin.Email;
                    return RedirectToAction("AdminHome", "Admin");
                }
                else
                {
                    ModelState.Clear();
                    adminlogin.Message = "invalid Login";
                    return View("AdminLogin", adminlogin);
                }
            }
            return View("AdminLogin", adminlogin);
        }
        /// <summary>
        /// Returns the view for the admin home page.
        /// </summary>
        /// <returns>ActionResult for the admin home view.</returns>

        public ActionResult AdminHome()
        {
            int roomCount = repository.GetRoomCount();
            ViewBag.RoomCount = roomCount;
            return View();
        }
        /// <summary>
        /// Returns the view for adding a new admin.
        /// </summary>
        /// <returns>ActionResult for the admin addition view.</returns>
        public ActionResult AdminAdd()
        {
            return View();
        }
        /// <summary>
        /// Handles the submission of a new admin's information.
        /// </summary>
        /// <param name="adminregistration">An instance of AdminRegistration containing admin details.</param>
        /// <returns>
        /// If the form data is valid, adds the new admin and displays a success message; otherwise, displays the "AdminAdd" view.
        /// </returns>
        public ActionResult AddAdmin(AdminRegistration adminregistration)
        {
            if (ModelState.IsValid)
            {
                repository.AddNewAdmin(adminregistration);
                adminregistration.Message = "New Admin is Added";
                return View("AdminAdd", adminregistration);

            }
            return View("AdminAdd", adminregistration);
        }
        /// <summary>
        /// Logs out the admin user by clearing the session and redirecting to the admin login page.
        /// </summary>
        /// <returns>ActionResult for the admin login page.</returns>
        public ActionResult AdminLogout()
        {
            Session.RemoveAll();
            return RedirectToAction("AdminLogin", "Admin");
        }
        /// <summary>
        /// Displays  list of registered users.
        /// </summary>
        /// <param name="userregistration">An optional UserRegistration object for filtering purposes.</param>
        /// <returns>ActionResult for displaying a list of registered users</returns>
        public ActionResult ViewUsers(UserRegistration userregistration)
        {
            return View(repository.DisplayAllUser(userregistration).ToList());
        }
        /// <summary>
        /// Returns the view for adding a new room.
        /// </summary>
        /// <returns>ActionResult for the room addition view.</returns>
        public ActionResult AddRoom()
        {
            return View();
        }
        /// <summary>
        /// Handles the submission of a new room, including file upload for room image.
        /// </summary>
        /// <param name="rooms">An instance of the Rooms model representing room details.</param>
        /// <param name="file">An HTTPPostedFileBase containing the room image file.</param>
        /// <returns>
        /// If the form data is valid and a file is uploaded, adds the new room with an image and displays a success message; 
        /// otherwise, displays the "AddRoom" view.
        /// </returns>
        [HttpPost]
        public ActionResult AddRoom(Rooms rooms, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    var location = Server.MapPath("~/images");
                    string path = Path.Combine(location, filename);
                    file.SaveAs(path);

                    var fullpath = Path.Combine("~\\images", filename);
                    rooms.RoomImage = fullpath;//set

                }
                repository.AddRoom(rooms);
                rooms.Message = "New Room is Added";
                return View("AddRoom", rooms);

            }
            return View("AddRoom", rooms);
        }
        /// <summary>
        /// Displays a list of all available rooms.
        /// </summary>
        /// <returns>ActionResult for displaying a list of rooms.</returns>
        public ActionResult GetAllRooms()
        {
           
            return View(repository.GetAllRooms());
        }

        public ActionResult EditRoomDetails(int Id)
        {

           
           return View(repository.GetAllRooms().Find(st => st.RoomId == Id));
            

        }
        [HttpPost]

        public ActionResult EditRoomDetails(Rooms rooms, int Id)
        {
            try
            {

                repository.UpdateRoom(rooms, Id);
                return RedirectToAction("GetAllRooms");
            }
            catch
            {
                return View();
            }
        }
        
      
        public ActionResult DeleteRoom(int id)
        {

            var data = repository.GetAllRooms().Find(st => st.RoomId == id);
            if(data==null)
            {
                return RedirectToAction("GetAllRooms");
            }
            return View(data);
        }
       
        public ActionResult DeleteConfirmation(int id)
        {

            repository.DeleteRoom(id);

            return RedirectToAction("GetAllRooms");
        }
        




    }
}
