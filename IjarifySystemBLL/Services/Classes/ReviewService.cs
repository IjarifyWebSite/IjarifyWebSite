using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Classes;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public bool CreateReview(ReviewFormViewModel createdReview , int userId)
        {
            try
            {
                var hasReviewed = reviewRepository.GetAllPropertyReviews(createdReview.PropertyId).Any(r => r.UserId == userId);
                if (hasReviewed) return false;

                var review = new Review
                {
                    Comment = createdReview.Comment,
                    Rating = createdReview.Rating,
                    PropertyId = createdReview.PropertyId,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };

                reviewRepository.Add(review);
                return reviewRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteReview(int reviewId, int userId)
        {
            try
            {
                var review = reviewRepository.GetReviewById(reviewId);
                if (review == null) return false;

                if (review.UserId != userId) return false;

                reviewRepository.Delete(review);
                return reviewRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public PropertyReviewsViewModel GetReviewsByProperty(int propertyId, int currentUserId)
        {
            var reviews = reviewRepository.GetAllPropertyReviews(propertyId).ToList();

            var totalReviews = reviews.Count;
            var averageReviews = totalReviews > 0 ? reviews.Average(r => r.Rating) : 0;

            var propertyReviews = new PropertyReviewsViewModel
            {
                PropertyId = propertyId,
                TotalReviews = totalReviews,
                AverageRating = averageReviews,
                Reviews = reviews.Select(r => new ReviewItemViewModel
                {
                    ReviewId = r.Id,
                    PropertyId = r.PropertyId,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt,
                    UserName = r.user.Name,
                    IsOwner = r.UserId == currentUserId
                }).ToList()
            };

            return propertyReviews;
        }

        public IEnumerable<ReviewItemViewModel> GetReviewsByUser(int userId)
        {
            var reviews = reviewRepository.GetAllUserReviews(userId).ToList();
            if (reviews is null || !reviews.Any()) return [];

            var userReviews = reviews.Select(r => new ReviewItemViewModel
            {
                ReviewId = r.Id,
                PropertyId = r.PropertyId,
                Comment = r.Comment,
                Rating = r.Rating,
                CreatedAt = r.CreatedAt,
                PropertyName = r.property.Title,
                IsOwner = true
            }).ToList();

            return userReviews;
        }

        public ReviewFormViewModel? GetReviewToUpdate(int reviewId)
        {
            var review = reviewRepository.GetReviewById(reviewId);
            if (review == null) return null;

            return new ReviewFormViewModel
            {
                ReviewId = review.Id,
                PropertyId = review.PropertyId,
                Comment = review.Comment,
                Rating = review.Rating,
            };
        }

        public bool UpdateReview(ReviewFormViewModel updatedReview, int userId , int reviewId)
        {
            try
            {
                var review = reviewRepository.GetReviewById(reviewId);
                if (review == null) return false;

                if (review.UserId != userId) return false;

                review.Comment = updatedReview.Comment;
                review.Rating = updatedReview.Rating;

                reviewRepository.Update(review);
                return reviewRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
