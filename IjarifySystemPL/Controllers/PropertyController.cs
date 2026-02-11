using IjarifySystemBLL.Services.Classes;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IjarifySystemPL.Controllers
{
    public class PropertyController(IPropertyService _propertyService) : Controller
    {

        // GET: PropertyController
        public async Task<ActionResult> Index(PropertyFilterViewModel filter, int page = 1)
        {
            var pageViewModel = await _propertyService.GetPagination(4, page, filter);
            ViewBag.CurrentPage = pageViewModel.CurrentPage;
            ViewBag.TotalPages = pageViewModel.TotalPages;
            return View(pageViewModel);
        }

        // GET: PropertyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            int fakeUserId = 3;
            var vmModel = await _propertyService.GetPropertyDetails(id , fakeUserId);

            return View("Details",vmModel);
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Create(CreatePropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Get current logged-in user ID (adjust based on your authentication)
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                if (userId == 0)
                {
                    return Unauthorized();
                }

                await _propertyService.CreatePropertyAsync(model, userId);

                TempData["Success"] = "Property created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the property: " + ex.Message);
                return View(model);
            }
        }

        // GET: PropertyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
