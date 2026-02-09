using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IjarifySystemDAL.Repositories.Classes
{
    public class PropertyRepository(IjarifyDbContext _context) : IPropertyRepository
    {
        public IQueryable<Property> GetQueryable() => _context.Properties.AsQueryable();

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Include(p => p.amenities)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Property>> GetForPagination(int need, int skip)
        {
            return await _context.Properties
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Skip(skip).Take(need).ToListAsync();
        }

        public async Task<int> PropertiesCount() => await _context.Properties.CountAsync();

        public async Task<List<Amenity>> GetAllAmenities() => await _context.amenities.ToListAsync();

        public async Task<List<string>> GetAllCities() => await _context.Locations.Select(l => l.City).Distinct().ToListAsync();

        public async Task<List<string>> GetAllRegions() => await _context.Locations.Select(l => l.Regoin).Distinct().ToListAsync();

        public async Task AddAsync(Property property) => await _context.Properties.AddAsync(property);

        public void Update(Property property) => _context.Properties.Update(property);

        public void Delete(Property property) => _context.Properties.Remove(property);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<Location?> GetLocationAsync(string city, string region, string street)
        {
            return await _context.Locations.FirstOrDefaultAsync(l =>
                l.City == city && l.Regoin == region && l.Street == street);
        }

        public async Task<Amenity?> GetAmenityByNameAsync(string name)
        {
            return await _context.amenities.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
        }
    }
}