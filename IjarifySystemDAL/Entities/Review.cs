using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Review :BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;

        #region Review-Property
        public int PropertyId { get; set; }
        public Property property { get; set; } = null!;
        #endregion

        #region Review-User
        public int UserId { get; set; }
        public User user { get; set; } = null!;
        #endregion
    }
}
