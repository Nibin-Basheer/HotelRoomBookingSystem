using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelRoomBookingSystem.Models;

namespace HotelRoomBookingSystem.Models
{
    public class CombinedModel
    {
        public List<UserRegistration> Users { get; set; }
        public List<Rooms> Rooms { get; set; }
        public List<Payment> Payments { get; set; }
    }
}