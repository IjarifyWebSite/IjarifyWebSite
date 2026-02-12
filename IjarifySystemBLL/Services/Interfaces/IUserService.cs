using IjarifySystemBLL.ViewModels.AccountViewModels;
using IjarifySystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Interfaces
{
    public interface IUserService
    {
        User? GetUserById(int id);
        bool UpdateUserProfile(EditProfileViewModel model, int userId, string? newImagePath = null);
        bool DeleteProfileImage(int userId);
    }
}
