using HotelRoomBookingSystem.Models;
using HotelRoomBookingSystem.Repository;
using System;
using System.Web.Mvc;

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
            ModelState.Clear();
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
                    return View("ProfileLoad",userprofile);
                }

                return View("ProfileLoad");
            }
            catch (Exception ex)
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
            var room = adminrespository.GetAllRooms().Find(r => r.RoomId == id);

            if (room != null)
            {
                return View(room);
            }


            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult CheckinRoom(Rooms rooms)
        {
            var room = adminrespository.GetAllRooms().Find(r => r.RoomId == rooms.RoomId);
            if (ModelState.IsValid && room != null)
            {
                
                repository.AddBooking(rooms, rooms.RoomId);
                return RedirectToAction("PaymentPage");
            }

            return View(room);
        }
        public ActionResult PaymentPage(Payment payment, int id)
        {

            try
            {
                repository.AddPayment(payment, id);
                return RedirectToAction("UserHome");
            }

                
            catch (Exception ex)
            {
                return View();
                throw new Exception("Error", ex);
            }
        }
        public ActionResult GetBookingDetails()
        {

            return View(repository.GetBookingDetails());
        }

    }
}
