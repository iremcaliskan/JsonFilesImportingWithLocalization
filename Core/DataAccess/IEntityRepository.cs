using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T: class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter); // Get an object with a filter
        List<T> GetAll(Expression<Func<T, bool>> filter = null); // List objects with or without a filter
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}