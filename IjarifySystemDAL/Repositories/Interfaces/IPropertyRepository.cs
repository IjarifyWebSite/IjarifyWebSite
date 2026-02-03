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
        Task GetByIdAsync(int propertyID);
        public Task<List<Property>?> GetForPagination(int need, int skip);
        public Task<int> PropertiesCount();
        public Task<Property?> GetByIdAsync(int id);
    }
}
