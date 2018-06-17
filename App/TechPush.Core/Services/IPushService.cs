using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Dto;

namespace TechPush.Core.Services
{
    public interface IPushService
    {
        Task<bool> RegisterPush(UserDto user);
        Task<bool> SendPushNotifications(PushMessageDto pushMessageDto);
    }
}
