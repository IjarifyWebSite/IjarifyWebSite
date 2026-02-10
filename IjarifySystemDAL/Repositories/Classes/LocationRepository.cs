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
    public class LocationRepository : ILocationRepository
    {
        public IjarifyDbContext _DbContext { get; }
        public LocationRepository(IjarifyDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public Location? GetByCity(string city)
        {
            return _DbContext.Locations.AsNoTracking().FirstOrDefault(l=>l.City==city);
        }

        public List<Location> GetAll()
        {
            return _DbContext.Locations.ToList();
        }
    }
}
