using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Classes
{
    public class OfferRepository : IOfferRepository
    {
        private readonly IjarifyDbContext _Dbcontext;

        public OfferRepository(IjarifyDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        public void Add(Offer offer) => _Dbcontext.Offers.Add(offer);


        public IEnumerable<Offer> GetAll(Expression<Func<Offer, bool>>? Condition = null)
        {
            if (Condition == null) return _Dbcontext.Offers.AsNoTracking().ToList();
            return _Dbcontext.Offers.AsNoTracking().Where(Condition).ToList();
        }

        public Offer? GetById(int id)
        {
            return _Dbcontext.Offers.AsNoTracking().FirstOrDefault(o => o.Id == id);
        }

        public void Update(Offer offer) => _Dbcontext.Offers.Update(offer);

        public void Delete(Offer offer) => _Dbcontext.Offers.Remove(offer);
        public int SaveChanges()
        {
            return _Dbcontext.SaveChanges();
        }

        public IEnumerable<Offer> GetAllForLocation(int locationId, Expression<Func<Offer, bool>>? Condition = null)
        {
            var query = _Dbcontext.Offers.AsNoTracking().Where(o => o.Property.LocationId == locationId);
            if (Condition == null)
            {
                return query.ToList();
            }
            return query.Where(Condition).ToList();
        }

    }
}
