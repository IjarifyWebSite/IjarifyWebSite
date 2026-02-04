using IjarifySystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var LocationOffersPage= _offerService.GetAllOffersByLocation(City);
            return View(LocationOffersPage);
        }
    }
}
