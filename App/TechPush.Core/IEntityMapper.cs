using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core
{
    public interface IEntityMapper
    {
        TOutput Map<TInput, TOutput>(TInput input);
        TOutput Map<TInput, TOutput>(TInput input, TOutput output);
    }
}
