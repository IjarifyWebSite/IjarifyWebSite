using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using IjarifySystemDAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IFavouriteService _favouriteService;
        private readonly UserManager<User> _userManager;

        public PropertyController(IPropertyService propertyService, IFavouriteService favouriteService, UserManager<User> userManager)
        {
            _propertyService = propertyService;
            _favouriteService = favouriteService;
            _userManager = userManager;
        }

        private async Task<int?> GetCurrentUserIdAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser?.Id;
        }

        // GET: PropertyController
        public async Task<ActionResult> Index(PropertyFilterViewModel filter, int page = 1)
        {
            var userId = await GetCurrentUserIdAsync();
            var pageViewModel = await _propertyService.GetPagination(4, page, filter, userId);
            ViewBag.CurrentPage = pageViewModel.CurrentPage;
            ViewBag.TotalPages = pageViewModel.TotalPages;
            return View(pageViewModel);
        }

        // GET: PropertyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var userId = await GetCurrentUserIdAsync();
            var vmModel = await _propertyService.GetPropertyDetails(id, userId ?? 0); // Service might expect 0 or handle null? Previous code passed int, let's check service.

            // Just in case service handles 0 as guest
            int effectiveUserId = userId ?? 0;

            if (vmModel != null)
            {
                if (userId.HasValue)
                {
                    vmModel.IsFavourite = _favouriteService.IsPropertyFavourite(userId.Value, id);
                }
                else 
                {
                    vmModel.IsFavourite = false;
                }
            }

            return View("Details", vmModel);
        }

        // GET: PropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
