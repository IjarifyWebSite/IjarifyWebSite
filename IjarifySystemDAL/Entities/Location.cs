using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string Regoin { get; set; } = null!;
        public string Street { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ICollection<Property>? Properties { get; set; }

    }
}
