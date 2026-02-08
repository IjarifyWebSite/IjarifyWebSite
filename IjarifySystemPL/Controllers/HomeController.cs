using System.Diagnostics;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels;
using IjarifySystemPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace IjarifySystemPL.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _HomeService;

        public HomeController(ILogger<HomeController> logger,IHomeService homeService)
        {
            _logger = logger;
            _HomeService = homeService;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
               TopLocations=_HomeService.GetTopLocations().ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
