using IjarifySystemBLL.ViewModels.LocationsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IHomeService
    {
        IEnumerable<LocationViewModel> GetTopLocations();
    }
}
