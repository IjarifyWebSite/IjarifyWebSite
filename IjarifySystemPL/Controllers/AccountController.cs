using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IjarifySystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string _imagePath;

        public AccountController(IReviewService reviewService, IBookingService bookingService, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _reviewService = reviewService;
            _bookingService = bookingService;
            _userService = userService;
            this.webHostEnvironment = webHostEnvironment;

            _imagePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", "profiles");

            if (!Directory.Exists(_imagePath))
            {
                Directory.CreateDirectory(_imagePath);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile() 
        {
            int userId = 3;
            var user = _userService.GetUserById(userId);

            var reviews = _reviewService.GetReviewsByUser(userId);

            var allBookings = await _bookingService.GetUserBookingsAsync(userId);

            var profile = new ProfileViewModel
            {
                FullName = user.Name,
                Email = user.Email,
                Address = user.Address ?? "Cairo, Egypt",
                ProfileImageUrl = user.ImageUrl ?? "/images/default-avatar.jpg",
                PhoneNumber = user.Phone,
                WhatsApp = user.Phone,
                Reviews = reviews,
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

        [HttpGet]
        public IActionResult EditProfile()
        {
            int userId = 3;

            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditProfileViewModel
            {
                FullName = user.Name,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.Phone,
                WhatsApp = user.Phone,
                ProfileImageUrl = user.ImageUrl ?? "/images/default-avatar.jpg"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            int userId = 3;

            try
            {
                string? newImagePath = null;

                // Handle image upload
                if (editModel.ProfileImage != null)
                {
                    // Delete old image if exists
                    var user = _userService.GetUserById(userId);
                    if (user != null && !string.IsNullOrEmpty(user.ImageUrl))
                    {
                        DeleteImageFile(user.ImageUrl);
                    }

                    newImagePath = await SaveProfileImageAsync(editModel.ProfileImage);
                }

                bool isUpdated = _userService.UpdateUserProfile(editModel, userId, newImagePath);

                if (isUpdated)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("Profile");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to update profile. Please try again.";
                    return View(editModel);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return View(editModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteProfileImage()
        {
            int userId = 3;

            try
            {
                var user = _userService.GetUserById(userId);
                if (user != null && !string.IsNullOrEmpty(user.ImageUrl))
                    DeleteImageFile(user.ImageUrl);

                bool isDeleted = _userService.DeleteProfileImage(userId);

                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Profile image removed successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to remove profile image.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToAction("EditProfile");
        }

        #region Helper Methods
        private async Task<string> SaveProfileImageAsync(IFormFile image)
        {
            try
            {
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(_imagePath, imageName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                return $"/Images/profiles/{imageName}";
            }
            catch
            {
                return null;
            }
        }

        private void DeleteImageFile(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl) || imageUrl.Contains("default-avatar") || imageUrl.Contains("agent-"))
                    return;

                var fileName = Path.GetFileName(imageUrl);

                var filePath = Path.Combine(_imagePath, fileName);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
            catch { }
        }

        #endregion





















































































































































































































































































































































































































































































































































































































































































































    }
}