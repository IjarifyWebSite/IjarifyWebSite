using IjarifySystemBLL.ViewModels.Booking;
using IjarifySystemBLL.ViewModels.InquiryViewModels;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.AccountViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ProfileImageUrl { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string WhatsApp { get; set; } = null!;
        public IEnumerable<ReviewItemViewModel> Reviews { get; set; }
        public List<BookingListViewModel> RecentBookings { get; set; } = new List<BookingListViewModel>();
        public int TotalBookings { get; set; }
        public List<InquiryItemViewModel> RecentInquiries { get; set; } = new List<InquiryItemViewModel>();
        public int TotalInquiries { get; set; }
    }
}
