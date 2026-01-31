using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IReviewService
    {
        public PropertyReviewsViewModel GetReviewsByProperty(int propertyId, int currentUserId);
        public IEnumerable<ReviewItemViewModel> GetReviewsByUser(int userId);
        public ReviewFormViewModel? GetReviewToUpdate(int reviewId);
        public bool UpdateReview(ReviewFormViewModel updatedReview, int userId, int reviewId);
        public bool CreateReview(ReviewFormViewModel createdReview , int userId);
        public bool DeleteReview(int reviewId, int userId);
    }
}
