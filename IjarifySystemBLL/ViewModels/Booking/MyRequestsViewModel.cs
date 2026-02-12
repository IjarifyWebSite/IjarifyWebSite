using IjarifySystemBLL.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.Booking
{
    public class MyRequestsViewModel
    {
        public List<BookingReadDto> PendingRequests { get; set; } = new();
        public List<BookingReadDto> ApprovedRequests { get; set; } = new();
        public List<BookingReadDto> RejectedRequests { get; set; } = new();
    }
}
