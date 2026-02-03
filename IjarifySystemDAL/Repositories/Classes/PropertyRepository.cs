using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IjarifySystemDAL.Repositories.Classes
{
    public class PropertyRepository(IjarifyDbContext _context) : IPropertyRepository
    {
        public Task GetByIdAsync(int propertyID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Property>?> GetForPagination(int need, int skip)
        {
            return await _context.Properties
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Skip(skip)
                .Take(need)
                .ToListAsync();
        }

        public Task<int> PropertiesCount()
        {
            throw new NotImplementedException();
        }
    }
}
