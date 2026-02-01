using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.OfferViewModels
{
    public class UpdateOfferViewModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Start Date Is Required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date Is Required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Discount Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Discount Percentage must be between 0 and 100.")]
        public decimal DiscountPercentage { get; set; }
        [Required(ErrorMessage = "Property Is required.")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Location Is required.")]
        public int LcationId { get; set; }
    }
}
