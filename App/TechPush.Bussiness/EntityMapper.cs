using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TechPush.Core;

namespace TechPush.Bussiness
{
    public class EntityMapper : IEntityMapper
    {
        public EntityMapper()
        {
            MappingConfiguration.Configure();
        }

        public TOutput Map<TInput, TOutput>(TInput input)
        {
            return Mapper.Map<TInput, TOutput>(input);
        }

        public TOutput Map<TInput, TOutput>(TInput input, TOutput output)
        {
            return Mapper.Map(input, output);
        }
    }
}
