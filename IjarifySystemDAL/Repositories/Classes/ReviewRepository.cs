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
    public class ReviewRepository : IReviewRepository
    {
        private readonly IjarifyDbContext dbContext;

        public ReviewRepository(IjarifyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Review review) => dbContext.reviews.Add(review);

        public void Delete(Review review) => dbContext.reviews.Remove(review);

        public IEnumerable<Review> GetAllPropertyReviews(int propertyId)
        {
            var propertyReviews = dbContext.reviews.Where(r => r.PropertyId == propertyId).ToList();
            return propertyReviews;
        }

        public IEnumerable<Review> GetAllUserReviews(int userId)
        {
            var userReviews = dbContext.reviews.Where(r => r.UserId == userId).ToList();
            return userReviews;
        }

        public Review? GetReviewById(int Id) => dbContext.reviews.Find(Id);

        public void Update(Review review) => dbContext.reviews.Update(review);
    }
}
