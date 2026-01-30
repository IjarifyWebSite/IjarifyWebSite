using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.ReviewsViewModels
{
    public class PropertyReviewsViewModel
    {
        public int PropertyId { get; set; }

        public double AverageRating { get; set; } // Computed

        public int TotalReviews { get; set; }

        public List<ReviewItemViewModel> Reviews { get; set; } = new();
    }
}
