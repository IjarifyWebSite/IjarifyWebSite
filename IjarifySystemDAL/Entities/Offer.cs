using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Entities
{
    public class Offer:BaseEntity
    {
        //CreateAt == StartDate
        public string Title { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public bool IsActive { get; set; } = true;

    }
}
