using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IFavouriteService _favouriteService;
        private readonly UserManager<User> _userManager;

        public FavouriteController(IFavouriteService favouriteService, UserManager<User> userManager)
        {
            _favouriteService = favouriteService;
            _userManager = userManager;
        }

        private async Task<int?> GetCurrentUserIdAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser?.Id;
        }

        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = _favouriteService.GetUserFavourites(userId.Value);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int propertyId, string returnUrl = "")
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = _favouriteService.AddToFavourites(userId.Value, propertyId);

            if (result)
            {
                TempData["SuccessMessage"] = "Added to favourites successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add to favourites.";
            }

            // Return to the previous page
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int propertyId)
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                return Json(new { success = false, message = "Unauthorized" });
            }

            var result = _favouriteService.RemoveFromFavourites(userId.Value, propertyId);

            if (result)
            {
                return Json(new { success = true, message = "Removed from favourites successfully!" });
            }

            return Json(new { success = false, message = "Failed to remove from favourites." });
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int propertyId, string returnUrl = "")
        {
            var userId = await GetCurrentUserIdAsync();

            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please login to add favorites";
                return RedirectToAction("Login", "Account", new { returnUrl });
            }

            var result = _favouriteService.ToggleFavourite(userId.Value, propertyId);

            if (result)
            {
                var isFavourite = _favouriteService.IsPropertyFavourite(userId.Value, propertyId);
                if (isFavourite)
                {
                    TempData["SuccessMessage"] = "Added to favourites!";
                }
                else
                {
                    TempData["SuccessMessage"] = "Removed from favourites!";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update favourite";
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");
        }
    }
}
