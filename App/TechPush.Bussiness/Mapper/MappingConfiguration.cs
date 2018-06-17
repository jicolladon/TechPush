using System;
using System.Collections.Generic;
using System.Text;
using TechPush.Core.Dto;
using TechPush.Core.Models;

namespace TechPush.Bussiness.Mapper
{
    public class MappingConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<MessageDto, NotificationModel>()
                 .ForMember(c=>c.Date, o=>o.MapFrom(v=>v.Date))
                 .ForMember(c => c.Guid, o => o.MapFrom(v => v.Id))
                 .ForMember(c => c.Date, o => o.MapFrom(v => v.Date))
                 .ForMember(c => c.Image, o => o.MapFrom(v => v.Image))
                 .ForMember(c => c.Message, o => o.MapFrom(v => v.Description))
                 .ForMember(c => c.Tag, o => o.MapFrom(v => v.Tag));

            });


        }
    }
}
