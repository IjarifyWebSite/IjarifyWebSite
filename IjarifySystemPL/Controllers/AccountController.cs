using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IjarifySystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookingService _bookingService; // ⬅️ أضف دي

        public AccountController(IReviewService reviewService, IBookingService bookingService)
        {
            _reviewService = reviewService;
            _bookingService = bookingService; 
        }

        // 🔹 Helper Method - نفس GetCurrentUserId من BookingController
        private int GetCurrentUserId()
        {
            return 4; // نفس الـ user اللي في BookingController
        }

        [HttpGet]
        public async Task<IActionResult> Profile() 
        {
            int userId = GetCurrentUserId();

            // جلب الـ Reviews
            var reviews = _reviewService.GetReviewsByUser(userId);

            // ⬇️ جلب الـ Bookings ⬇️
            var allBookings = await _bookingService.GetUserBookingsAsync(userId);

            var profile = new ProfileViewModel
            {
                FullName = "User Name",
                Email = "user@example.com",
                Address = "Cairo, Egypt",
                ProfileImageUrl = "/assets/img/real-estate/agent-1.webp",
                PhoneNumber = "+201234567890",
                WhatsApp = "+201234567890",
                Reviews = reviews,

                // ⬇️ أضف الـ Bookings دي ⬇️
                RecentBookings = allBookings
                    .OrderByDescending(b => b.Check_In)
                    .Take(6)
                    .Select(b => new IjarifySystemBLL.ViewModels.Booking.BookingListViewModel
                    {
                        Id = b.Id,
                        PropertyTitle = b.PropertyTitle,
                        Check_In = b.Check_In,
                        Check_Out = b.Check_Out,
                        TotalPrice = b.TotalPrice,
                        Status = b.Status,
                        PropertyID = b.PropertyID,
                        IsValid = b.IsValid
                    })
                    .ToList(),
                TotalBookings = allBookings.Count()
            };

            return View(profile);
        }
    }
}