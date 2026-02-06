using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.PropertyViewModels
{
    public class PropertyFilterViewModel
    {
        public string? PropertyType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MinBathrooms { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? ListingType { get; set; } 
        public List<int>? AmenityIds { get; set; }
    }
}
