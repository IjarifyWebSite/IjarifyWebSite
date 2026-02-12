using IjarifySystemBLL.DTOs.Bookings;
using IjarifySystemBLL.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync();
        Task<BookingReadDto?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(int userId);
        Task<IEnumerable<BookingReadDto>> GetPropertyBookingsAsync(int propertyId);
        Task<BookingReadDto> CreateBookingAsync(BookingCreateDto dto, int userId);
        Task<BookingReadDto?> UpdateBookingAsync(int id, BookingUpdateDto dto);
        Task<PropertyBasicInfoDto?> GetPropertyBasicInfo(int propertyId);
        Task<MyRequestsViewModel> GetPropertyOwnerRequestsAsync(int userId);
        Task<bool> ApproveBookingAsync(int bookingId, int propertyOwnerId);
        Task<bool> RejectBookingAsync(int bookingId, int propertyOwnerId);
        Task<bool> DeleteBookingAsync(int id);
        Task<bool> ApproveBookingAsync(int id);
        Task<bool> RejectBookingAsync(int id);
    }
}
