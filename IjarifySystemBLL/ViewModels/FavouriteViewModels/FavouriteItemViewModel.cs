using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.FavouriteViewModels
{
    public class FavouriteItemViewModel
    {

        public int FavouriteId { get; set; }
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; } = null;
        public string PropertyDescription { get; set; } = null;
        public decimal Price { get; set; }
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public decimal Area { get; set; }
        public string ListingType { get; set; } = null;
        public string Type { get; set; } = null;

        // Location Info
        public string City { get; set; } = null;
        public string Region { get; set; } = null;
        public string Street { get; set; } = null;

      
        public string? ImageUrl { get; set; }

        // Favourite Date
        public DateTime CreatedAt { get; set; }
    }
}
