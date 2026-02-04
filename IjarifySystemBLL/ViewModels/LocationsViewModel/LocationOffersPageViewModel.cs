using IjarifySystemBLL.ViewModels.OfferViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.LocationsViewModel
{
    public class LocationOffersPageViewModel
    {
        public string City { get; set; } = null!;
        public string LocationImageUrl { get; set; } = null!;
        public List<OfferViewModel> Offers { get; set; } = null!;
    }
}
