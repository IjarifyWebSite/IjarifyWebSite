using IjarifySystemBLL.DTOs.Bookings;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.Booking;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly UserManager<User> _userManager;

        public BookingController(IBookingService bookingService, UserManager<User> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
        }

        // ✅ Helper Method
        private async Task<int?> GetCurrentUserIdAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser?.Id;
        }

        // GET: Booking/Create/5
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int propertyId)
        {
            if (propertyId == 0)
            {
                TempData["Error"] = "Property not found";
                return RedirectToAction("Index", "Property");
            }

            var property = await _bookingService.GetPropertyBasicInfo(propertyId);

            if (property == null)
            {
                TempData["Error"] = "Property not found";
                return RedirectToAction("Index", "Property");
            }

            var viewModel = new BookingCreateViewModel
            {
                PropertyID = propertyId,
                PropertyTitle = property.Title,
                PricePerNight = property.Price,
                Check_In = DateTime.Today.AddDays(1),
                Check_Out = DateTime.Today.AddDays(2),
                TotalPrice = property.Price
            };

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BookingCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill all required fields correctly";
                return View(viewModel);
            }

            try
            {
                var userId = await GetCurrentUserIdAsync();

                if (userId == null)
                {
                    TempData["Error"] = "Please login to continue";
                    return RedirectToAction("Login", "Account");
                }

                var createDto = new BookingCreateDto
                {
                    PropertyID = viewModel.PropertyID,
                    Check_In = viewModel.Check_In,
                    Check_Out = viewModel.Check_Out,
                    TotalPrice = viewModel.TotalPrice
                };

                var booking = await _bookingService.CreateBookingAsync(createDto, userId.Value);

                TempData["Success"] = "Booking created successfully! Waiting for approval.";
                return RedirectToAction("Details", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return View(viewModel);
            }
        }

        // GET: Booking/MyBookings
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyBookings()
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = await _bookingService.GetUserBookingsAsync(userId.Value);

            var viewModel = new MyBookingsViewModel
            {
                UpcomingBookings = bookings
                    .Where(b => b.Status == BookingStatus.Approved && b.Check_In >= DateTime.Now)
                    .Select(MapToListViewModel)
                    .ToList(),

                PastBookings = bookings
                    .Where(b => b.Check_Out < DateTime.Now)
                    .Select(MapToListViewModel)
                    .ToList(),

                PendingBookings = bookings
                    .Where(b => b.Status == BookingStatus.Pending)
                    .Select(MapToListViewModel)
                    .ToList(),

                RejectedBookings = bookings
                    .Where(b => b.Status == BookingStatus.Rejected)
                    .Select(MapToListViewModel)
                    .ToList()
            };

            return View(viewModel);
        }

        // GET: Booking/Details/5
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);

            if (booking == null)
            {
                TempData["Error"] = "Booking not found";
                return RedirectToAction(nameof(MyBookings));
            }

            var userId = await GetCurrentUserIdAsync();

            if (userId == null || booking.UserID != userId.Value)
            {
                TempData["Error"] = "You don't have permission to view this booking";
                return RedirectToAction(nameof(MyBookings));
            }

            var viewModel = MapToDetailsViewModel(booking);
            return View(viewModel);
        }

        // POST: Booking/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);

            if (booking == null)
            {
                TempData["Error"] = "Booking not found";
                return RedirectToAction(nameof(MyBookings));
            }

            var userId = await GetCurrentUserIdAsync();

            if (userId == null || booking.UserID != userId.Value)
            {
                TempData["Error"] = "You don't have permission to cancel this booking";
                return RedirectToAction(nameof(MyBookings));
            }

            if (booking.Status != BookingStatus.Pending)
            {
                TempData["Error"] = "Only pending bookings can be cancelled";
                return RedirectToAction(nameof(Details), new { id });
            }

            await _bookingService.DeleteBookingAsync(id);
            TempData["Success"] = "Booking cancelled successfully";
            return RedirectToAction(nameof(MyBookings));
        }

        // GET: Booking/MyRequests
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyRequests()
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = await _bookingService.GetPropertyOwnerRequestsAsync(userId.Value);
            return View(viewModel);
        }

        // POST: Booking/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var userId = await GetCurrentUserIdAsync();

                if (userId == null)
                {
                    TempData["Error"] = "Please login to continue";
                    return RedirectToAction("Login", "Account");
                }

                await _bookingService.ApproveBookingAsync(id, userId.Value);
                TempData["Success"] = "Booking approved successfully!";
            }
            catch (KeyNotFoundException)
            {
                TempData["Error"] = "Booking not found";
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(MyRequests));
        }

        // POST: Booking/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var userId = await GetCurrentUserIdAsync();

                if (userId == null)
                {
                    TempData["Error"] = "Please login to continue";
                    return RedirectToAction("Login", "Account");
                }

                await _bookingService.RejectBookingAsync(id, userId.Value);
                TempData["Success"] = "Booking rejected successfully!";
            }
            catch (KeyNotFoundException)
            {
                TempData["Error"] = "Booking not found";
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(MyRequests));
        }

        // Mapping Methods
        private BookingListViewModel MapToListViewModel(BookingReadDto dto)
        {
            return new BookingListViewModel
            {
                Id = dto.Id,
                Check_In = dto.Check_In,
                Check_Out = dto.Check_Out,
                Status = dto.Status,
                TotalPrice = dto.TotalPrice,
                IsValid = dto.IsValid,
                PropertyID = dto.PropertyID,
                PropertyTitle = dto.PropertyTitle
            };
        }

        private BookingDetailsViewModel MapToDetailsViewModel(BookingReadDto dto)
        {
            return new BookingDetailsViewModel
            {
                Id = dto.Id,
                Check_In = dto.Check_In,
                Check_Out = dto.Check_Out,
                Status = dto.Status,
                TotalPrice = dto.TotalPrice,
                IsValid = dto.IsValid,
                CreatedAt = dto.CreatedAt,
                PropertyID = dto.PropertyID,
                PropertyTitle = dto.PropertyTitle,
                PropertyAddress = dto.PropertyAddress,
                PropertyType = dto.PropertyType,
                UserID = dto.UserID,
                UserName = dto.UserName,
                UserEmail = dto.UserEmail,
                UserPhone = dto.UserPhone
            };
        }
    }
}