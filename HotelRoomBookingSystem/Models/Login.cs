using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Enter valid address")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Enter Password")]
        public string password { set; get; }
        public string message { set; get; }

    }
}