using IjarifySystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class FavouriteController : Controller
    {



        private readonly IFavouriteService _favouriteService;

        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }


        public IActionResult Index(int UserId)
        {
            if (UserId == 0)
            {
                UserId = 1; // مؤقت للتجربة
            }

            var viewModel = _favouriteService.GetUserFavourites(UserId);
            return View(viewModel);
        }





        [HttpPost]
        public IActionResult Add(int userId, int propertyId, string returnUrl = "")
        {
            var result = _favouriteService.AddToFavourites(userId, propertyId);

            if (result)
            {
                TempData["SuccessMessage"] = "Added to favourites successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add to favourites.";
            }

            // ارجع للصفحة اللي جاي منها
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", new { userId = userId });
        }




        [HttpPost]
        public IActionResult Remove(int userId, int propertyId, string returnUrl = "")
        {
            var result = _favouriteService.RemoveFromFavourites(userId, propertyId);

            if (result)
            {
                TempData["SuccessMessage"] = "Removed from favourites successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove from favourites.";
            }

            // ارجع للصفحة اللي جاي منها
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", new { userId = userId });
        }




        [HttpPost]
        public IActionResult Toggle(int userId, int propertyId, string returnUrl = "")
        {
            if (userId == 0)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction("Index", "Home");
            }

            var result = _favouriteService.ToggleFavourite(userId, propertyId);

            if (result)
            {
                var isFavourite = _favouriteService.IsPropertyFavourite(userId, propertyId);
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

            // ارجع للصفحة اللي جاي منها
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", new { userId = userId });
        }
    }
}

 