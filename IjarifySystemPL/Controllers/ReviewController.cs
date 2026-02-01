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

        [HttpGet]
        public IActionResult PropertyReviews(int propertyId)
        {
            int fakeUserId = 1;
            var reviewsViewModel = reviewService.GetReviewsByProperty(propertyId, fakeUserId);

            return View(reviewsViewModel);
        }

        [HttpGet]
        public IActionResult Create(int propertyId)
        {
            var review = new ReviewFormViewModel { PropertyId = propertyId };
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewFormViewModel createReview)
        {
            if (!ModelState.IsValid)
            {
                return View(createReview);
            }

            int fakeUserId = 1;
            bool IsCreated = reviewService.CreateReview(createReview, fakeUserId);

            if (IsCreated)
            {
                return RedirectToAction("Details", "Property", new { id = createReview.PropertyId });
            }

            ModelState.AddModelError("", "You have already reviewed this property.");
            return View(createReview);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var review = reviewService.GetReviewToUpdate(id);
            if (review == null) return NotFound();

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReviewFormViewModel updateReview)
        {
            if (!ModelState.IsValid) return View(updateReview);

            int fakeUserId = 1;
            bool IsUpdated = reviewService.UpdateReview(updateReview, fakeUserId, updateReview.ReviewId ?? 0);

            if (IsUpdated)
            {
                return RedirectToAction("Details", "Property", new { id = updateReview.PropertyId });
            }

            ModelState.AddModelError("", "Unable to update review. Make sure you are the owner of review.");
            return View(updateReview);
        }

        [HttpPost]
        public IActionResult Delete(int id, int propertyId)
        {
            int fakeUserId = 1;
            bool IsDeleted = reviewService.DeleteReview(id, fakeUserId);

            if (IsDeleted)
            {
                return RedirectToAction("Details", "Property", new { id = propertyId });
            }

            return BadRequest("Delete failed.");
        }


        [HttpGet]
        public IActionResult MyReviews()
        {
            int fakeUserId = 1;
            var reviews = reviewService.GetReviewsByUser(fakeUserId);
            return View(reviews);
        }
    }
}