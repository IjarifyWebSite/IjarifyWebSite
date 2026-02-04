using IjarifySystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.DTOs.Bookings
{
    public class BookingReadDto
    {
        public int Id { get; set; }
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreatedAt { get; set; }

        // Property Info
        public int PropertyID { get; set; }
        public string PropertyTitle { get; set; } = string.Empty;
        public string PropertyAddress { get; set; } = string.Empty;
        public PropertyType? PropertyType { get; set; }

        // User Info
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
    }
}
