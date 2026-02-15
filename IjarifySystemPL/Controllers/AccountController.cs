using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AccountViewModels;
using IjarifySystemDAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IjarifySystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IInquiryService _inquiryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly string _imagePath;

        public AccountController(IReviewService reviewService, IBookingService bookingService, IUserService userService, IInquiryService inquiryService, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _reviewService = reviewService;
            _bookingService = bookingService;
            _userService = userService;
            _inquiryService = inquiryService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _imagePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", "profiles");

            if (!Directory.Exists(_imagePath))
            {
                Directory.CreateDirectory(_imagePath);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile() 
        {
            var currentUser = await userManager.GetUserAsync(User);
            
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var user = _userService.GetUserById(currentUser.Id);

            var reviews = _reviewService.GetReviewsByUser(currentUser.Id);

            var allBookings = await _bookingService.GetUserBookingsAsync(currentUser.Id);

            var userInquiries = _inquiryService.GetUserInquiries(currentUser.Id);

            var profile = new ProfileViewModel
            {
                FullName = user.Name,
                Email = user.Email,
                Address = user.Address ?? "Cairo, Egypt",
                ProfileImageUrl = user.ImageUrl ?? "/Images\\profiles\\default_avatar.jpg",
                PhoneNumber = user.PhoneNumber,
                WhatsApp = user.PhoneNumber,
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
                TotalBookings = allBookings.Count(),
                RecentInquiries = userInquiries.Inquiries.Take(6).ToList(),
                TotalInquiries = userInquiries.TotalCount
            };

            return View(profile);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
             var currentUser = await userManager.GetUserAsync(User);

             if (currentUser == null)
             {
                 return RedirectToAction("Login");
             }

            var user = _userService.GetUserById(currentUser.Id);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditProfileViewModel
            {
                FullName = user.Name,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                WhatsApp = user.PhoneNumber,
                ProfileImageUrl = user.ImageUrl ?? "/Images\\profiles\\default_avatar.jpg"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }

            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            try
            {
                string? newImagePath = null;

                // Handle image upload
                if (editModel.ProfileImage != null)
                {
                    // Delete old image if exists
                    var user = _userService.GetUserById(currentUser.Id);
                    if (user != null && !string.IsNullOrEmpty(user.ImageUrl))
                    {
                        DeleteImageFile(user.ImageUrl);
                    }

                    newImagePath = await SaveProfileImageAsync(editModel.ProfileImage);
                }

                bool isUpdated = _userService.UpdateUserProfile(editModel, currentUser.Id, newImagePath);

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
        [Authorize]
        public async Task<IActionResult> DeleteProfileImage()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            try
            {
                var user = _userService.GetUserById(currentUser.Id);
                if (user != null && !string.IsNullOrEmpty(user.ImageUrl))
                    DeleteImageFile(user.ImageUrl);

                bool isDeleted = _userService.DeleteProfileImage(currentUser.Id);

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


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUser);
            }

            User user = await userManager.FindByNameAsync(loginUser.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginUser);
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(user, loginUser.Password);

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginUser);
            }

            await signInManager.SignInAsync(user, loginUser.RememberMe);

            TempData["SuccessMessage"] = $"Welcome back, {user.Name}!";
            return RedirectToAction("Index", "Home");
        }
      
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                var existingUser = await userManager.FindByNameAsync(newUser.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Username already exists");
                    return View(newUser);
                }

                // Check if email already exists
                var existingEmail = await userManager.FindByEmailAsync(newUser.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    return View(newUser);
                }

                // Check if phone number already exists
                if (!string.IsNullOrEmpty(newUser.PhoneNumber))
                {
                    var existingPhone = _userService.GetUserByPhoneNumber(newUser.PhoneNumber);
                    if (existingPhone != null)
                    {
                        ModelState.AddModelError("PhoneNumber", "This phone number is already registered. Please use a different number.");
                        return View(newUser);
                    }
                }

                // Create new user
                User user = new User
                {
                    Name = newUser.Name,
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    Address = newUser.Address,
                    PhoneNumber = newUser.PhoneNumber,
                    ImageUrl = "/Images\\profiles\\default_avatar.jpg",
                    CreatedAt = DateTime.Now
                };

                // Create user with password
                var result = await userManager.CreateAsync(user, newUser.Password);

                if (result.Succeeded)
                {
                    // Auto login after registration
                    await signInManager.SignInAsync(user, isPersistent: false);
                    TempData["SuccessMessage"] = "Registration successful! Welcome to Ijarify.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Show errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(newUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "You have been logged out successfully";
            return RedirectToAction("Index", "Home");
        }








































































































































































































































































































































































































































































































































































































































    }
}