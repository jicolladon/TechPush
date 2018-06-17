using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Data
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        int InsertOrUpdate(T entity);
        int Remove(T entity);
        T Find(object pk);
        T Get(object pk);
        T Get(Func<T, bool> predicate);
        List<T> GetAll();
    }
}
