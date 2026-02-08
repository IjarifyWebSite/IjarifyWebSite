using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.OfferViewModels
{
    public class OfferFilterViewModel
    {
        public List<int> HotOffers { get; set; } =new List<int>() {10,20,50};
        public List<string> Areas { get; set; } = null!;
        public List<string> Compounds { get; set; } = null!;
        public List<OfferViewModel> Offers { get; set; } = null!;
        public string? SearchTerm { get; set; }
        public List<string> SelectedAreas { get; set; } = new();  // to not make check on nullability in view just check for contain items or not
        public List<string> SelectedCompounds { get; set; } = new();
        public List<decimal> SelectedDiscounts { get; set; } = new();
    }
}
