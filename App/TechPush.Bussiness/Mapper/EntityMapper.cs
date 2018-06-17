using System;
using System.Collections.Generic;
using System.Text;
using TechPush.Core.Mapper;
using AutoMapper;

namespace TechPush.Bussiness.Mapper
{
    public class EntityMapper : IEntityMapper
    {
        public EntityMapper()
        {
            MappingConfiguration.Configure();
        }

        public TOutput Map<TInput, TOutput>(TInput input)
        {
            return AutoMapper.Mapper.Map<TInput, TOutput>(input);
        }

        public TOutput Map<TInput, TOutput>(TInput input, TOutput output)
        {
            return AutoMapper.Mapper.Map<TInput, TOutput>(input, output);
        }
    }
}