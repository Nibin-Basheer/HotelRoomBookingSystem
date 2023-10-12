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
    }
}
