using IjarifySystemDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Property:BaseEntity
    {
        //CreatedAt
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public decimal Area { get; set; }
        public PropertyListingType ListingType { get; set; }
        public PropertyType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public ICollection<PropertyImages> PropertyImages { get; set; } = null!;
        public ICollection<Inquiry>? PropertyInquiries { get; set; }
        public ICollection<Offer>? PropertyOffers { get; set; }

        public ICollection<Amenity>? amenities { get; set; } 

        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<Favourite>? Favorites { get; set; }

        public ICollection<Review>? Reviews { get; set; }


    }
}
