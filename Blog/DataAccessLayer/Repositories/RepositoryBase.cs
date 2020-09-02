using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly ApplicationDbContext _ctx;
        private DbSet<TEntity> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _ctx = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Get(TKey id)
        {
            return _dbSet.FirstOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public void DeleteById(TKey id, bool isSaveChangeRequired = true)
        {
            var entityToDelete = _dbSet.FirstOrDefault(x => x.Id.Equals(id));
            if (entityToDelete == null) return;
            _dbSet.Remove(entityToDelete);
            if (isSaveChangeRequired) _ctx.SaveChanges();
        }

        public void Add(TEntity entity, bool isSaveChangeRequired = true)
        {
            _dbSet.Add(entity);
            if (isSaveChangeRequired) _ctx.SaveChanges();
        }

        public virtual void Update(TEntity entity, bool isSaveChangeRequired = true)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            if (isSaveChangeRequired) _ctx.SaveChanges();
        }
    }
}
