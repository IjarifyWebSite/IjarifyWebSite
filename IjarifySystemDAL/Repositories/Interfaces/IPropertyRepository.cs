using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    
        public interface IPropertyRepository
        {
            Task<Property?> GetByIdAsync(int id);
            Task<List<Property>> GetForPagination(int need, int skip);
            Task<int> PropertiesCount();

            // New Methods
            Task<List<Amenity>> GetAllAmenities();
            Task<List<string>> GetAllCities();
            Task<List<string>> GetAllRegions();
        }
    }

