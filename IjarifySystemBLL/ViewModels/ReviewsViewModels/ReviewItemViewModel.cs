using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.ReviewsViewModels
{
    public class ReviewItemViewModel
    {
        public int ReviewId { get; set; }
        public int PropertyId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PropertyName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsOwner { get; set; } // For Enable Update | Delete
    }
}
