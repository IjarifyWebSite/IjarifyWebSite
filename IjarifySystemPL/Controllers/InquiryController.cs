using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.InquiryViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IInquiryService _inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

  
        public IActionResult Index(int userId)
        {
            
            if (userId == 0)
            {
                userId = 1;  
            }

            var viewModel = _inquiryService.GetUserInquiries(userId);
            return View(viewModel);
        }
 
        public IActionResult Property(int propertyId)
        {
            if (propertyId == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = _inquiryService.GetPropertyInquiries(propertyId);
            return View(viewModel);
        }
 
       
        public IActionResult Details(int id)
        {
            var viewModel = _inquiryService.GetInquiryDetails(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
 
        public IActionResult Create(int propertyId)
        {
            if (propertyId == 0)
            {
                return RedirectToAction("Index", "Property");
            }

            var model = new CreateInquiryViewModel
            {
                PropertyId = propertyId,
                UserId = 1  
            };

            return View(model);
        }

   
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateInquiryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

        
            model.UserId = 1;

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
                
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return View(model);
            }
        }

         
       
        [HttpPost]
        public IActionResult Delete(int id, int userId)
        {
           
            if (userId == 0)
            {
                userId = 1;
            }

            var result = _inquiryService.DeleteInquiry(id, userId);

            if (result)
            {
                return Json(new { success = true, message = "Inquiry deleted successfully" });
            }

            return Json(new { success = false, message = "Failed to delete inquiry" });
        }

    
    }
}