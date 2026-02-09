using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User? GetById(int id);
        public void Update(User user);
        public void Add(User user);
        public void Delete(User user);
        public int SaveChanges();
    }
}
