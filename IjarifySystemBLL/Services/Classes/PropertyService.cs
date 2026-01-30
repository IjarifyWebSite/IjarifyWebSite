using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class PropertyService(IPropertyRepository _repo) : IPropertyService
    {
        public async Task<(List<PropertyIndexViewModel>?, int, int)> GetPagination(int pageSize, int page)
        {
            var properties = await _repo.GetForPagination(pageSize, (page - 1) * pageSize);
            int pages = (int)Math.Ceiling(await _repo.PropertiesCount() / (double)pageSize);
            page = Math.Max(1, Math.Min(page, pages));

            var vmList = properties.Select(p => new PropertyIndexViewModel
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
                AgentPhone = p.User.Phone,
                AgentAvatar = p.User.ImageUrl ?? "assets/img/real-estate/default-agent.webp"
            }).ToList();

            return (vmList, pages, page);
        }
    }
}
