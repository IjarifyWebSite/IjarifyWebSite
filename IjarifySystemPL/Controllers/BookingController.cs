using IjarifySystemBLL.DTOs.Bookings;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.Booking;
using IjarifySystemDAL.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IjarifySystemPL.Controllers
{

    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        // 🔹 TEMP USER (بديل Identity)
        // ===============================
        private int GetCurrentUserId()
        {
            // TODO: Replace with Identity later
            return 4; // user وهمي للتجربة
        }

        //GET: Booking/Create/5
        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateViewModel viewModel)
        {
            Console.WriteLine("=== Form Submitted ===");
            Console.WriteLine($"PropertyID: {viewModel.PropertyID}");
            Console.WriteLine($"Check_In: {viewModel.Check_In}");
            Console.WriteLine($"Check_Out: {viewModel.Check_Out}");
            Console.WriteLine($"TotalPrice: {viewModel.TotalPrice}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("=== ModelState Invalid ===");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                TempData["Error"] = "Please fill all required fields correctly";
                return View(viewModel);
            }

            try
            {
                int userId = GetCurrentUserId();

                var createDto = new BookingCreateDto
                {
                    PropertyID = viewModel.PropertyID,
                    Check_In = viewModel.Check_In,
                    Check_Out = viewModel.Check_Out,
                    TotalPrice = viewModel.TotalPrice
                };

                var booking = await _bookingService.CreateBookingAsync(createDto, userId);

                TempData["Success"] = "Booking created successfully! Waiting for approval.";
                return RedirectToAction("Details", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== Exception: {ex.Message} ===");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                TempData["Error"] = $"Error: {ex.Message}";
                return View(viewModel);
            }
        }

        // POST: Booking/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(BookingCreateViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);
        //    }

        //    try
        //    {
        //        //int userId = 4;
        //        //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //        int userId = GetCurrentUserId();
        //        var createDto = new BookingCreateDto
        //        {
        //            PropertyID = viewModel.PropertyID,
        //            Check_In = viewModel.Check_In,
        //            Check_Out = viewModel.Check_Out,
        //            TotalPrice = viewModel.TotalPrice
        //        };

        //        var booking = await _bookingService.CreateBookingAsync(createDto, userId);

        //        TempData["Success"] = "Booking created successfully! Waiting for approval.";
        //        return RedirectToAction(nameof(Details), new { id = booking.Id });
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(viewModel);
        //    }
        //}

        // GET: Booking/MyBookings
        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            int userId = GetCurrentUserId();
            //int userId = 4;
            //    var userId = User.FindFirst(ClaimTypes.NameIdentifier) is null? 0
            //                                                         : int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var bookings = await _bookingService.GetUserBookingsAsync(userId);

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
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                TempData["Error"] = "Booking not found";
                return RedirectToAction(nameof(MyBookings));
            }
            int userId = GetCurrentUserId();
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (booking.UserID != userId)
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
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                TempData["Error"] = "Booking not found";
                return RedirectToAction(nameof(MyBookings));
            }
            int userId = GetCurrentUserId();
            //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (booking.UserID != userId)
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
