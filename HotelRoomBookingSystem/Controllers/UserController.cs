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
        AdminRepository adminrespository = new AdminRepository();
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
            try
            {
                if (ModelState.IsValid)
                {
                    repository.EditProfile(userprofile);

                    userprofile.Message = "Updated...!";
                    return View("ProfileLoad");
                }

                return View("ProfileLoad");
            }
            catch(Exception ex)
            {
                throw new Exception("Error while editing profile", ex);
            }
           
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult ViewRooms()
        {

            ModelState.Clear();
            return View(adminrespository.GetAllRooms());
        }
        public ActionResult CheckinRoom(int id)
        {
            return View(adminrespository.GetAllRooms().Find(room=>room.RoomId==id));
        }
        [HttpPost]
        public ActionResult RoomBooking(Rooms rooms, int id)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    adminrespository.GetAllRooms().Find(room => room.RoomId == id);
                    repository.AddBooking(rooms, id);
                    return RedirectToAction("UserHome");
                }

                return RedirectToAction("CheckinRoom");
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

    }
}
