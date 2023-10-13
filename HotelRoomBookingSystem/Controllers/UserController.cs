using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelRoomBookingSystem.Models;
using HotelRoomBookingSystem.Repository;

namespace HotelRoomBookingSystem.Controllers
{
    public class UserController : Controller
    {
        UserRepository repository = new UserRepository();
        public ActionResult UserHome()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();

        }

        public ActionResult ChangePasswordClick(Password password)
        {
            if (ModelState.IsValid)
            {

                bool Isvalid = repository.PasswordChange(password);

                if (Isvalid == true)
                {
                    password.Message = "Password changed";

                }
                else
                {
                    password.Message = "Invalid password";

                }
            }
            return View("ChangePassword", password);
        }

        public ActionResult ProfileLoad(UserProfile userprofile)
        {

            repository.UserProfile(userprofile);

            return View(new UserProfile
            {
                FirstName = userprofile.FirstName,
                LastName = userprofile.LastName,
                Gender = userprofile.Gender,
                Address = userprofile.Address,
                Phone = userprofile.Phone

            });

        }
        public ActionResult ProfileEdit(UserProfile userprofile)
        {

            repository.EditProfile(userprofile);

            userprofile.Message = "Updated...!";

            return View("ProfileLoad", new UserProfile
            {
                FirstName = userprofile.FirstName,
                LastName = userprofile.LastName,
                Gender = userprofile.Gender,
                Address = userprofile.Address,
                Phone = userprofile.Phone

            });

        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }
    }
}
