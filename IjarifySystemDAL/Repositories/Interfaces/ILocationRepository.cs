using IjarifySystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        public List<Location> GetAllForUser(int UserId);
        Location? GetByCity(string city);
    }
}
