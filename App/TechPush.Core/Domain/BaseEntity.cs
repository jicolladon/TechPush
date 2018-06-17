using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Domain
{
    public class BaseEntity<T>
    {
        [PrimaryKey]
        public T Id { get; set; }
    }
}
