using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetAllUserReviews(int userId);
        public IEnumerable<Review> GetAllPropertyReviews(int propertyId);
        public Review? GetReviewById(int Id);
        public void Add(Review review);
        public void Update(Review review);
        public void Delete(Review review);
    }
}
