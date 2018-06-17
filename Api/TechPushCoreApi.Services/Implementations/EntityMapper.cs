using System;
using System.Collections.Generic;
using System.Text;
using TechPushCoreApi.Services.Intefaces;

namespace TechPushCoreApi.Services.Implementations
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