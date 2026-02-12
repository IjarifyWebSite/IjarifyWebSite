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
    public class InquiryRepository : IInquiryRepository
    {
        private readonly IjarifyDbContext _context;

        public InquiryRepository(IjarifyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Inquiry> GetAll(Expression<Func<Inquiry, bool>>? Condition = null)
        {
            if (Condition == null)
            {
                return _context.Inquiries
                    .Include(i => i.User)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.Location)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.User) // Property owner
                    .OrderByDescending(i => i.CreatedAt)
                    .ToList();
            }
            else
            {
                return _context.Inquiries
                    .Include(i => i.User)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.Location)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.User)
                    .Where(Condition)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToList();
            }
        }

        public IEnumerable<Inquiry> GetAllByUserId(int userId, Expression<Func<Inquiry, bool>>? Condition = null)
        {
            if (Condition == null)
            {
                return _context.Inquiries
                    .Include(i => i.User)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.Location)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.User)
                    .Where(i => i.UserId == userId)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToList();
            }
            else
            {
                return _context.Inquiries
                    .Include(i => i.User)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.Location)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.User)
                    .Where(i => i.UserId == userId)
                    .Where(Condition)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToList();
            }
        }

        public IEnumerable<Inquiry> GetAllByPropertyId(int propertyId, Expression<Func<Inquiry, bool>>? Condition = null)
        {
            if (Condition == null)
            {
                return _context.Inquiries
                    .Include(i => i.User)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.Location)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.User)
                    .Where(i => i.PropertyId == propertyId)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToList();
            }
            else
            {
                return _context.Inquiries
                    .Include(i => i.User)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.Location)
                    .Include(i => i.Property)
                        .ThenInclude(p => p.User)
                    .Where(i => i.PropertyId == propertyId)
                    .Where(Condition)
                    .OrderByDescending(i => i.CreatedAt)
                    .ToList();
            }
        }

        public Inquiry? GetById(int id)
        {
            return _context.Inquiries
                .Include(i => i.User)
                .Include(i => i.Property)
                    .ThenInclude(p => p.Location)
                .Include(i => i.Property)
                    .ThenInclude(p => p.User)
                .FirstOrDefault(i => i.Id == id);
        }

        public void Add(Inquiry inquiry)
        {
            _context.Inquiries.Add(inquiry);
        }

     
        public void Delete(Inquiry inquiry)
        {
            _context.Inquiries.Remove(inquiry);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
