using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.DTOs.Bookings
{
    public class BookingCreateDto
    {
        [Required]
        public int PropertyID { get; set; }

        [Required]
        public DateTime Check_In { get; set; }

        [Required]
        public DateTime Check_Out { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal TotalPrice { get; set; }
    }
}
