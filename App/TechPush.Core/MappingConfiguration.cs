using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TechPush.Core.Domain;
using TechPush.Core.Dto;

namespace TechPush.Core
{
    public static class MappingConfiguration
    {
        public static void Configure()
        {
            ConfigureMapper();
        }

        private static void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserEntity, UserDto>();
             

            });

            Mapper.Configuration.CreateMapper();
        }

  
    }
}
