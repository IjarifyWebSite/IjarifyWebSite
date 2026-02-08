using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IjarifySystemDAL.Repositories.Classes
{
    public class PropertyRepository(IjarifyDbContext _context) : IPropertyRepository
    {
        // ✅ ADD THIS METHOD
        public IQueryable<Property> GetQueryable()
        {
            return _context.Properties.AsQueryable();
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Include(p => p.amenities)
                .Include(p => p.Reviews!).ThenInclude(r => r.user)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Property>> GetForPagination(int need, int skip)
        {
            return await _context.Properties
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Skip(skip)
                .Take(need)
                .ToListAsync();
        }

        public async Task<int> PropertiesCount()
        {
            return await _context.Properties.CountAsync();
        }

        public async Task<List<Amenity>> GetAllAmenities()
        {
            return await _context.amenities.ToListAsync();
        }

        public async Task<List<string>> GetAllCities()
        {
            return await _context.Locations
                .Select(l => l.City)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<string>> GetAllRegions()
        {
            return await _context.Locations
                .Select(l => l.Regoin) // ✅ Changed from Street to Regoin
                .Distinct()
                .ToListAsync();
        }
    }
}