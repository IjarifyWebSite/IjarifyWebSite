using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.FavouriteViewModels;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;

namespace IjarifySystemBLL.Services.Classes
{
    public class FavouriteService : IFavouriteService
    {


        private readonly IFavouriteRepository _favouriteRepository;

            public FavouriteService(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }





        public UserFavouritesViewModel GetUserFavourites(int userId)
        {

            var favourites = _favouriteRepository.GetAllByUserId(userId);

            var viewModel = new UserFavouritesViewModel
            {
                UserId = userId,
                UserName = favourites.FirstOrDefault()?.User?.Name ?? "User",
                Favourites = favourites.Select(f => new FavouriteItemViewModel
                {

                    FavouriteId = f.Id,
                    PropertyId = f.PropertyId,
                    PropertyTitle = f.Property.Title,
                    PropertyDescription = f.Property.Description,
                    Price = f.Property.Price,
                    BedRooms = f.Property.BedRooms,
                    BathRooms = f.Property.BathRooms,
                    Area = f.Property.Area,
                    ListingType = f.Property.ListingType.ToString(),
                    Type = f.Property.Type.ToString(),
                    City = f.Property.Location?.City ?? "",
                    Region = f.Property.Location?.Regoin ?? "",
                    Street = f.Property.Location?.Street ?? "",
                    ImageUrl = f.Property.PropertyImages?.FirstOrDefault()?.ImageUrl,
                    CreatedAt = f.CreatedAt
                }).ToList(),
                TotalCount = favourites.Count()
            };

            return viewModel;

        }






        public bool AddToFavourites(int userId, int propertyId)
        {


            try
            {
                // check if already exists
                var existing = _favouriteRepository.GetByUserIdAndPropertyId(userId, propertyId);
                if (existing != null)
                {
                    return false; // already exists
                }

                var favourite = new Favourite
                {
                    UserId = userId,
                    PropertyId = propertyId,
                    CreatedAt = DateTime.Now
                };

                _favouriteRepository.Add(favourite);
                var result = _favouriteRepository.SaveChanges();

                return result > 0;
            }
            catch
            {
                return false;
            }
        }

     
        public bool IsPropertyFavourite(int userId, int propertyId)
        {
            var favourite = _favouriteRepository.GetByUserIdAndPropertyId(userId, propertyId);
            return favourite != null;
        }

        public bool RemoveFromFavourites(int userId, int propertyId)
        {
            try
            {
                var favourite = _favouriteRepository.GetByUserIdAndPropertyId(userId, propertyId);
                if (favourite == null)
                {
                    return false; // does not exist
                }

                _favouriteRepository.Delete(favourite);
                var result = _favouriteRepository.SaveChanges();

                return result > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool ToggleFavourite(int userId, int propertyId)
        {
            var favourite = _favouriteRepository.GetByUserIdAndPropertyId(userId, propertyId);

            if (favourite != null)
            {
                // exists → delete 
                return RemoveFromFavourites(userId, propertyId);
            }
            else
            {
                // does not exist → add
                return AddToFavourites(userId, propertyId);
            }
        }
    }
}
