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
        

        public async Task<List<Property>?> GetForPagination(int need, int skip)
        {
            return await _context.Properties
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Include(p => p.Reviews!)
                .Skip(skip)
                .Take(need)
                .ToListAsync();
        }
        

        public async Task<int> PropertiesCount()
        {
            int count = await _context.Properties.CountAsync();
           return count;
        }
    }
}
