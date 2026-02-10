using IjarifySystemBLL.Services.Interfaces;
using IjarifySystemBLL.ViewModels.AccountViewModels;
using IjarifySystemDAL.Entities;
using IjarifySystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemBLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetUserById(int id) => _userRepository.GetById(id);

        public bool UpdateUserProfile(EditProfileViewModel updatedModel, int userId, string? newImagePath = null)
        {
            try
            {
                var user = _userRepository.GetById(userId);
                
                if (user == null)
                {
                    return false;
                }

                user.Name = updatedModel.FullName;
                user.Email = updatedModel.Email;
                user.Phone = updatedModel.PhoneNumber;
                
                // Update image
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    user.ImageUrl = newImagePath;
                }

                user.UpdatedAt = DateTime.Now;

                _userRepository.Update(user);
                return _userRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProfileImage(int userId)
        {
            try
            {
                var user = _userRepository.GetById(userId);
                
                if (user == null)
                {
                    return false;
                }

                user.ImageUrl = null;
                user.UpdatedAt = DateTime.Now;

                _userRepository.Update(user);
                return _userRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var user = _userRepository.GetById(userId);
                
                if (user == null)
                {
                    return false;
                }

                _userRepository.Delete(user);
                return _userRepository.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
