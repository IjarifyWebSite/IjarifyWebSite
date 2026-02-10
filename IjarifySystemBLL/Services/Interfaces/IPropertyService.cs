using IjarifySystemBLL.ViewModels.PropertyViewModels;
using IjarifySystemDAL.Entities;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyIndexPageViewModel> GetPagination(int pageSize, int page, PropertyFilterViewModel filter);
        Task<PropertyDetailsViewModel?> GetPropertyDetails(int id, int? currentUserId = null);
        Task CreatePropertyAsync(CreatePropertyViewModel model, int userId);
        Task UpdatePropertyAsync(int id, CreatePropertyViewModel model, int userId);
        Task DeletePropertyAsync(int id);
        Task<CreatePropertyViewModel?> GetPropertyForEditAsync(int id);
        List<Property>GetPropertyByuser(int userId);
        public List<Property> GetByLocationAndUser(int locationId, int userId);
    }
}