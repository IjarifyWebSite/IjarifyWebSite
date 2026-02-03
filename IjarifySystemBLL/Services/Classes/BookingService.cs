using IjarifySystemBLL.DTOs.Bookings;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IPropertyRepository _propertyRepo;

        public BookingService(IBookingRepository bookingRepo, IPropertyRepository propertyRepo)
        {
            _bookingRepo = bookingRepo;
            _propertyRepo = propertyRepo;
        }

        public async Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return bookings.Select(MapToReadDto);
        }

        public async Task<BookingReadDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetBookingWithDetailsAsync(id);
            return booking == null ? null : MapToReadDto(booking);
        }

        public async Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepo.GetUserBookingsAsync(userId);
            return bookings.Select(MapToReadDto);
        }

        public async Task<IEnumerable<BookingReadDto>> GetPropertyBookingsAsync(int propertyId)
        {
            var bookings = await _bookingRepo.GetPropertyBookingsAsync(propertyId);
            return bookings.Select(MapToReadDto);
        }

        public async Task<BookingReadDto> CreateBookingAsync(BookingCreateDto dto, int userId)
        {
            // Validate dates
            if (dto.Check_In < DateTime.Now)
                throw new InvalidOperationException("Check-in date cannot be in the past");

            if (dto.Check_Out <= dto.Check_In)
                throw new InvalidOperationException("Check-out must be after check-in");

            // Check property exists
            //var property = await _propertyRepo.GetByIdAsync(dto.PropertyID);
            //if (property == null)
            //    throw new InvalidOperationException("Property not found");

            // Check availability
            var isAvailable = await _bookingRepo.IsPropertyAvailableAsync(
                dto.PropertyID, dto.Check_In, dto.Check_Out);

            if (!isAvailable)
                throw new InvalidOperationException("Property is not available for selected dates");

            var booking = new Booking
            {
                PropertyID = dto.PropertyID,
                UsertID = userId,
                Check_In = dto.Check_In,
                Check_Out = dto.Check_Out,
                TotalPrice = dto.TotalPrice,
                Status = BookingStatus.Pending
            };

            await _bookingRepo.AddAsync(booking);
            await _bookingRepo.SaveChangesAsync();

            // Get with details for proper mapping
            var createdBooking = await _bookingRepo.GetBookingWithDetailsAsync(booking.Id);
            return MapToReadDto(createdBooking!);
        }

        public async Task<BookingReadDto?> UpdateBookingAsync(int id, BookingUpdateDto dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return null;

            if (dto.Check_In.HasValue)
                booking.Check_In = dto.Check_In.Value;

            if (dto.Check_Out.HasValue)
                booking.Check_Out = dto.Check_Out.Value;

            if (dto.Status.HasValue)
                booking.Status = dto.Status.Value;

            if (dto.TotalPrice.HasValue)
                booking.TotalPrice = dto.TotalPrice.Value;

            _bookingRepo.Update(booking);
            await _bookingRepo.SaveChangesAsync();

            var updatedBooking = await _bookingRepo.GetBookingWithDetailsAsync(id);
            return MapToReadDto(updatedBooking!);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return false;

            _bookingRepo.Delete(booking);
            await _bookingRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveBookingAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return false;

            booking.Status = BookingStatus.Approved;
            _bookingRepo.Update(booking);
            await _bookingRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectBookingAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return false;

            booking.Status = BookingStatus.Rejected;
            _bookingRepo.Update(booking);
            await _bookingRepo.SaveChangesAsync();
            return true;
        }

        // Manual Mapping
        private BookingReadDto MapToReadDto(Booking booking)
        {
            return new BookingReadDto
            {
                Id = booking.Id,
                Check_In = booking.Check_In,
                Check_Out = booking.Check_Out,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                IsValid = booking.IsValid,
                CreatedAt = booking.CreatedAt,
                PropertyID = booking.PropertyID,
                PropertyTitle = booking.Property?.Title ?? string.Empty,
                PropertyAddress = booking.Property?.Location?.City ?? string.Empty,
                PropertyType = booking.Property?.Type,
                UserID = booking.UsertID,
                UserName = booking.user?.Name ?? string.Empty,
                UserEmail = booking.user?.Email ?? string.Empty,
                UserPhone = booking.user?.Phone ?? string.Empty
            };
        }
    }
}
