using IjarifySystemBLL.ViewModels.LocationsViewModel;
using IjarifySystemBLL.ViewModels.OfferViewModels;
using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IOfferService
    {
        IEnumerable<OfferViewModel> GetAllOffers();
        LocationOffersPageViewModel? GetAllOffersByLocation(string LocationName);
        OfferFilterViewModel GetFilterPageIntialData();
        OfferFilterViewModel GetFilteredOffers(OfferFilterRequestViewModel? Request=null);
        OfferViewModel? GetOfferById(int id);
        bool CreateOffer(CreateOfferViewModel offerViewModel);
        UpdateOfferViewModel? GetOfferForUpdate(int id);
        bool UpdateOffer(UpdateOfferViewModel offerViewModel,int offerId);
        bool DeleteOffer(int id);


    }
}
