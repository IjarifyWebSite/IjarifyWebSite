using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Amenity:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;

        #region Amenity-Property
        public ICollection<Property> properties { get; set; } = null!;
        #endregion

    }
}
