using IjarifySystemBLL.ViewModels.AmenityViewModels;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.PropertyViewModels
{
    public class PropertyDetailsViewModel
    {
        // Basic Property Information
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ListingType { get; set; } // "For Sale" or "For Rent"
        public string PropertyType { get; set; }

        // Property Stats
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public decimal Area { get; set; } // Square Feet
        public decimal? LotSize { get; set; } // Acre Lot (optional)
        public int? YearBuilt { get; set; } // Year Built (optional)
        public int? GarageSpaces { get; set; } // Garage (optional)

        // Location Information
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string FullAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string NeighborhoodInfo { get; set; }

        // Images
        public List<PropertyImageViewModel> Images { get; set; }

        // Amenities
        public List<AmenityViewModel> Amenities { get; set; }

        // Agent Information
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentTitle { get; set; }
        public string AgentPhone { get; set; }
        public string AgentEmail { get; set; }
        public string AgentAvatar { get; set; }
        public bool IsOwner { get; set; }

        // Additional Info
        public DateTime CreatedAt { get; set; }
        public bool IsNew { get; set; }

        // Reviews
        public List<ReviewItemViewModel> Reviews { get; set; } = new List<ReviewItemViewModel>();

        //User Info
        public string? CurrentUserImage { get; set; }

        // Favourite status
        public bool IsFavourite { get; set; }
    } 
}
