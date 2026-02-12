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
        public User? GetUserById(int id);
        public bool UpdateUserProfile(EditProfileViewModel model, int userId, string? newImagePath = null);
        public bool DeleteProfileImage(int userId);
        public User? GetUserByPhoneNumber(string phoneNumber);
    }
}
