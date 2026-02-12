using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemDAL.Entities;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IInquiryRepository
    {


       
        IEnumerable<Inquiry> GetAll(Expression<Func<Inquiry, bool>>? Condition = null);

        
        IEnumerable<Inquiry> GetAllByUserId(int userId, Expression<Func<Inquiry, bool>>? Condition = null);

       
        IEnumerable<Inquiry> GetAllByPropertyId(int propertyId, Expression<Func<Inquiry, bool>>? Condition = null);

       
        Inquiry? GetById(int id);

     
        void Add(Inquiry inquiry);

        
   

       
        void Delete(Inquiry inquiry);

   
        int SaveChanges();
    }
}
