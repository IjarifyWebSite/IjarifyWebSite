using IjarifySystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IjarifySystemBLL.ViewModels.OfferViewModels;
using IjarifySystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace IjarifySystemPL.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly ILocationService _locationService;
        private readonly IPropertyService _propertyService;
        private readonly UserManager<User> userManager;

        public OfferController(IOfferService offerService,ILocationService locationService,IPropertyService propertyService,UserManager<User> userManager)
        {
            _offerService = offerService;
            _locationService = locationService;
            _propertyService = propertyService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var Model = new CreateOfferViewModel()
            {
                properties = _propertyService.GetPropertyByuser(currentUser.Id),
                locations=_locationService.GetAllLocations()

            };


            return View(Model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateOfferViewModel request)
        {
            ModelState.Remove("properties");
            ModelState.Remove("locations");
            if (!ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(User);
                request.locations = _locationService.GetAllLocations();
                request.properties = _propertyService.GetPropertyByuser(currentUser.Id);
                return View(request);
            }
            bool IsCreated= _offerService.CreateOffer(request);
            if(IsCreated)
            {
                return RedirectToAction("MyOffers");
            }

            var currentUser = await userManager.GetUserAsync(User);
            request.locations = _locationService.GetAllLocations();
            request.properties = _propertyService.GetPropertyByuser(currentUser.Id);
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
            var currentUser = await userManager.GetUserAsync(User);
            var properties = _propertyService.GetByLocationAndUser(locationId, currentUser.Id);
            return Json(properties);
        }

        [Authorize]
        public IActionResult MyOffers()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var offers= _offerService.GetUserOffers(currentUser.Id);
            return View(offers);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int offerId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var offer = _offerService.GetOfferForUpdate(offerId,currentUser.Id);
            
            return View("Create",offer);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
