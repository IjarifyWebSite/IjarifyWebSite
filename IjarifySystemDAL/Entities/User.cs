using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class User:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string  Phone { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Address { get; set; } = null!;

        public ICollection<Property>? Properties { get; set; }
        public ICollection<Inquiry>? UserInquiries { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

        public ICollection<Favourite>? Favorites { get; set; }

        public ICollection<Review>? reviews { get;set; }


    }
}
