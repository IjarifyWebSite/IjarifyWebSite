using Microsoft.AspNetCore.Mvc;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;

namespace IjarifyWeb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewFormViewModel createReview)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Property", new { id = createReview.PropertyId });
            }

            int fakeUserId = 4;
            bool isCreated = reviewService.CreateReview(createReview, fakeUserId);

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
        public IActionResult Edit(ReviewFormViewModel updateReview)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Property", new { id = updateReview.PropertyId });
            }

            int fakeUserId = 4;
            bool isUpdated = reviewService.UpdateReview(updateReview, fakeUserId, updateReview.ReviewId ?? 0);

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
        public IActionResult Delete(int id, int propertyId)
        {
            int fakeUserId = 4;
            bool isDeleted = reviewService.DeleteReview(id, fakeUserId);

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