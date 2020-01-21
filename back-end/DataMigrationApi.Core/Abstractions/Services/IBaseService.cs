using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IBaseService<TEntity, T, TParams> where TEntity : class
    {
        IEnumerable<TEntity> Get(TParams parameters);
        TEntity Get(T id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(T id);
    }
}
