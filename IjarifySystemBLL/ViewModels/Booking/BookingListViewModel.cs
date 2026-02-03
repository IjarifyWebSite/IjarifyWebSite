using IjarifySystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.Booking
{
    public class BookingListViewModel
    {
        public int Id { get; set; }
        public DateTime Check_In { get; set; }
        public DateTime Check_Out { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsValid { get; set; }

        public int PropertyID { get; set; }
        public string PropertyTitle { get; set; } = string.Empty;
        public string PropertyImageUrl { get; set; } = string.Empty;

        public int TotalNights => (Check_Out - Check_In).Days;

        public string StatusText => Status.ToString();

        public string StatusBadgeClass => Status switch
        {
            BookingStatus.Pending => "bg-warning text-dark",
            BookingStatus.Approved => "bg-success",
            BookingStatus.Rejected => "bg-danger",
            _ => "bg-secondary"
        };
    }
}
