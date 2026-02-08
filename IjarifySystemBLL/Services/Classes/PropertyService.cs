using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AmenityViewModels;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using IjarifySystemDAL.Entities.Enums;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class PropertyService(IPropertyRepository _repo) : IPropertyService
    {
        // ✅ IMPLEMENT THE FIRST OVERLOAD (without filter)
        public async Task<(List<PropertyIndexViewModel>?, int, int)> GetPagination(int pageSize, int page)
        {
            int totalProperties = await _repo.PropertiesCount();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            totalPages = Math.Max(1, totalPages);
            int currentPage = Math.Max(1, Math.Min(page, totalPages));

            var properties = await _repo.GetForPagination(pageSize, (currentPage - 1) * pageSize);

            var propertyViewModels = properties.Select(p => new PropertyIndexViewModel
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

            return (propertyViewModels, currentPage, totalPages);
        }

        // ✅ SECOND OVERLOAD (with filter) - CORRECTED
        public async Task<PropertyIndexPageViewModel> GetPagination(int pageSize, int page, PropertyFilterViewModel filter)
        {
            // Build query with filters
            var query = _repo.GetQueryable()
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Include(p => p.amenities)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter?.PropertyType) && Enum.TryParse<PropertyType>(filter.PropertyType, out var propType))
            {
                query = query.Where(p => p.Type == propType);
            }

            if (!string.IsNullOrEmpty(filter?.ListingType) && Enum.TryParse<PropertyListingType>(filter.ListingType, out var listType))
            {
                query = query.Where(p => p.ListingType == listType);
            }

            if (filter?.MinPrice.HasValue == true)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter?.MaxPrice.HasValue == true)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (filter?.MinBedrooms.HasValue == true)
            {
                query = query.Where(p => p.BedRooms >= filter.MinBedrooms.Value);
            }

            if (filter?.MinBathrooms.HasValue == true)
            {
                query = query.Where(p => p.BathRooms >= filter.MinBathrooms.Value);
            }

            if (!string.IsNullOrEmpty(filter?.City))
            {
                query = query.Where(p => p.Location.City == filter.City);
            }

            if (!string.IsNullOrEmpty(filter?.Region))
            {
                query = query.Where(p => p.Location.Regoin == filter.Region);
            }

            if (filter?.AmenityIds != null && filter.AmenityIds.Any())
            {
                query = query.Where(p => p.amenities.Any(a => filter.AmenityIds.Contains(a.Id)));
            }

            // Get total count for pagination
            int totalProperties = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            totalPages = Math.Max(1, totalPages);
            int currentPage = Math.Max(1, Math.Min(page, totalPages));

            // Get paginated results
            var properties = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Map to ViewModels
            var propertyViewModels = properties.Select(p => new PropertyIndexViewModel
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

            // ✅ FIX: Map amenities from Entity to ViewModel
            var amenitiesEntities = await _repo.GetAllAmenities();
            var amenitiesViewModels = amenitiesEntities.Select(a => new AmenityViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Icon = a.Icon,
                Category = a.Catigory.ToString()
            }).ToList();

            var cities = await _repo.GetAllCities();
            var regions = await _repo.GetAllRegions();

            // Build and return the complete page view model
            var pageViewModel = new PropertyIndexPageViewModel
            {
                Properties = propertyViewModels,
                Filter = filter ?? new PropertyFilterViewModel(),
                PropertyTypes = Enum.GetNames(typeof(PropertyType)).ToList(),
                ListingTypes = Enum.GetNames(typeof(PropertyListingType)).ToList(),
                Amenities = amenitiesViewModels, // ✅ Use mapped ViewModels
                Cities = cities,
                Regions = regions,
                CurrentPage = currentPage,
                TotalPages = totalPages
            };

            return pageViewModel;
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