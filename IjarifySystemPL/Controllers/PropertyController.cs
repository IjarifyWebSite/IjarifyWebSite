using IjarifySystemBLL.Services.Classes;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.Services.Classes;
using Microsoft.AspNetCore.Identity;
using IjarifySystemDAL.Entities;

namespace IjarifySystemPL.Controllers
{
    public class PropertyController(IPropertyService _propertyService, UserManager<User> _userManager) : Controller
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
            var currentUser = await _userManager.GetUserAsync(User);
            int? userId = currentUser?.Id;
            var vmModel = await _propertyService.GetPropertyDetails(id , userId);

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
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _propertyService.GetPropertyForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            // Optional: Check if current user owns the property
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var property = await _propertyService.GetPropertyDetails(id);

            if (property.AgentId != userId)
            {
                TempData["Error"] = "You don't have permission to edit this property.";
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(model);
        }

        // POST: PropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreatePropertyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                if (userId == 0)
                {
                    return Unauthorized();
                }

                await _propertyService.UpdatePropertyAsync(id, model, userId);

                TempData["Success"] = "Property updated successfully!";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the property: " + ex.Message);
                return View(model);
            }
        }


        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: PropertyController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var property = await _propertyService.GetPropertyDetails(id);

            if (property == null)
            {
                return NotFound();
            }

            // Check if current user owns the property
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (property.AgentId != userId)
            {
                TempData["Error"] = "You don't have permission to delete this property.";
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(property);
        }

        // POST: PropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                bool deleted = await _propertyService.DeletePropertyAsync(id, userId);

                if (!deleted)
                {
                    TempData["Error"] = "Property not found.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Success"] = "Property deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the property: " + ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
