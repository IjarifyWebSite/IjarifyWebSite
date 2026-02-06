using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemBLL.ViewModels.PropertyViewModels;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<(List<PropertyIndexViewModel>?, int, int)> GetPagination(int pageSize, int page);
        Task<PropertyIndexPageViewModel> GetPagination(int pageSize, int page, PropertyFilterViewModel filter);
    }
}
