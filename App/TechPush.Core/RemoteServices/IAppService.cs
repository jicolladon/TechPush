using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Dto;

namespace TechPush.Core.RemoteServices
{
    public interface IAppService
    {
        Task<bool> RegisterPush(PushRegistrationDto data);
        Task<bool> RegisterUser(UserDto userDto);
        Task<IEnumerable<MessageDto>> GetNotifications(UserDto user);
        Task<UserDto> LoginUser(UserDto user);
        Task<IEnumerable<MessageDto>> GetUserMessages(BaseDto user);
        Task<AdminInfoDto> GetAdminInfo(UserDto user);
        Task<bool> SendNotification(PushMessageDto pushMessageDto);
    }
}
