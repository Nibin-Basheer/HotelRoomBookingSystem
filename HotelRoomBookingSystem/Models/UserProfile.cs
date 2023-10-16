using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class UserProfile
    {
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { set; get; }
        [Required(ErrorMessage = "Enter Last Name")]

        public string LastName { set; get; }
        [Required(ErrorMessage = "Enter Gender")]
        public string Gender { set; get; }
        [Required(ErrorMessage = "Enter Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { set; get; }
        [Required(ErrorMessage = "Enter the Address")]
        public string Address { set; get; }
        public string Message { set; get; }
    }
}