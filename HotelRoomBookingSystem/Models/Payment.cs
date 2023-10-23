using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class Payment
    {
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}