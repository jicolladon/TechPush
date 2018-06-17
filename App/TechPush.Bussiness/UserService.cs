using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core;
using TechPush.Core.Domain;
using TechPush.Core.Dto;
using TechPush.Core.RemoteServices;
using TechPush.Core.Repositories;
using TechPush.Core.Services;

namespace TechPush.Bussiness
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IAppService appService;

        public UserService(IUserRepository userRepository, IAppService appService)
        {
            this.userRepository = userRepository;
            this.appService = appService;
        }
        public UserDto GetLastUser()
        {
            var user = userRepository.GetUser();
            if (user != null)
            {
                return new UserDto()
                {
                    Username = user.Username,
                    Password = user.Password,
                    RememberMe = user.RememberMe,
                    IsAdmin = user.IsAdmin,
                };
            }
            else return null;
        }

        public async Task<IEnumerable<MessageDto>> GetNotifications()
        {
            var user = GetLastUser();
            return await appService.GetUserMessages(user);
        }

        public async Task<bool> LoginUser(UserDto user)
        {
            UserDto result = await appService.LoginUser(user);
            if (result != null)
            {
                result.RememberMe = user.RememberMe;
                result.Password = user.Password;
                SetLoginUser(result);
            }
            return result != null;
        }


        public void SetLoginUser(UserDto user)
        {
            var userToSave = new UserEntity()
            {
                Username = user.Username,
                Password = user.Password,
                RememberMe = user.RememberMe,
                IsAdmin = user.IsAdmin,
            };
            userRepository.SetUser(userToSave);
        }
        public async Task<bool> RegisterUser(UserDto userDto)
        {
            var result = await appService.RegisterUser(userDto);
            if (result)
            {
                await LoginUser(userDto);
            }
            return result;
        }

        public async Task<AdminInfoDto> GetAdminInfo()
        {
            var user = GetLastUser();
            return await appService.GetAdminInfo(user);
        }

        public void RemoveLoguedUser()
        {
            userRepository.RemoveAll();
        }
    }
}
