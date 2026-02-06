using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemDAL.Entities;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IFavouriteRepository
    {
        //All Favourites or with Condition
        IEnumerable<Favourite> GetAll(Expression<Func<Favourite, bool>>? Condition = null);

        //Get Favourite by User id
        IEnumerable<Favourite> GetAllByUserId(int userId, Expression<Func<Favourite, bool>>? Condition = null);

        
        Favourite? GetById(int id);

        //CHECK IF FAVOURITE EXISTS
        Favourite? GetByUserIdAndPropertyId(int userId, int propertyId);

        void Add(Favourite favourite);


        void Delete(Favourite favourite);

        int SaveChanges();




    }
}
