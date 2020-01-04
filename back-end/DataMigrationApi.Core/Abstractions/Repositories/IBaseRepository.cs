using DataMigrationApi.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        TEntity GetById(T id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(T id);
    }
}
