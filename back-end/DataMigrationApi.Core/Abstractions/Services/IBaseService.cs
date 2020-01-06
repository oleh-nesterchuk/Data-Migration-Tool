using System;
using System.Collections.Generic;
using System.Text;

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
