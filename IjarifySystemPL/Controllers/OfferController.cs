using IjarifySystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IjarifySystemBLL.ViewModels.OfferViewModels;
using IjarifySystemDAL.Entities;
namespace IjarifySystemPL.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly ILocationService _locationService;
        private readonly IPropertyService _propertyService;

        public OfferController(IOfferService offerService,ILocationService locationService,IPropertyService propertyService)
        {
            _offerService = offerService;
            _locationService = locationService;
            _propertyService = propertyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var FakeUserId = 1;
            var Model = new CreateOfferViewModel()
            {
                properties = _propertyService.GetPropertyByuser(FakeUserId),
                locations=_locationService.GetAllLocations()

            };


            return View(Model);
        }
        [HttpPost]
        public IActionResult Create(CreateOfferViewModel request)
        {
            ModelState.Remove("properties");
            ModelState.Remove("locations");
            if (!ModelState.IsValid)
            {
                var FakeUserId = 1;
                request.locations = _locationService.GetAllLocations();
                request.properties = _propertyService.GetPropertyByuser(FakeUserId);
                return View(request);
            }
            bool IsCreated= _offerService.CreateOffer(request);
            if(IsCreated)
            {
                return RedirectToAction("MyOffers");
            }

            var fakeUserId = 1;
            request.locations = _locationService.GetAllLocations();
            request.properties = _propertyService.GetPropertyByuser(fakeUserId);
            ModelState.AddModelError("", "Something went wrong while creating the offer. Please try again.");
            return View(request);
        }
        public IActionResult LocationOffers(string City)
        {
            var LocationOffersPage = _offerService.GetAllOffersByLocation(City);
            return View(LocationOffersPage);
        }
        public IActionResult FilterOffers()
        {
            var FilterPage = _offerService.GetFilterPageIntialData();
            return View(FilterPage);
        }
        [HttpGet]
        public IActionResult FilterOffers(OfferFilterRequestViewModel Filter)
        {
            var FilteredOffers = _offerService.GetFilteredOffers(Filter);
            return View(FilteredOffers);
        }

        // for ajax call
       public IActionResult GetPropertiesByLocation(int locationId)
        {
            var fakeUserId = 1;
            var properties = _propertyService.GetByLocationAndUser(locationId, fakeUserId);
            return Json(properties);
        }

        public IActionResult MyOffers()
        {
            var fakeUserId = 1;
            var offers= _offerService.GetUserOffers(fakeUserId);
            return View(offers);
        }

        [HttpGet]
        public IActionResult Edit(int offerId)
        {
            var fakeuUserId = 1;
            var offer = _offerService.GetOfferForUpdate(offerId,fakeuUserId);
            
            return View("Create",offer);
        }

        [HttpPost]
        public IActionResult Edit(CreateOfferViewModel request)
        {
            ModelState.Remove("properties");
            ModelState.Remove("locations");
            if(!ModelState.IsValid)
            {
                return View("Create",request);
            }
            bool IsUpdated = _offerService.UpdateOffer(request);
            if(IsUpdated)
            {
                return RedirectToAction("MyOffers");
            }
            ModelState.AddModelError("", "Something went wrong while Editing the offer. Please try again.");
            return View("Create",request);

        }
        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {
            bool IsDeleted= await _offerService.DeleteOffer(id);
            if (IsDeleted)
            {
                return RedirectToAction("MyOffers");
            }
            ModelState.AddModelError("", "Something went wrong while creating the offer. Please try again.");
            return RedirectToAction("MyOffers");
        }
    } 
}
