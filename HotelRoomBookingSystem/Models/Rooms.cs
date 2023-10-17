using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelRoomBookingSystem.Models
{
    public class Rooms
    {
        [Required(ErrorMessage = "Room Number is required")]
        [StringLength(10, ErrorMessage = "Room Number must be at most 10 characters")]
        public string RoomNumber { get; set; }
        public string RoomImage { get; set; }

        [Required(ErrorMessage = "Room Type is required")]
        public string RoomType { get; set; }

        [Required(ErrorMessage = "Room Description is required")]
        [StringLength(255, ErrorMessage = "Description must be at most 255 characters")]
        public string RoomDescription { get; set; }

        [Required(ErrorMessage = "Price per Day is required")]
        [Range(100, 10000, ErrorMessage = "Price must be between 100 and 10,000")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Maximum Capacity is required")]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int MaximumCapacity { get; set; }

        [Required(ErrorMessage = "Number of Beds is required")]
        [Range(1, 5, ErrorMessage = "Number of Beds must be between 1 and 5")]
        public int NumberOfBeds { get; set; }

        public string Features { get; set; }

        [Required(ErrorMessage = "Availability is required")]
        public string Availablity { get; set; }

        public string Message { get; set; }
    }
}