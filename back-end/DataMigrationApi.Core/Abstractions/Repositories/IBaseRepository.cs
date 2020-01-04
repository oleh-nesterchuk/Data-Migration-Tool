using DataMigrationApi.Core.Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        TEntity GetById(T id);
        IEnumerable<TEntity> GetAll();
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(T id);
    }
}
