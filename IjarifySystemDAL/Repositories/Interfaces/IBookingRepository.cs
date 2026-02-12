using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        // Read Operations
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetUserBookingsAsync(int userId);
        Task<IEnumerable<Booking>> GetPropertyBookingsAsync(int propertyId);
        Task<Booking?> GetBookingWithDetailsAsync(int bookingId);
        Task<IEnumerable<Booking>> GetActiveBookingsAsync();
        Task<Property?> GetPropertyBasicInfo(int propertyId);
        Task<IEnumerable<Booking>> GetPropertyOwnerBookingsAsync(int userId);

        // Write Operations
        Task AddAsync(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);

        // Business Logic
        Task<bool> IsPropertyAvailableAsync(int propertyId, DateTime checkIn, DateTime checkOut);

        // Save Changes
        Task<int> SaveChangesAsync();

    }
}
