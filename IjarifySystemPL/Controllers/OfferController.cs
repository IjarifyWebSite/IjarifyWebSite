using IjarifySystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IjarifySystemBLL.ViewModels.OfferViewModels;
namespace IjarifySystemPL.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        public IActionResult Index()
        {
            return View();
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
    } 
}
