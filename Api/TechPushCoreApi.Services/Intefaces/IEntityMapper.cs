using System;
using System.Collections.Generic;
using System.Text;

namespace TechPushCoreApi.Services.Intefaces
{
    public interface IEntityMapper
    {
        TOutput Map<TInput, TOutput>(TInput input);
        TOutput Map<TInput, TOutput>(TInput input, TOutput output);
    }
}
