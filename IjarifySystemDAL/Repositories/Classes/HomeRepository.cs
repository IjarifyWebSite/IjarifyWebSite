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
    public class HomeRepository : IHomeRepository
    {
        private readonly IjarifyDbContext _DBContext;

        public HomeRepository(IjarifyDbContext dbContext)
        {
            _DBContext = dbContext;
        }
        public IEnumerable<Location> GetTopLocations()
        {
            var Locations = _DBContext.Locations.AsNoTracking().ToList();
            return Locations;
        }
    }
}
