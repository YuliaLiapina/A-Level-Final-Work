using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> Get();
        void DeleteById(TKey id, bool isSaveChangeRequired);
        void Add(TEntity entity, bool isSaveChangeRequired);
        void Update(TEntity entity, bool isSaveChangeRequired);
    }
}
