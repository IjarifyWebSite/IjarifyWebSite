using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AmenityViewModels;
using IjarifySystemBLL.ViewModels.HomeViewModels;
using IjarifySystemBLL.ViewModels.PropertyViewModels;
using IjarifySystemBLL.ViewModels.ReviewsViewModels;
using IjarifySystemDAL.Data.Context;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Entities.Enums;
using IjarifySystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class PropertyService(IPropertyRepository _repo, IFavouriteRepository _favouriteRepo,IjarifyDbContext _context) : IPropertyService
    {
        // Using Directory.GetCurrentDirectory() to avoid hosting environment issues
        private readonly string _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public async Task<PropertyIndexPageViewModel> GetPagination(int pageSize, int page, PropertyFilterViewModel filter, int? currentUserId = null)
        {
            var query = _repo.GetQueryable()
                .Include(p => p.User)
                .Include(p => p.Location)
                .Include(p => p.PropertyImages)
                .Include(p => p.amenities)
                .Include(p => p.Reviews)
                .AsQueryable();

            // Apply filters...
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

            // Calculate pagination variables
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var currentPage = page;

            // Get properties with pagination
            var properties = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Get user favorites if logged in
            var favoritePropertyIds = new HashSet<int>();
            if (currentUserId.HasValue)
            {
                var favorites = _favouriteRepo.GetAllByUserId(currentUserId.Value);
                foreach (var fav in favorites)
                {
                    favoritePropertyIds.Add(fav.PropertyId);
                }
            }

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
                IsFavourite = favoritePropertyIds.Contains(p.Id),
                Reviews = new PropertyReviewsViewModel
                {
                    PropertyId = p.Id,
                    AverageRating = p.Reviews != null && p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                    TotalReviews = p.Reviews?.Count ?? 0
                }
            }).ToList();

            // Map Amenities from List<Amenity> to List<AmenityViewModel>
            var amenities = await _repo.GetAllAmenities();
            var amenityViewModels = amenities.Select(a => new AmenityViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Icon = a.Icon,
                Category = a.Category.ToString()
            }).ToList();

            var cities = await _repo.GetAllCities();
            var regions = await _repo.GetAllRegions();

            var pageViewModel = new PropertyIndexPageViewModel
            {
                Properties = propertyViewModels,
                Filter = filter ?? new PropertyFilterViewModel(),
                PropertyTypes = Enum.GetNames(typeof(PropertyType)).ToList(),
                ListingTypes = Enum.GetNames(typeof(PropertyListingType)).ToList(),
                Amenities = amenityViewModels,
                Cities = cities,
                Regions = regions,
                CurrentPage = currentPage,
                TotalPages = totalPages
            };

            return pageViewModel;
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
                //AgentPhone = property.User.PhoneNumber,
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

        public async Task<bool> DeletePropertyAsync(int id, int userId)
        {
            var property = await _repo.GetByIdAsync(id);

            if (property == null)
            {
                return false;
            }

            // Check if the property belongs to the user
            if (property.UserId != userId)
            {
                throw new UnauthorizedAccessException("You don't have permission to delete this property.");
            }

            // Delete images from file system
            if (property.PropertyImages != null && property.PropertyImages.Any())
            {
                foreach (var image in property.PropertyImages)
                {
                    DeleteImageFile(image.ImageUrl);
                }
            }

            // Delete the property (cascade delete will handle related entities)
            _repo.Delete(property);
            await _repo.SaveAsync();

            return true;
        }

        private void DeleteImageFile(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return;

            string filePath = Path.Combine("wwwroot", imageUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception)
                {
                    //later
                }
            }
        }

        public async Task<CreatePropertyViewModel?> GetPropertyForEditAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new CreatePropertyViewModel
            {
                Id = p.Id,
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
            // 1. Map basic property information
            property.Title = model.Title;
            property.Description = model.Description;
            property.Price = model.Price;
            property.BedRooms = model.BedRooms;
            property.BathRooms = model.BathRooms;
            property.Area = model.Area;

            // 2. Parse and map enums
            property.ListingType = Enum.Parse<PropertyListingType>(model.ListingType);
            property.Type = Enum.Parse<PropertyType>(model.PropertyType);

            // 3. Handle Location
            var location = await _repo.GetLocationAsync(model.City, model.Region, model.Street);

            if (location == null)
            {
                location = new Location
                {
                    City = model.City,
                    Regoin = model.Region,
                    Street = model.Street,
                    Latitude = 0,
                    Longitude = 0,
                    ImageUrl = string.Empty,
                    CreatedAt = DateTime.Now
                };

                _context.Locations.Add(location);
                await _repo.SaveAsync();
            }

            property.LocationId = location.Id;

            // 4. Initialize PropertyImages collection
            property.PropertyImages = new List<PropertyImages>();

            // 5. Handle Main Image
            if (model.MainImage != null)
            {
                string mainImagePath = await SaveImageAsync(model.MainImage, "properties");

                property.PropertyImages.Add(new PropertyImages
                {
                    ImageUrl = mainImagePath,
                    CreatedAt = DateTime.Now
                });
            }

            // 6. Handle Gallery Images
            if (model.GalleryImages != null && model.GalleryImages.Any())
            {
                foreach (var galleryImage in model.GalleryImages)
                {
                    string imagePath = await SaveImageAsync(galleryImage, "properties");

                    property.PropertyImages.Add(new PropertyImages
                    {
                        ImageUrl = imagePath,
                        CreatedAt = DateTime.Now
                    });
                }
            }

            // 7. Handle Amenities
            if (model.Amenities != null && model.Amenities.Any())
            {
                property.amenities = new List<Amenity>();

                foreach (var amenityInput in model.Amenities.Values)
                {
                    var existingAmenity = await _repo.GetAmenityByNameAsync(amenityInput.Name);

                    if (existingAmenity != null)
                    {
                        property.amenities.Add(existingAmenity);
                    }
                    else
                    {
                        var newAmenity = new Amenity
                        {
                            Name = amenityInput.Name,
                            Category = amenityInput.Category,
                            Icon = GetDefaultIcon(amenityInput.Category.ToString()),
                            CreatedAt = DateTime.Now
                        };

                        property.amenities.Add(newAmenity);
                    }
                }
            }
        }

        private async Task<string> SaveImageAsync(IFormFile image, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string uploadsFolder = Path.Combine("wwwroot", "uploads", folder);
            Directory.CreateDirectory(uploadsFolder);
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/uploads/{folder}/{fileName}";
        }

        private string GetDefaultIcon(string category)
        {
            return category == "Interior" ? "bi-house-door" : "bi-building";
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

        // related to offers
        public List<Property> GetPropertyByuser(int userId)
        {
            return _repo.GetByUser(userId);
        }

        public List<Property> GetByLocationAndUser(int locationId, int userId)
        {
            return _repo.GetByLocationAndUser(locationId, userId);
        }


        public async Task<List<LocationCardViewModel>> GetTopLocationsAsync(int count)
        {
            var locations = await _repo.GetTopLocationsWithPropertyCountAsync(count);

            return locations.Select(l => new LocationCardViewModel
            {
                Id = l.Id,
                City = l.City,
                Region = l.Regoin,
                ImageUrl = l.ImageUrl ?? "assets/img/real-estate/default-location.webp",
                PropertyCount = l.Properties?.Count ?? 0
            }).ToList();

        }
    }
}