using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Data.DbModels;
using TechPushCoreApi.Services.Dtos;

namespace TechPushCoreApi.Services.Implementations
{
    public class MappingConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<UserDto, User>()
                    .ForMember(o => o.UserEmail, v => v.MapFrom(c => c.Username))
                    .ForMember(o => o.UserPassword, v => v.MapFrom(c => c.Password));
                config.CreateMap<User, UserDto>()
                    .ForMember(o => o.Username, v => v.MapFrom(c => c.UserEmail));
                config.CreateMap<PushRegistrationDto,PushRegistration>()
                    .ForMember(o => o.DeviceId, v => v.MapFrom(c => c.DeviceId))
                    .ForMember(o => o.Token, v => v.MapFrom(c => c.DeviceToken))
                    .ForMember(o => o.PlatformType, v => v.MapFrom(c => c.DeviceType.ToString()))
                    .ForMember(o => o.Tags, v => v.MapFrom(c => string.Join("|", c.Tags)));
                config.CreateMap<PushMessage, MessageDto>();
            });


        }
    }
}