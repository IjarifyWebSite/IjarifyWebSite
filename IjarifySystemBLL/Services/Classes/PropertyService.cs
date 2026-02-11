using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AmenityViewModels;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class PropertyService(IPropertyRepository _repo) : IPropertyService
    {
        // Using Directory.GetCurrentDirectory() to avoid hosting environment issues
        private readonly string _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public async Task<PropertyIndexPageViewModel> GetPagination(int pageSize, int page, PropertyFilterViewModel filter)
        {
            var query = _repo.GetQueryable()
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Include(p => p.amenities)
                .Include(p => p.Reviews!)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter?.PropertyType) && Enum.TryParse<PropertyType>(filter.PropertyType, out var propType))
            {
                query = query.Where(p => p.Type == propType);
            }

            if (!string.IsNullOrEmpty(filter?.ListingType) && Enum.TryParse<PropertyListingType>(filter.ListingType, out var listType))
            {
                query = query.Where(p => p.ListingType == listType);
            }

            if (filter?.MinPrice.HasValue == true) query = query.Where(p => p.Price >= filter.MinPrice.Value);
            if (filter?.MaxPrice.HasValue == true) query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            if (filter?.MinBedrooms.HasValue == true) query = query.Where(p => p.BedRooms >= filter.MinBedrooms.Value);
            if (filter?.MinBathrooms.HasValue == true) query = query.Where(p => p.BathRooms >= filter.MinBathrooms.Value);
            if (!string.IsNullOrEmpty(filter?.City)) query = query.Where(p => p.Location.City == filter.City);
            if (!string.IsNullOrEmpty(filter?.Region)) query = query.Where(p => p.Location.Regoin == filter.Region);

            if (filter?.AmenityIds != null && filter.AmenityIds.Any())
            {
                query = query.Where(p => p.amenities.Any(a => filter.AmenityIds.Contains(a.Id)));
            }

            int totalProperties = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            totalPages = Math.Max(1, totalPages);
            int currentPage = Math.Max(1, Math.Min(page, totalPages));

            var properties = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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
                AgentPhone = p.User.PhoneNumber,
                AgentAvatar = p.User.ImageUrl ?? "assets/img/real-estate/default-agent.webp",
                Reviews = new PropertyReviewsViewModel
                {
                    PropertyId = p.Id,
                    AverageRating = p.Reviews != null && p.Reviews.Any()? p.Reviews.Average(r => r.Rating): 0,
                    TotalReviews = p.Reviews?.Count ?? 0
                }
            }).ToList();

            var amenitiesEntities = await _repo.GetAllAmenities();
            var amenitiesViewModels = amenitiesEntities.Select(a => new AmenityViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Icon = a.Icon,
                Category = a.Category.ToString()
            }).ToList();

            return new PropertyIndexPageViewModel
            {
                Properties = propertyViewModels,
                Filter = filter ?? new PropertyFilterViewModel(),
                PropertyTypes = Enum.GetNames(typeof(PropertyType)).ToList(),
                ListingTypes = Enum.GetNames(typeof(PropertyListingType)).ToList(),
                Amenities = amenitiesViewModels,
                Cities = await _repo.GetAllCities(),
                Regions = await _repo.GetAllRegions(),
                CurrentPage = currentPage,
                TotalPages = totalPages
            };
        }

        public async Task<PropertyDetailsViewModel?> GetPropertyDetails(int id, int? currentUserId = null)
        {
            var property = await _repo.GetByIdAsync(id);
            if (property == null) return null;

            return new PropertyDetailsViewModel
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                ListingType = property.ListingType.ToString(),
                PropertyType = property.Type.ToString(),
                BedRooms = property.BedRooms,
                BathRooms = property.BathRooms,
                Area = property.Area,
                Street = property.Location.Street,
                City = property.Location.City,
                Region = property.Location.Regoin,
                FullAddress = $"{property.Location.Street}, {property.Location.City}, {property.Location.Regoin}",
                Latitude = property.Location.Latitude,
                Longitude = property.Location.Longitude,
                NeighborhoodInfo = $"Located in {property.Location.Regoin}, this property offers convenient access to local amenities.",
                Images = property.PropertyImages?.Select(img => new PropertyImageViewModel
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    AltText = property.Title
                }).ToList() ?? new List<PropertyImageViewModel>(),
                Amenities = property.amenities?.Select(a => new AmenityViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Icon = a.Icon,
                    Category = a.Category.ToString()
                }).ToList() ?? new List<AmenityViewModel>(),
                AgentId = property.UserId,
                AgentName = property.User.Name,
                AgentTitle = "Licensed Real Estate Agent",
                //AgentPhone = property.User.Phone,
                AgentEmail = property.User.Email,
                AgentAvatar = property.User.ImageUrl ?? "assets/img/real-estate/default-agent.webp",
                CreatedAt = property.CreatedAt,
                IsNew = (DateTime.Now - property.CreatedAt).TotalDays <= 30,

                 Reviews = property.Reviews?.Select(r => new ReviewItemViewModel
                 {
                     ReviewId = r.Id,
                     Comment = r.Comment,
                     Rating = r.Rating,
                     UserName = r.user.Name,
                     UserImage = r.user.ImageUrl,
                     CreatedAt = r.CreatedAt,
                     PropertyId = r.PropertyId,
                     IsOwner = r.UserId == currentUserId
                 }).ToList() ?? new List<ReviewItemViewModel>()
            };
        }

        public async Task CreatePropertyAsync(CreatePropertyViewModel model, int userId)
        {
            var property = new Property
            {
                CreatedAt = DateTime.Now,
                UserId = userId
            };

            await MapViewModelToEntity(model, property);
            await _repo.AddAsync(property);
            await _repo.SaveAsync();
        }

        public async Task UpdatePropertyAsync(int id, CreatePropertyViewModel model, int userId)
        {
            var property = await _repo.GetByIdAsync(id);
            if (property == null) return;

            // Delete old physical files
            DeletePhysicalImages(property.PropertyImages);

            // Clear collections for replace-all strategy
            property.PropertyImages.Clear();
            property.amenities.Clear();

            await MapViewModelToEntity(model, property);

            // Ensure userId is preserved or updated
            property.UserId = userId;

            _repo.Update(property);
            await _repo.SaveAsync();
        }

        public async Task DeletePropertyAsync(int id)
        {
            var property = await _repo.GetByIdAsync(id);
            if (property == null) return;

            DeletePhysicalImages(property.PropertyImages);
            _repo.Delete(property);
            await _repo.SaveAsync();
        }

        public async Task<CreatePropertyViewModel?> GetPropertyForEditAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new CreatePropertyViewModel
            {
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Area = p.Area,
                BedRooms = p.BedRooms,
                BathRooms = p.BathRooms,
                City = p.Location.City,
                Region = p.Location.Regoin,
                Street = p.Location.Street,
                PropertyType = p.Type.ToString(),
                ListingType = p.ListingType.ToString()
            };
        }

        private async Task MapViewModelToEntity(CreatePropertyViewModel model, Property property)
        {
            property.Title = model.Title;
            property.Description = model.Description;
            property.Price = model.Price;
            property.Area = model.Area;
            property.BedRooms = model.BedRooms;
            property.BathRooms = model.BathRooms;
            property.Type = Enum.Parse<PropertyType>(model.PropertyType);
            property.ListingType = Enum.Parse<PropertyListingType>(model.ListingType);

            // Handle Location
            var loc = await _repo.GetLocationAsync(model.City, model.Region, model.Street);
            property.Location = loc ?? new Location
            {
                City = model.City,
                Regoin = model.Region,
                Street = model.Street
            };

            // Handle Main Image
            if (model.MainImage != null)
            {
                var path = await SaveFile(model.MainImage);
                property.PropertyImages.Add(new PropertyImages { ImageUrl = path });
            }

            // Handle Gallery Images
            if (model.GalleryImages != null)
            {
                foreach (var file in model.GalleryImages)
                {
                    var path = await SaveFile(file);
                    property.PropertyImages.Add(new PropertyImages { ImageUrl = path });
                }
            }

            // Handle Amenities
            if (model.Amenities != null)
            {
                foreach (var amVM in model.Amenities.Values)
                {
                    var existingAmenity = await _repo.GetAmenityByNameAsync(amVM.Name);
                    property.amenities.Add(existingAmenity ?? new Amenity
                    {
                        Name = amVM.Name,
                        // --- FIX IS BELOW: Changed <Amenity> to <AminityCategory> ---
                        Category = Enum.Parse<AminityCategory>(amVM.Category)
                    });
                }
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string folder = Path.Combine(_rootPath, "imgs");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fullPath = Path.Combine(folder, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/imgs/{fileName}";
        }

        private void DeletePhysicalImages(ICollection<PropertyImages> images)
        {
            if (images == null) return;

            foreach (var img in images)
            {
                var fullPath = Path.Combine(_rootPath, img.ImageUrl.TrimStart('/'));
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }
}