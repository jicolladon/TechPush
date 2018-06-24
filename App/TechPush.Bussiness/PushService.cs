using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Dto;
using TechPush.Core.RemoteServices;
using TechPush.Core.Services;

namespace TechPush.Bussiness
{
    public class PushService : IPushService
    {
        private readonly IDeviceService deviceService;
        private readonly IAppService appService;
        private IUserService _userService;

        public PushService(IDeviceService deviceService, IAppService appService, IUserService userService)
        {
            _userService = userService;
            this.deviceService = deviceService;
            this.appService = appService;
        }

        //Registro en el servidor de notificaciones push
        public async Task<bool> RegisterPush(UserDto user)
        {
            var tries = 5;
            var result = false;
            for (int regTry = 0; regTry < tries && !result; regTry++)
            {
                var token = deviceService.GetToken();
                //var token = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(token))
                {
                    var data = new PushRegistrationDto()
                    {
                        DeviceId = CrossDeviceInfo.Current.Id,
                        DeviceToken = token,
                        DeviceType = deviceService.GetDeviceType(),
                        Tags = GetTags(),
                        Username = user.Username,
                        Password = user.Password,
                    };
                    result = await appService.RegisterPush(data);
                }
                await Task.Delay(2000);
            }
            return result;
        }

        public async Task<bool> SendPushNotifications(PushMessageDto pushMessageDto)
        {
            var result = false;
            var user = _userService.GetLastUser();
            if (user != null && user.IsAdmin)
            {
                pushMessageDto.Username = user.Username;
                pushMessageDto.Password = user.Password;
                result = await appService.SendNotification(pushMessageDto);
            }

            return result;
        }

        private List<string> GetTags()
        {
            var list = new List<string>()
            {
                    CrossDeviceInfo.Current.Idiom.ToString(),
                    CrossDeviceInfo.Current.Model,
                    CrossDeviceInfo.Current.Version,
                    CrossDeviceInfo.Current.Platform.ToString(),
            };
            return list;
        }
    }
}
