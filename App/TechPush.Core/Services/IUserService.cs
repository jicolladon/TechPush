using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Dto;

namespace TechPush.Core.Services
{
    public interface IUserService
    {
        UserDto GetLastUser();
        Task<bool> LoginUser(UserDto user);
        Task<bool> RegisterUser(UserDto userDto);
        Task<IEnumerable<MessageDto>> GetNotifications();
        Task<AdminInfoDto> GetAdminInfo();
        void RemoveLoguedUser();
    }
}
