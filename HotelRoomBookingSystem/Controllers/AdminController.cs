using System;
using System.Collections.Generic;
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
        public ActionResult AdminRegister()
        {
            AdminRegistration adminregistration = new AdminRegistration();
            return View(adminregistration);
        }

        public ActionResult AdminRegisterClick(AdminRegistration adminregistration)
        {
            if (ModelState.IsValid)
            {
                repository.AddAdmin(adminregistration);
                adminregistration.Message = "Admin Registration Successfully Completed";
                return View("AdminRegister", adminregistration);

            }
            return View("AdminRegister", adminregistration);
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

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
        public ActionResult AdminHome()
        {
            return View();
        }
        public ActionResult AdminAdd()
        {
            return View();
        }

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
        public ActionResult AdminLogout()
        {
            Session.RemoveAll();
            return RedirectToAction("AdminLogin","Admin");
        }
        public ActionResult ViewUsers(UserRegistration userregistration)
        {
            return View(repository.DisplayAllUser(userregistration).ToList());
        }

        public ActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRoom(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                repository.AddRoom(rooms);
                rooms.Message = "New Room is Added";
                return View("AddRoom", rooms);

            }
            return View("AddRoom", rooms);
        }
    }
}
