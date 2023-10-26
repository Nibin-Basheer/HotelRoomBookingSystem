using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace HotelRoomBookingSystem.Models
{
    public class UserRegistration
    {

        [Required(ErrorMessage ="Enter First Name")]
        public string FirstName { set; get; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Select Gender")]
        public string Gender { set; get; }
        [Required(ErrorMessage = "Enter Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { set; get; }

        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Enter valid address")]
       
        public string Email { set; get; }
        [Required(ErrorMessage = "Enter the Address")]
        public string Address { set; get; }
        [Required(ErrorMessage = "Select State")]
        public string State { set; get; }
        [Required(ErrorMessage = "Select City")]

        public string City { set; get; }
        [Required(ErrorMessage = "Enter Password")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }
        public string message { set; get; }
        public List<SelectListItem> States { get; set; } = new List<SelectListItem>
       {
        new SelectListItem { Value = "Kerala", Text = "Kerala" },
        new SelectListItem { Value = "Tamilnadu", Text = "Tamilnadu" },
        
       };
        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>
       {
        new SelectListItem { Value = "Kannur", Text = "Kannur" },
        new SelectListItem { Value = "Chennai", Text = "Chennai" },
        
       };

    }
}