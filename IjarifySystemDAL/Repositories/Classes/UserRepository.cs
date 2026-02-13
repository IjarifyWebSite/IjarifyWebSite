using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly IjarifyDbContext dbContext;

        public UserRepository(IjarifyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User? GetById(int id) => dbContext.Users.Find(id);

        public void Add(User user) => dbContext.Users.Add(user);

        public void Update(User user) => dbContext.Users.Update(user);

        public int SaveChanges() => dbContext.SaveChanges();

        public void Delete(User user) => dbContext.Users.Remove(user);

        public User? GetByPhoneNumber(string phoneNumber) => dbContext.Users.FirstOrDefault(u=>u.PhoneNumber==phoneNumber);
    }
}
