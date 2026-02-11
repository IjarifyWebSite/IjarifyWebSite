using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Property>? Properties { get; set; }
        public ICollection<Inquiry>? UserInquiries { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<Favourite>? Favorites { get; set; }

        public ICollection<Review>? reviews { get;set; }


    }
}
