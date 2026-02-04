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
        IEnumerable<OfferViewModel> GetAllOffersByLocation(string LocationName);
        OfferViewModel? GetOfferById(int id);
        bool CreateOffer(CreateOfferViewModel offerViewModel);
        UpdateOfferViewModel? GetOfferForUpdate(int id);
        bool UpdateOffer(UpdateOfferViewModel offerViewModel,int offerId);
        bool DeleteOffer(int id);


    }
}
