using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.Booking
{
    public class MyBookingsViewModel
    {
        public List<BookingListViewModel> UpcomingBookings { get; set; } = new();
        public List<BookingListViewModel> PastBookings { get; set; } = new();
        public List<BookingListViewModel> PendingBookings { get; set; } = new();
        public List<BookingListViewModel> RejectedBookings { get; set; } = new();
    }
}
