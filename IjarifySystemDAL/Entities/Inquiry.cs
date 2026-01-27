using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Inquiry:BaseEntity
    {
        public string Message { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;
    }
}
