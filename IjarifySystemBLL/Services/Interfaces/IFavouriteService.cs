using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemBLL.ViewModels.FavouriteViewModels;

namespace IjarifySystemBLL.Services.Interfaces
{
      public interface IFavouriteService
    {

         UserFavouritesViewModel GetUserFavourites(int userId);

       
        bool AddToFavourites(int userId, int propertyId);

        
        bool RemoveFromFavourites(int userId, int propertyId);

    
        bool IsPropertyFavourite(int userId, int propertyId);

        // if exists remove else add
        bool ToggleFavourite(int userId, int propertyId);

    }
}
