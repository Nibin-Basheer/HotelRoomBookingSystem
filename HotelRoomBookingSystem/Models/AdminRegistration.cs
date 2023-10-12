using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class AdminRegistration
    {
        [Required(ErrorMessage = "Enter Name")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Enter valid address")]
        public string Email { get; set; }
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }
        public string Message { set; get; }

    }
}