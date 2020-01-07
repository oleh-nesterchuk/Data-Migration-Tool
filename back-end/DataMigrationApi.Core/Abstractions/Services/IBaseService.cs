using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IBaseService<TEntity, T> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(T id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(T id);
    }
}
