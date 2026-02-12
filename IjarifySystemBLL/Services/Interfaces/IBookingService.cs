using IjarifySystemBLL.DTOs.Bookings;
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
        Task<bool> DeleteBookingAsync(int id);
        Task<bool> ApproveBookingAsync(int id);
        Task<bool> RejectBookingAsync(int id);
    }
}
