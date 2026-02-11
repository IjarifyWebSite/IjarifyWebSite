using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.InquiryViewModels
{
    public class CreateInquiryViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Property is required")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters")]
        public string Message { get; set; } = null!;


        public string? PropertyTitle { get; set; }
    }
}