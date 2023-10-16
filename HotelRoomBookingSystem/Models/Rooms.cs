using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class Rooms
    {
        public string RoomNumber { set; get; }
        public string RoomImage { set; get; }
        public string RoomType { set; get; }
        public string RoomDescription { set; get; }
        public decimal PricePerDay { set; get; }
        public int MaximumCapacity { set; get; }
        public int NumberOfBeds { set; get; }
        public string Features { set; get; }
        public string Availablity { set; get; }
        public string Message { set; get; }


    }
}