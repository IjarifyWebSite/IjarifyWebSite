using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.PropertyViewModels
{
    public class PropertyIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public decimal Area { get; set; }
        public string ListingType { get; set; } // "For Sale" or "For Rent"
        public string PropertyType { get; set; }
        public string MainImage { get; set; }
        public int TotalImages { get; set; }
        public bool IsNew { get; set; }

        // Agent/User information
        public string AgentName { get; set; }
        public string AgentPhone { get; set; }
        public string AgentAvatar { get; set; }

        //reviews & rating
        public PropertyReviewsViewModel? Reviews { get; set; }

        public bool IsFavourite { get; set; }
    }
}
