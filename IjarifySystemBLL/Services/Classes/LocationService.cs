using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class LocationService:ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
           _locationRepository = locationRepository;
        }
        public List<Location> GetAllLocationsByUser(int userId)
        {
            return _locationRepository.GetAllForUser(userId);
        }
    }
}
