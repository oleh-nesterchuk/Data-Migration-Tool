using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity, T, TParams> where TEntity : class, IEntity<T>
    {
        TEntity GetById(T id);
        IEnumerable<TEntity> GetAll(TParams parameters);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(T id);
    }
}
