using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AmenityViewModels;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class PropertyService(IPropertyRepository _repo) : IPropertyService
    {
        public async Task<(List<PropertyIndexViewModel>?, int, int)> GetPagination(int pageSize, int page)
        {
            var properties = await _repo.GetForPagination(pageSize, (page - 1) * pageSize);
            int pages = (int)Math.Ceiling(await _repo.PropertiesCount() / (double)pageSize);
            page = Math.Max(1, Math.Min(page, pages));

            var vmList = properties.Select(p => new PropertyIndexViewModel
            {
                Id = p.Id,
                Name = p.Title,
                Price = p.Price,
                Location = $"{p.Location.Street}, {p.Location.City}, {p.Location.Regoin}",
                BedRooms = p.BedRooms,
                BathRooms = p.BathRooms,
                Area = p.Area,
                ListingType = p.ListingType.ToString(),
                PropertyType = p.Type.ToString(),
                MainImage = p.PropertyImages?.FirstOrDefault()?.ImageUrl ?? "assets/img/real-estate/default-property.webp",
                TotalImages = p.PropertyImages?.Count ?? 0,
                IsNew = (DateTime.Now - p.CreatedAt).TotalDays <= 30,
                AgentName = p.User.Name,
                AgentPhone = p.User.Phone,
                AgentAvatar = p.User.ImageUrl ?? "assets/img/real-estate/default-agent.webp"
            }).ToList();

            return (vmList, pages, page);
        }
        public async Task<PropertyDetailsViewModel?> GetPropertyDetails(int id)
        {
            var property = await _repo.GetByIdAsync(id);

            if (property == null) return null;

            var viewModel = new PropertyDetailsViewModel
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                ListingType = property.ListingType.ToString(),
                PropertyType = property.Type.ToString(),

                // Property Stats
                BedRooms = property.BedRooms,
                BathRooms = property.BathRooms,
                Area = property.Area,
                

                // Location
                Street = property.Location.Street,
                City = property.Location.City,
                Region = property.Location.Regoin,
                FullAddress = $"{property.Location.Street}, {property.Location.City}, {property.Location.Regoin}",
                Latitude = property.Location.Latitude,
                Longitude = property.Location.Longitude,
                NeighborhoodInfo = $"Located in {property.Location.Regoin}, this property offers convenient access to local amenities and attractions.",

                // Images
                Images = property.PropertyImages?.Select(img => new PropertyImageViewModel
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    AltText = property.Title
                }).ToList() ?? new List<PropertyImageViewModel>(),

                // Amenities
                Amenities = property.amenities?.Select(a => new AmenityViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Icon = a.Icon,
                    Category = a.Catigory.ToString()
                }).ToList() ?? new List<AmenityViewModel>(),

                // Agent Info
                AgentId = property.UserId,
                AgentName = property.User.Name,
                AgentTitle = "Licensed Real Estate Agent",
                AgentPhone = property.User.Phone,
                AgentEmail = property.User.Email,
                AgentAvatar = property.User.ImageUrl ?? "assets/img/real-estate/default-agent.webp",

                // Additional
                CreatedAt = property.CreatedAt,
                IsNew = (DateTime.Now - property.CreatedAt).TotalDays <= 30
            };

            return viewModel;
        }
    }
}
