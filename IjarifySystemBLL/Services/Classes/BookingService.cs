using IjarifySystemBLL.DTOs.Bookings;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.Booking;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using IjarifySystemDAL.Repositories.Interfaces;

namespace IjarifySystemBLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        // Constructor
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

     
        public async Task<PropertyBasicInfoDto?> GetPropertyBasicInfo(int propertyId)
        {
            var property = await _bookingRepository.GetPropertyBasicInfo(propertyId);

            if (property == null)
                return null;

            return new PropertyBasicInfoDto
            {
                Id = property.Id,
                Title = property.Title,
                Price = property.Price,
                ImageUrl = property.PropertyImages?.FirstOrDefault()?.ImageUrl
            };
        }

        
        public async Task<BookingReadDto> CreateBookingAsync(BookingCreateDto createDto, int userId)
        {
   
            bool isAvailable = await _bookingRepository.IsPropertyAvailableAsync(
                createDto.PropertyID,
                createDto.Check_In,
                createDto.Check_Out
            );

            if (!isAvailable)
            {
                throw new InvalidOperationException("Property is not available for the selected dates");
            }

            var booking = new Booking
            {
                PropertyID = createDto.PropertyID,
                UserID = userId,
                Check_In = createDto.Check_In,
                Check_Out = createDto.Check_Out,
                TotalPrice = createDto.TotalPrice,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.Now
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return await MapToReadDto(booking);
        }

        public async Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetUserBookingsAsync(userId);
            return bookings.Select(MapToReadDtoSync);
        }

        public async Task<BookingReadDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingWithDetailsAsync(id);

            if (booking == null)
                return null;

            return await MapToReadDto(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
                return false;

            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Only pending bookings can be cancelled");

            _bookingRepository.Delete(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }


        // Helper Methods
        private async Task<BookingReadDto> MapToReadDto(Booking booking)
        {
            var bookingWithDetails = await _bookingRepository.GetBookingWithDetailsAsync(booking.Id);
            return MapToReadDtoSync(bookingWithDetails ?? booking);
        }

        private BookingReadDto MapToReadDtoSync(Booking booking)
        {
            return new BookingReadDto
            {
                Id = booking.Id,
                PropertyID = booking.PropertyID,
                PropertyTitle = booking.Property?.Title ?? "Unknown Property",
                PropertyAddress = booking.Property?.Location != null
                    ? $"{booking.Property.Location.Street}, {booking.Property.Location.Regoin}, {booking.Property.Location.City}"
                    : "Unknown Location",
                PropertyType = booking.Property?.Type,
                UserID = booking.UserID,
                UserName = booking.user?.Name ?? "Unknown User",
                UserEmail = booking.user?.Email ?? "",
                UserPhone = booking.user?.PhoneNumber ?? "",
                Check_In = booking.Check_In,
                Check_Out = booking.Check_Out,
                Status = booking.Status,
                TotalPrice = booking.TotalPrice,
                IsValid = booking.IsValid,
                CreatedAt = booking.CreatedAt
            };
        }

        public Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingReadDto>> GetPropertyBookingsAsync(int propertyId)
        {
            throw new NotImplementedException();
        }

        public Task<BookingReadDto?> UpdateBookingAsync(int id, BookingUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApproveBookingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RejectBookingAsync(int id)
        {
            throw new NotImplementedException();
        }
        // ✅ Get Property Owner Requests
        public async Task<MyRequestsViewModel> GetPropertyOwnerRequestsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetPropertyOwnerBookingsAsync(userId);
            var bookingDtos = bookings.Select(MapToReadDtoSync).ToList();

            return new  MyRequestsViewModel
            {
                PendingRequests = bookingDtos
                    .Where(b => b.Status == BookingStatus.Pending)
                    .ToList(),

                ApprovedRequests = bookingDtos
                    .Where(b => b.Status == BookingStatus.Approved)
                    .ToList(),

                RejectedRequests = bookingDtos
                    .Where(b => b.Status == BookingStatus.Rejected)
                    .ToList()
            };
        }

        // ✅ Approve Booking
        public async Task<bool> ApproveBookingAsync(int bookingId, int propertyOwnerId)
        {
            var booking = await _bookingRepository.GetBookingWithDetailsAsync(bookingId);

            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            if (booking.Property?.UserId != propertyOwnerId)
                throw new UnauthorizedAccessException("You can only approve bookings on your own properties");

            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Only pending bookings can be approved");

            booking.Status = BookingStatus.Approved;
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectBookingAsync(int bookingId, int propertyOwnerId)
        {
            var booking = await _bookingRepository.GetBookingWithDetailsAsync(bookingId);

            if (booking == null)
                throw new KeyNotFoundException("Booking not found");

            if (booking.Property?.UserId != propertyOwnerId)
                throw new UnauthorizedAccessException("You can only reject bookings on your own properties");

            if (booking.Status != BookingStatus.Pending)
                throw new InvalidOperationException("Only pending bookings can be rejected");

            booking.Status = BookingStatus.Rejected;
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        //Task<MyRequestsViewModel> IBookingService.GetPropertyOwnerRequestsAsync(int userId)
        //{
        //    throw new NotImplementedException();
        //}

        
    }
}