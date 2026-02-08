using IjarifySystemDAL.Entities;
using System.Linq;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IPropertyRepository
    {
        Task<Property?> GetByIdAsync(int id);
        Task<List<Property>> GetForPagination(int need, int skip);
        Task<int> PropertiesCount();
        Task<List<Amenity>> GetAllAmenities();
        Task<List<string>> GetAllCities();
        Task<List<string>> GetAllRegions();

        IQueryable<Property> GetQueryable();
    }
}