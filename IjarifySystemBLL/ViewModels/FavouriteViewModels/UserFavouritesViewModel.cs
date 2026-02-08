using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.ViewModels.FavouriteViewModels
{
    public class UserFavouritesViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null;
        public List<FavouriteItemViewModel> Favourites { get; set; } = new List<FavouriteItemViewModel>();
        public int TotalCount { get; set; }
    }
}
