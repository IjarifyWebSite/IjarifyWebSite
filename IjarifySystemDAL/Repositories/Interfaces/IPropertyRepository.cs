using IjarifySystemDAL.Entities;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IPropertyRepository
    {
        IQueryable<Property> GetQueryable();
        Task<Property?> GetByIdAsync(int id);
        Task<List<Property>> GetForPagination(int need, int skip);
        Task<int> PropertiesCount();
        Task<List<Amenity>> GetAllAmenities();
        Task<List<string>> GetAllCities();
        Task<List<string>> GetAllRegions();

        // CRUD Operations
        Task AddAsync(Property property);
        void Update(Property property);
        void Delete(Property property);
        Task SaveAsync();

        // Lookup Helpers
        Task<Location?> GetLocationAsync(string city, string region, string street);
        Task<Amenity?> GetAmenityByNameAsync(string name);
        Task<List<Location>> GetTopLocationsWithPropertyCountAsync(int count);
    }
}