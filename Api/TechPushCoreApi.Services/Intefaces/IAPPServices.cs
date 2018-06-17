using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Services.Dtos;

namespace TechPushCoreApi.Services.Intefaces
{
    public interface IAPPServices
    {
        bool RegisterUser(UserDto user);
        UserDto ValidateUser(BaseDto user);

        bool RegisterPush(PushRegistrationDto push);
        IEnumerable<MessageDto> GetMessages(BaseDto user);
        AdminInfoDto GetAdminInfo(BaseDto baseDto);
        bool SendNotification(PushMessageDto message, string rootPath);
    }
}
