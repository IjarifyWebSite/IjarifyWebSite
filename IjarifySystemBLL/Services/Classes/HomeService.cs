using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.LocationsViewModel;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _HomeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _HomeRepository = homeRepository;
        }
        public IEnumerable<LocationViewModel> GetTopLocations()
        {
            var locations = _HomeRepository.GetTopLocations();
            var locationViewModels = locations.Select(loc => new LocationViewModel
            {
               City = loc.City,
               ImageUrl = loc.ImageUrl,
            }).ToList();
            return locationViewModels;
        }
    }
}
