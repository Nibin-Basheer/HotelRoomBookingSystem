using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class UserProfile
    {
        public string FirstName { set; get; }
      
        public string LastName { set; get; }
        public string Gender { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Message { set; get; }
    }
}