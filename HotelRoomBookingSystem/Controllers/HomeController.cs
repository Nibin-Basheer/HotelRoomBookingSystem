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
        AdminRepository adminRepository = new AdminRepository();
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
                Session["UserId"] = login.UserId;
                Session["Email"] = login.Email;
                return RedirectToAction("UserHome","User");
            }
            else
            {
                ModelState.Clear();
                login.Message = "invalid Login";
                return View("Login", login);
            }
        }
            return View("Login", login);
    }
        public JsonResult CheckEmailAvailablity(UserRegistration userregistration,string userData)
        {
            System.Threading.Thread.Sleep(200);
            var searchData = adminRepository.DisplayAllUser(userregistration).Where(x => x.Email == userData).SingleOrDefault();
            if (searchData != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }

        }

        

    }
}
