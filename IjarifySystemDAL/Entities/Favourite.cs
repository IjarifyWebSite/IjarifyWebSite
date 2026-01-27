using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Favourite:BaseEntity
    {
        #region Favourite-Property
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;
        #endregion
        #region Favourite-User
        public int UserId { get; set; }
        public User User { get; set; }=null!;
        #endregion
    }
}
