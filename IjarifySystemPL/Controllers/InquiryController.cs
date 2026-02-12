using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.InquiryViewModels;
using IjarifySystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryService _inquiryService;
        private readonly UserManager<User> _userManager;

        public InquiryController(IInquiryService inquiryService, UserManager<User> userManager)
        {
            _inquiryService = inquiryService;
            _userManager = userManager;
        }

        private async Task<int?> GetCurrentUserIdAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser?.Id;
        }

        //   /Inquiry/Index
        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var viewModel = _inquiryService.GetUserInquiries(userId.Value);
            return View(viewModel);
        }

        //   /Inquiry/Property/{propertyId}
        public IActionResult Property(int propertyId)
        {
            if (propertyId == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = _inquiryService.GetPropertyInquiries(propertyId);
            return View(viewModel);
        }

        //  /Inquiry/Details/{id}
        public IActionResult Details(int id)
        {
            var viewModel = _inquiryService.GetInquiryDetails(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        //  /Inquiry/Create
        public async Task<IActionResult> Create(int propertyId)
        {
            if (propertyId == 0)
            {
                return RedirectToAction("Index", "Property");
            }

            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", new { propertyId }) });
            }

            var model = new CreateInquiryViewModel
            {
                PropertyId = propertyId,
                UserId = userId.Value 
            };

            return View(model);
        }

        //   /Inquiry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInquiryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                 return RedirectToAction("Login", "Account");
            }

            model.UserId = userId.Value;

            try
            {
                var result = _inquiryService.CreateInquiry(model);

                if (result)
                {
                    TempData["SuccessMessage"] = "Your inquiry has been sent successfully!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Failed to send inquiry. Please try again.";
                return View(model);
            }
            catch (Exception ex)
            {
                // Show the actual error message to help debugging
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(model);
            }
        }

        //   /Inquiry/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await GetCurrentUserIdAsync();
           
            if (userId == null)
            {
                return Json(new { success = false, message = "Unauthorized" });
            }

            var result = _inquiryService.DeleteInquiry(id, userId.Value);

            if (result)
            {
                return Json(new { success = true, message = "Inquiry deleted successfully" });
            }

            return Json(new { success = false, message = "Failed to delete inquiry" });
        }
    }
}
