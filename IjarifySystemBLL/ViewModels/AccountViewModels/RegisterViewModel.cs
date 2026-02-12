using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be at least 4 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 100 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Phone must be a valid Egyptian number (e.g., 01012345678)")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
