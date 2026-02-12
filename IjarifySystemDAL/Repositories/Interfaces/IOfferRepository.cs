using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
 

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetAll(Expression< Func<Offer,bool>>? Condition=null);
        IEnumerable<Offer> GetAllForLocation(string LocationName, Expression<Func<Offer, bool>>? Condition = null);
        IEnumerable<Offer> GetOffersWithPropertyAndLocation(Expression<Func<Offer, bool>>? Condition = null, string? Search = null, List<string>? Areas = null, List<string>? Compounds = null, List<decimal>? HotOffers=null);
        IEnumerable<Offer> GetByUserId(int UserId);
        Offer? GetById(int id);
        void Add(Offer offer);
        void Update(Offer offer);
        void Delete(Offer offer);
        int SaveChanges();

    }
}
