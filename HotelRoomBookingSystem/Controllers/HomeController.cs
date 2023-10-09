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
        /// This is Main home page design
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }




    }
}
