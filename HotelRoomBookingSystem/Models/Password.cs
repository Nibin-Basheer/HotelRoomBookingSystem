using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class Password
    {
            [Required(ErrorMessage = "Enter password")]
            public string Oldpassword { set; get; }
            [Required(ErrorMessage = "Enter password")]
            public string Newpassword { set; get; }
            [Compare("Newpassword", ErrorMessage = "Password mismatch")]
            public string Confirmpassword { set; get; }
            public string Message { set; get; }
        
    }
}