using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.Booking
{
    public class BookingCreateViewModel
    {
        [Required(ErrorMessage = "Property is required")]
        public int PropertyID { get; set; }

        [Required(ErrorMessage = "Check-in date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Check-In Date")]
        public DateTime Check_In { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Check-Out Date")]
        public DateTime Check_Out { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        // For display purposes
        public string? PropertyTitle { get; set; }
        public decimal? PricePerNight { get; set; }
        public int TotalNights => (Check_Out - Check_In).Days;
    }
}
