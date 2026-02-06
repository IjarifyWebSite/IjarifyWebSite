using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IjarifySystemBLL.ViewModels.PropertyViewModels
{
    public class CreatePropertyViewModel
    {
        //Property Info
        [Required(ErrorMessage = "Property title is required")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 150 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, MinimumLength = 20, ErrorMessage = "Description must be at least 20 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Listing type is required")]
        public string ListingType { get; set; } = null!;

        [Required(ErrorMessage = "Property type is required")]
        public string PropertyType { get; set; } = null!;

        //Property Specifications
        [Required(ErrorMessage = "Number of bedrooms is required")]
        [Range(0, 50, ErrorMessage = "Invalid number of bedrooms")]
        public int BedRooms { get; set; }

        [Required(ErrorMessage = "Number of bathrooms is required")]
        [Range(0, 50, ErrorMessage = "Invalid number of bathrooms")]
        public int BathRooms { get; set; }

        [Required(ErrorMessage = "Area is required")]
        [Range(10, 100000, ErrorMessage = "Area must be at least 10 m²")]
        public decimal Area { get; set; }

        //Location Info
        [Required(ErrorMessage = "Street is required")]
        [StringLength(150)]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Region is required")]
        [StringLength(100)]
        public string Region { get; set; } = null!;

        //Images
        [Required(ErrorMessage = "Main image is required")]
        [Display(Name = "Main Property Image")]
        public IFormFile MainImage { get; set; } = null!;

        [Required(ErrorMessage = "images of property is required")]
        [Display(Name = "Property Gallery Images")]
        public List<IFormFile> GalleryImages { get; set; } = null!;

        //Amenities 
        [Display(Name = "Amenities")]
        public Dictionary<string, AmenityInput>? Amenities { get; set; }
    }

    #region Helper Class
    public class AmenityInput
    {
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!; // Interior or Exterior
    }
    #endregion
}