using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelRoomBookingSystem.Models;
using HotelRoomBookingSystem.Repository;
namespace HotelRoomBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        UserRepository repository = new UserRepository();
        /// <summary>
        /// This is Main home page action
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// This is login page action
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// This is register page action
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            UserRegistration userreg = new UserRegistration();
            return View(userreg);
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult RegisterClick(UserRegistration userregistration)
        {
            if (ModelState.IsValid)
            {
                repository.AddUser(userregistration);
                userregistration.message = "User Registration Successfully Completed";
                return View("Register",userregistration);

            }
            return View("Register",userregistration );
        }

        public ActionResult LoginClick(Login login)
        {
            if (ModelState.IsValid)
            {
                bool isValidLogin = repository.LoginUser(login);


            if (isValidLogin)
            {
                Session["Email"] = login.Email;
                return RedirectToAction("UserHome");
            }
            else
            {
                ModelState.Clear();
                login.message = "invalid Login";
                return View("Login", login);
            }
        }
            return View("Login", login);
    }



    }
}
