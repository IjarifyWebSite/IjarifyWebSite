using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.LocationsViewModel;
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
        private readonly ILocationRepository _locationRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IBookingRepository _bookingRepository;

        public OfferService(IOfferRepository offerRepository,ILocationRepository locationRepository,IPropertyRepository propertyRepository,IBookingRepository bookingRepository)
        {
            _offerRepository = offerRepository;
            _locationRepository = locationRepository;
            _propertyRepository = propertyRepository;
            _bookingRepository = bookingRepository;
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
                    DiscountPercentage = (int)offerViewModel.DiscountPercentage,
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
            var Offers = _offerRepository.GetAll(o=>DateTime.Now<o.EndDate);
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

      
        public LocationOffersPageViewModel? GetAllOffersByLocation(string city)
        {
            var Offers= _offerRepository.GetAllForLocation(city, o => DateTime.Now < o.EndDate && o.IsActive);
            var location = _locationRepository.GetByCity(city);
            if(location == null)
            {
                return null;
            }
            var model = new LocationOffersPageViewModel
            {
                City = city,
                LocationImageUrl =location.ImageUrl ,
                Offers = Offers.Select(o => new OfferViewModel
                {
                    Id = o.Id,
                    Title = o.Title,
                    StartDateRaw = o.CreatedAt,
                    EndDateRaw = o.EndDate,
                    DiscountPercentage = (int)o.DiscountPercentage,
                    PropertyTitle = o.Property.Title,
                    LocationName = $"{o.Property.Location.City}-{o.Property.Location.Street}-{o.Property.Location.Regoin}"
                }).ToList()
            };

            return model;
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
                DiscountPercentage = (int)Offer.DiscountPercentage,
                PropertyTitle = Offer.Property.Title,
                LocationName = $"{Offer.Property.Location.City}-{Offer.Property.Location.Street}-{Offer.Property.Location.Regoin}"
            };
            return offerViewModel;
        }

        public CreateOfferViewModel? GetOfferForUpdate(int id,int userId)
        {
            var offer = _offerRepository.GetById(id);

            if (offer == null)
            {
                return null;
            }
            var properties = _propertyRepository.GetByUser(userId);
            var Locations = _locationRepository.GetAll();
            var offerViewModel = new CreateOfferViewModel
            {
                Id= offer.Id,
                Title = offer.Title,
                StartDate = offer.CreatedAt,
                EndDate = offer.EndDate,
                DiscountPercentage = (int)offer.DiscountPercentage,
                PropertyId = offer.PropertyId,
                LoctionId = offer.Property.LocationId,
                properties = properties,
                locations = Locations,
            };
            return offerViewModel;
        }

        public bool UpdateOffer(CreateOfferViewModel offerViewModel)
        {
            try
            {
                var offer = _offerRepository.GetById(offerViewModel.Id);
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
        public async Task <bool> DeleteOffer(int id)
        {
            var offer = _offerRepository.GetById(id);
            if (offer == null)
            {
                return false;
            }
            offer.IsActive = false;
            _offerRepository.Update(offer);
            _offerRepository.SaveChanges();
            var bookings = await _bookingRepository.GetAllAsync(b => b.PropertyID == offer.PropertyId);
            if (!bookings.Any())
            {
                _offerRepository.Delete(offer);
                return _offerRepository.SaveChanges() > 0;
            }
            return true;
            
           
        }

        public OfferFilterViewModel GetFilterPageIntialData()
        {
            var offers = _offerRepository.GetOffersWithPropertyAndLocation(o => DateTime.Now < o.EndDate);
            var areas = offers.Select(o => o.Property.Location.City).Distinct().ToList();
            var Compounds= offers.Select(o => o.Property.Title).Distinct().ToList();
            var filterData = new OfferFilterViewModel
            {
                Areas = areas,
                Compounds = Compounds,
                
                Offers = offers.Select(o => new OfferViewModel
                {
                    Title = o.Title,
                    StartDateRaw = o.CreatedAt,
                    EndDateRaw = o.EndDate,
                    DiscountPercentage = (int)o.DiscountPercentage,
                    PropertyImageUrl= o.Property.PropertyImages?.FirstOrDefault()?.ImageUrl ?? "assets/img/real-estate/property-interior-7",
                    PropertyTitle = o.Property.Title,
                    LocationName = $"{o.Property.Location.City}-{o.Property.Location.Street}-{o.Property.Location.Regoin}"
                }).ToList()

            };
            return filterData;

        }
        public OfferFilterViewModel GetFilteredOffers(OfferFilterRequestViewModel? Request = null)
        {
            var FilteredOffers = _offerRepository.GetOffersWithPropertyAndLocation(o => DateTime.Now < o.EndDate, Request?.SearchTerm,Request?.Areas,Request?.Compounds,Request?.MinimumDiscount);
            var filterData = new OfferFilterViewModel
            {
                Offers = FilteredOffers.Select(o => new OfferViewModel
                {
                    Title = o.Title,
                    StartDateRaw = o.CreatedAt,
                    EndDateRaw = o.EndDate,
                    DiscountPercentage = (int)o.DiscountPercentage,
                    PropertyImageUrl = o.Property.PropertyImages?.FirstOrDefault()?.ImageUrl ?? "assets/img/real-estate/property-interior-7",
                    PropertyTitle = o.Property.Title,
                    LocationName = $"{o.Property.Location.City}-{o.Property.Location.Street}-{o.Property.Location.Regoin}"
                }).ToList(),
                SelectedAreas = Request?.Areas ?? new List<string>(),
                SelectedCompounds = Request?.Compounds ?? new List<string>(),
                SelectedDiscounts = Request?.MinimumDiscount ?? new List<decimal>(),
                Areas = _offerRepository.GetOffersWithPropertyAndLocation(o => DateTime.Now < o.EndDate).Select(o => o.Property.Location.City).Distinct().ToList(),
                Compounds = _offerRepository.GetOffersWithPropertyAndLocation(o => DateTime.Now < o.EndDate).Select(o => o.Property.Title).Distinct()


                .ToList()
            };
            return filterData;
        }

        IEnumerable<OfferViewModel?> IOfferService.GetUserOffers(int UserId)
        {
           var offers= _offerRepository.GetByUserId(UserId);    
            if(offers == null || !offers.Any())
            {
                return Enumerable.Empty<OfferViewModel?>();
            }
            var model = offers.Select(o => new OfferViewModel
            {
                Id = o.Id,
                Title = o.Title,
                StartDateRaw = o.CreatedAt,
                EndDateRaw = o.EndDate,
                DiscountPercentage = (int)o.DiscountPercentage,
                PropertyTitle = o.Property.Title,
                LocationName = $"{o.Property.Location.City}-{o.Property.Location.Street}-{o.Property.Location.Regoin}"

            });
            return model;

        }

       
    }
}
