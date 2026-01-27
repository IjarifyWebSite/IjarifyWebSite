using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string  Phone { get; set; } = null!;
        public string Role { get; set; } = null!;

        public ICollection<Property>? Properties { get; set; }
        public ICollection<Inquiry>? UserInquiries { get; set; }


    }
}
