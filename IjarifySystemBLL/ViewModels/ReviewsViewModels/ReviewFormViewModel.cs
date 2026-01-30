using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.ReviewsViewModels
{
    //For Create And Update Review 
    public class ReviewFormViewModel
    {
        public int PropertyId { get; set; }
        public int? ReviewId { get; set; }

        [Required(ErrorMessage = "Rating Is Required")]
        [Range(1, 5, ErrorMessage = "Rating Must Be Between 1 And 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment Is Required")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Comment Must Be At Least 5 Chars And Cannot Exceed 500 Chars")]
        public string Comment { get; set; } = null!;
    }
}
