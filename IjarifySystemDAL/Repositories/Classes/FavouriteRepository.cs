using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IjarifySystemDAL.Repositories.Classes
{
    public class FavouriteRepository : IFavouriteRepository
    {


        private readonly IjarifyDbContext _Dbcontext;
        public FavouriteRepository(IjarifyDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }


        public IEnumerable<Favourite> GetAll(Expression<Func<Favourite, bool>>? Condition = null)
        {
            

            if (Condition == null)
            {
                return _Dbcontext.favourites
                    .Include(f=> f.Property)
                    .Include(f=> f.User)
                    .ToList();
            }
            else
            {

                return _Dbcontext.favourites
                    .Include(f => f.Property)
                    .Include(f => f.User)
                    .Where(Condition)
                    .ToList();
            }

        }





        public IEnumerable<Favourite> GetAllByUserId(int userId, Expression<Func<Favourite, bool>>? Condition = null)
        {

            var query = _Dbcontext.favourites
                .Include(f=> f.Property)
                    .ThenInclude(Property => Property.Location)
                .Include(p=> p.Property)
                    .ThenInclude(p => p.PropertyImages)
                .Where(f => f.UserId == userId);
            if (Condition != null)
            {
                                query = query.Where(Condition);
            }
            return query.ToList();

        }



        public Favourite? GetById(int id)
        {
            return _Dbcontext.favourites
                .Include(f=>f.Property)
                .Include(f=>f.User)
                .FirstOrDefault(f => f.Id == id);
        }




        public Favourite? GetByUserIdAndPropertyId(int userId, int propertyId)
        {
           
            return _Dbcontext.favourites
                .FirstOrDefault(f=> f.UserId == userId && f.PropertyId == propertyId);
        }




        public void Add(Favourite favourite)
        {
            _Dbcontext.favourites.Add(favourite);
        }

        public void Delete(Favourite favourite)
        {
            _Dbcontext.favourites.Remove(favourite);
        }

    

        public int SaveChanges()
        {
                return _Dbcontext.SaveChanges();
        }
    }
}
