using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.OfferViewModels;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public bool CreateOffer(CreateOfferViewModel offerViewModel)
        {
            try
            {
                var offer = new Offer()
                {
                    Title = offerViewModel.Title,
                    CreatedAt = offerViewModel.StartDate,
                    EndDate = offerViewModel.EndDate,
                    DiscountPercentage = offerViewModel.DiscountPercentage,
                    PropertyId = offerViewModel.PropertyId,

                };
                _offerRepository.Add(offer);
                return _offerRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }       
        public IEnumerable<OfferViewModel> GetAllOffers()
        {
            var Offers = _offerRepository.GetAll(o=>o.CreatedAt<o.EndDate);
            if (Offers == null || !Offers.Any())
            {
                return Enumerable.Empty<OfferViewModel>();
            }
          var memberViewModels = Offers.Select(o => new OfferViewModel
            {
                Id = o.Id,
                Title = o.Title,
                StartDateRaw = o.CreatedAt,
                EndDateRaw = o.EndDate,
                DiscountPercentage = o.DiscountPercentage,
                PropertyTitle = o.Property.Title,
                LocationName = $"{o.Property.Location.City}-{o.Property.Location.Street}-{o.Property.Location.Regoin}"
          });
            return memberViewModels;
        }

        public IEnumerable<OfferViewModel> GetAllOffersByLocation(string city)
        {
            var Offers= _offerRepository.GetAllForLocation(city,o=>o.CreatedAt<o.EndDate);
            if (Offers == null || !Offers.Any())
            {
                return Enumerable.Empty<OfferViewModel>();
            }
            var memberViewModels = Offers.Select(o => new OfferViewModel
            {
                Id = o.Id,
                Title = o.Title,
                StartDateRaw = o.CreatedAt,
                EndDateRaw = o.EndDate,
                DiscountPercentage = o.DiscountPercentage,
                PropertyTitle = o.Property.Title,
                LocationName = $"{o.Property.Location.City}-{o.Property.Location.Street}-{o.Property.Location.Regoin}"
            });
            return memberViewModels;
        }

        public OfferViewModel? GetOfferById(int id)
        {
            var Offer = _offerRepository.GetById(id);
            if (Offer == null)
            {
                return null;
            }
            var offerViewModel = new OfferViewModel
            {
                Id = Offer.Id,
                Title = Offer.Title,
                StartDateRaw = Offer.CreatedAt,
                EndDateRaw = Offer.EndDate,
                DiscountPercentage = Offer.DiscountPercentage,
                PropertyTitle = Offer.Property.Title,
                LocationName = $"{Offer.Property.Location.City}-{Offer.Property.Location.Street}-{Offer.Property.Location.Regoin}"
            };
            return offerViewModel;
        }

        public UpdateOfferViewModel? GetOfferForUpdate(int id)
        {
            var offer = _offerRepository.GetById(id);
            if (offer == null)
            {
                return null;
            }
            var offerViewModel = new UpdateOfferViewModel
            {
                Title = offer.Title,
                StartDate = offer.CreatedAt,
                EndDate = offer.EndDate,
                DiscountPercentage = offer.DiscountPercentage,
                PropertyId = offer.PropertyId,
            };
            return offerViewModel;
        }

        public bool UpdateOffer(UpdateOfferViewModel offerViewModel,int OfferId)
        {
            try
            {
                var offer = _offerRepository.GetById(OfferId);
                if (offer == null)
                {
                    return false;
                }
                offer.Title = offerViewModel.Title;
                offer.CreatedAt = offerViewModel.StartDate;
                offer.EndDate = offerViewModel.EndDate;
                offer.DiscountPercentage = offerViewModel.DiscountPercentage;
                offer.PropertyId = offerViewModel.PropertyId;
                offer.UpdatedAt = DateTime.Now;
                _offerRepository.Update(offer);
                return _offerRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOffer(int id)
        {
            //var offer = _offerRepository.GetById(id);
            //if (offer == null)
            //{
            //    return false;
            //}
            //offer.IsActive = false;
            //_offerRepository.Update(offer);
            //_offerRepository.SaveChanges();

            //if(_bookingRepo.GetAll(b=>b.propertyId == id).any())
            //{
            //    return True;
            //}
            //_offerRepository.Delete(offer);
            //return _offerRepository.SaveChanges() > 0;
            return false;
        }

    }
}
