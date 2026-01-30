using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IjarifySystemBLL.ViewModels;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IPropertyService
    {
        public  Task<(List<PropertyIndexViewModel>?, int, int)> GetPagination(int pageSize, int page);
    }
}
