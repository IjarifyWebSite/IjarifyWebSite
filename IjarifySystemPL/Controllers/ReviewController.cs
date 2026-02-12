using Microsoft.AspNetCore.Mvc;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using IjarifySystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IjarifyWeb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<User> userManager;

        public ReviewController(IReviewService reviewService, UserManager<User> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ReviewFormViewModel createReview)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Property", new { id = createReview.PropertyId });
            }

            var currentUser = await userManager.GetUserAsync(User);
            bool isCreated = reviewService.CreateReview(createReview, currentUser.Id);

            if (isCreated)
            {
                TempData["SuccessMessage"] = "Review posted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "You have already reviewed this property.";
            }

            return RedirectToAction("Details", "Property", new { id = createReview.PropertyId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(ReviewFormViewModel updateReview)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Property", new { id = updateReview.PropertyId });
            }

            var currentUser = await userManager.GetUserAsync(User);
            bool isUpdated = reviewService.UpdateReview(updateReview, currentUser.Id, updateReview.ReviewId ?? 0);

            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Review updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Oops! Something went wrong while updating.";
            }

            return RedirectToAction("Details", "Property", new { id = updateReview.PropertyId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id, int propertyId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            bool isDeleted = reviewService.DeleteReview(id, currentUser.Id);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Your review has been deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Oops! Something went wrong while deleting. Please try again.";
            }

            return RedirectToAction("Details", "Property", new { id = propertyId });
        }
    }
}