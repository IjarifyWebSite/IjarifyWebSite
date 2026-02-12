using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Classes
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IjarifyDbContext _context;

        public BookingRepository(IjarifyDbContext context)
        {
            _context = context;
        }

        // Read Operations
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Location)
                .Include(b => b.user)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId)
        {
            return await _context.bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Location)
                .Include(b => b.user)
                .Where(b => b.UserID == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetPropertyBookingsAsync(int propertyId)
        {
            return await _context.bookings
                .Include(b => b.user)
                .Include(b => b.Property)
                .Where(b => b.PropertyID == propertyId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingWithDetailsAsync(int bookingId)
        {
            return await _context.bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Location)
                .Include(b => b.user)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync()
        {
            return await _context.bookings
                .Include(b => b.Property)
                .Include(b => b.user)
                .Where(b => b.Status == BookingStatus.Approved && b.Check_Out >= DateTime.Now)
                .ToListAsync();
        }

        // Write Operations
        public async Task AddAsync(Booking booking)
        {
            await _context.bookings.AddAsync(booking);
        }

        public void Update(Booking booking)
        {
            _context.bookings.Update(booking);
        }

        public void Delete(Booking booking)
        {
            _context.bookings.Remove(booking);
        }

        // Business Logic
        public async Task<bool> IsPropertyAvailableAsync(int propertyId, DateTime checkIn, DateTime checkOut)
        {
            var overlappingBookings = await _context.bookings
                .Where(b => b.PropertyID == propertyId
                    && b.Status == BookingStatus.Approved
                    && ((checkIn >= b.Check_In && checkIn < b.Check_Out)
                        || (checkOut > b.Check_In && checkOut <= b.Check_Out)
                        || (checkIn <= b.Check_In && checkOut >= b.Check_Out)))
                .AnyAsync();

            return !overlappingBookings;
        }

        // Save Changes
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<Property?> GetPropertyBasicInfo(int propertyId)
        {
            return await _context.Properties
                .Include(p => p.PropertyImages)
                .FirstOrDefaultAsync(p => p.Id == propertyId);
        }

        public async Task<IEnumerable<Booking>> GetPropertyOwnerBookingsAsync(int userId)
        {
            return await _context.bookings
                .Include(b => b.Property)
                .Include(b => b.user)
                .Where(b => b.Property.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

    }
}
