using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemBLL.ViewModels.LocationsViewModel;
namespace IjarifySystemBLL.ViewModels
{
    public class HomeViewModel
    {
        public List<LocationViewModel> TopLocations { get; set; } = null!;
    }
}
