using IjarifySystemBLL.ViewModels.AmenityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.PropertyViewModels
{
    public class PropertyIndexPageViewModel
    {
        public List<PropertyIndexViewModel> Properties { get; set; }
        public PropertyFilterViewModel Filter { get; set; }
        public List<string> PropertyTypes { get; set; }
        public List<string> ListingTypes { get; set; }
        public List<AmenityViewModel> Amenities { get; set; }
        public List<string> Cities { get; set; }
        public List<string> Regions { get; set; }

        // Add these new properties
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
