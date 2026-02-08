using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IReviewService _reviewService;
        public AccountController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            int userId = 1;

            var profile = new ProfileViewModel
            {
                FullName = "User Name",
                Email = "user@example.com",
                Address = "Cairo, Egypt",
                ProfileImageUrl = "/assets/img/real-estate/agent-1.webp",
                PhoneNumber = "+201234567890",
                WhatsApp = "+201234567890",
                Reviews = _reviewService.GetReviewsByUser(userId)
            };

            return View(profile);
        }
    }
}