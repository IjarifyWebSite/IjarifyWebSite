using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.OfferViewModels
{
    public class OfferFilterRequestViewModel
    {
        public string? SearchTerm { get; set; }
        public List<decimal>? MinimumDiscount { get; set; }
        public List<string>? Areas { get; set; }
        public List<string>? Compounds { get; set; }

    }
}
