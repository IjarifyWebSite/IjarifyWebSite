using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.InquiryViewModels;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;

        public InquiryService(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        public UserInquiriesViewModel GetUserInquiries(int userId)
        {
            var inquiries = _inquiryRepository.GetAllByUserId(userId);

            var viewModel = new UserInquiriesViewModel
            {
                UserId = userId,
                UserName = inquiries.FirstOrDefault()?.User?.Name ?? "User",
                Inquiries = inquiries.Select(i => new InquiryItemViewModel
                {
                    InquiryId = i.Id,
                    PropertyId = i.PropertyId,
                    PropertyTitle = i.Property?.Title ?? "Unknown Property",
                    PropertyImage = i.Property?.PropertyImages?.FirstOrDefault()?.ImageUrl ?? "/images/no-image.jpg",
                    PropertyLocation = $"{i.Property?.Location?.City}, {i.Property?.Location?.Regoin}",
                    PropertyPrice = i.Property?.Price ?? 0,
                    SenderId = i.UserId,
                    SenderName = i.User?.Name ?? "Unknown",
                    SenderEmail = i.User?.Email ?? "",
                    SenderPhone = i.User?.PhoneNumber ?? "",
                    Message = i.Message,
                    CreatedAt = i.CreatedAt,
                    PropertyOwnerName = i.Property?.User?.Name ?? "Unknown Owner",
                    PropertyOwnerId = i.Property?.UserId ?? 0
                }).ToList()
            };

            return viewModel;
        }

        public PropertyInquiriesViewModel GetPropertyInquiries(int propertyId)
        {
            var inquiries = _inquiryRepository.GetAllByPropertyId(propertyId);
            var property = inquiries.FirstOrDefault()?.Property;

            var viewModel = new PropertyInquiriesViewModel
            {
                PropertyId = propertyId,
                PropertyTitle = property?.Title ?? "Unknown Property",
                Inquiries = inquiries.Select(i => new InquiryItemViewModel
                {
                    InquiryId = i.Id,
                    PropertyId = i.PropertyId,
                    PropertyTitle = i.Property?.Title ?? "Unknown Property",
                   
                    PropertyLocation = $"{i.Property?.Location?.City}, {i.Property?.Location?.Regoin}",
                    PropertyPrice = i.Property?.Price ?? 0,
                    SenderId = i.UserId,
                    SenderName = i.User?.Name ?? "Unknown",
                    SenderEmail = i.User?.Email ?? "",
                    SenderPhone = i.User?.PhoneNumber ?? "",
                    Message = i.Message,
                    CreatedAt = i.CreatedAt,
                    PropertyOwnerName = i.Property?.User?.Name ?? "Unknown Owner",
                    PropertyOwnerId = i.Property?.UserId ?? 0
                }).ToList()
            };

            return viewModel;
        }

        public InquiryDetailsViewModel? GetInquiryDetails(int inquiryId)
        {
            var inquiry = _inquiryRepository.GetById(inquiryId);

            if (inquiry == null)
                return null;

            var viewModel = new InquiryDetailsViewModel
            {
                InquiryId = inquiry.Id,
                PropertyId = inquiry.PropertyId,
                PropertyTitle = inquiry.Property?.Title ?? "Unknown Property",
                PropertyImage = inquiry.Property?.PropertyImages?.FirstOrDefault()?.ImageUrl ?? "/images/no-image.jpg",
                PropertyLocation = $"{inquiry.Property?.Location?.City}, {inquiry.Property?.Location?.Regoin}",
                PropertyPrice = inquiry.Property?.Price ?? 0,
                SenderId = inquiry.UserId,
                SenderName = inquiry.User?.Name ?? "Unknown",
                SenderEmail = inquiry.User?.Email ?? "",
                SenderPhone = inquiry.User?.PhoneNumber ?? "",
                PropertyOwnerId = inquiry.Property?.UserId ?? 0,
                PropertyOwnerName = inquiry.Property?.User?.Name ?? "Unknown Owner",
                PropertyOwnerEmail = inquiry.Property?.User?.Email ?? "",
                PropertyOwnerPhone = inquiry.Property?.User?.PhoneNumber ?? "",
                Message = inquiry.Message,
                CreatedAt = inquiry.CreatedAt
            };

            return viewModel;
        }

        public bool CreateInquiry(CreateInquiryViewModel model)
        {
            try
            {
                var inquiry = new Inquiry
                {
                    UserId = model.UserId,
                    PropertyId = model.PropertyId,
                    Message = model.Message,
                    CreatedAt = DateTime.Now
                };

                _inquiryRepository.Add(inquiry);
                var result = _inquiryRepository.SaveChanges();

                return result > 0;
            }
            catch (Exception ex)
            {
                // Throw with inner exception message for better debugging
                var errorMsg = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Failed to create inquiry: {errorMsg}", ex);
            }
        }

        public bool DeleteInquiry(int inquiryId, int userId)
        {
            try
            {
                var inquiry = _inquiryRepository.GetById(inquiryId);

                if (inquiry == null || inquiry.UserId != userId)
                    return false;

                _inquiryRepository.Delete(inquiry);
                var result = _inquiryRepository.SaveChanges();

                return result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}