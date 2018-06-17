using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechPush.Bussiness.Helper;
using TechPush.Core.Dto;
using TechPush.Core.RemoteServices;

namespace TechPush.Bussiness.RemoteServices
{
    public class AppService : BaseClientService, IAppService
    {
        private readonly IRestClientService restClientService;

        public AppService(IRestClientService restClientService)
        {
            this.restClientService = restClientService;
        }

        public async Task<IEnumerable<MessageDto>> GetNotifications(UserDto user)
        {
            var data = new List<MessageDto>();
            for(int i = 0; i < 20; i++)
            {
                data.Add(new MessageDto
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Image = null,
                    Description = Common.LoremIpsum(1, 5, 1, 1, 1),
                    Tag = "Random"

                });
            }
            return data;
        }

        public async Task<UserDto> LoginUser(UserDto user)
        {
            var result = await restClientService.PostAsync<BaseDto, UserDto>("api/User/Login ", user);
            return result.Entity;
        }

        public async Task<bool> RegisterPush(PushRegistrationDto data)
        {
            var result = await restClientService.PostAsync<PushRegistrationDto, bool>("api/User/RegisterPush", data);
            return result.Entity;
        }

        public async Task<bool> RegisterUser(UserDto userDto)
        {
            var result = await restClientService.PostAsync<UserDto, bool>("api/User/RegisterUser", userDto);
            return result.Entity;
        }

        public async Task<IEnumerable<MessageDto>> GetUserMessages(BaseDto user)
        {
            var result = await restClientService.PostAsync<BaseDto, IEnumerable<MessageDto>>("api/User/GetMessages", user);
            return result.Entity;
        }

        public async Task<AdminInfoDto> GetAdminInfo(UserDto user)
        {
            var result = await restClientService.PostAsync<BaseDto, AdminInfoDto>("api/User/GetAdminInfo", user);
            return result.Entity;
        }

        public async Task<bool> SendNotification(PushMessageDto pushMessageDto)
        {
            var result = await restClientService.PostAsync<PushMessageDto, bool>("api/User/SendNotification", pushMessageDto);
            return result.Entity;
        }
    }
}
