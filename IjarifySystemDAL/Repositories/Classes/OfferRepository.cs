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
            return _Dbcontext.Offers.AsNoTracking()
                .Include(o=>o.Property)
                .FirstOrDefault(o => o.Id == id);
        }

        public void Update(Offer offer) => _Dbcontext.Offers.Update(offer);

        public void Delete(Offer offer) => _Dbcontext.Offers.Remove(offer);
        public int SaveChanges()
        {
            return _Dbcontext.SaveChanges();
        }

        public IEnumerable<Offer> GetAllForLocation(string LocationName, Expression<Func<Offer, bool>>? Condition = null)
        {
            var query = _Dbcontext.Offers.AsNoTracking().Include(o => o.Property).ThenInclude(o => o.Location).Where(o => o.Property.Location.City == LocationName);
            if (Condition == null)
            {
                return query.ToList();
            }
            return query.Where(Condition).ToList();
        }

        public IEnumerable<Offer> GetOffersWithPropertyAndLocation(Expression<Func<Offer, bool>>? Condition = null, string? Search = null, List<string>? Areas = null, List<string>? Compounds = null, List<decimal>? HotOffers = null)
        { 
            var query = _Dbcontext.Offers.AsNoTracking()
                .Include(o => o.Property).
                ThenInclude(p => p.Location).
                Include(l => l.Property).
                ThenInclude(p => p.PropertyImages).
                AsNoTracking().
                AsQueryable();
            if (Condition != null)
            {
                query = query.Where(Condition);
            }
            if (Search != null)
            {
              query = query.Where( o=>o.Property.Title.Contains(Search) || o.Property.Location.City.Contains(Search));

            }
            if (Areas != null && Areas.Any())
            {
                query = query.Where(o => Areas.Contains(o.Property.Location.City));
            }
            if (Compounds != null && Compounds.Any())
            {
                query = query.Where(o => Compounds.Contains(o.Property.Title));
            }
            if (HotOffers != null && HotOffers.Any())
            {
                query = query.Where(o => HotOffers.Contains((int)(o.DiscountPercentage)));
            }
            return query.ToList();
        }

        public IEnumerable<Offer> GetByUserId(int UserId)
        {
            return _Dbcontext.Offers.
                Include(o=>o.Property).
                ThenInclude(o=>o.Location)
                .Where(o => o.Property.UserId == UserId).ToList();
        }
    }
}
