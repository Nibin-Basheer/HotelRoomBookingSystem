using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelRoomBookingSystem.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }




    }
}
