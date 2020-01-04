using DataMigrationApi.Core.Abstractions.Repositories;
using System;

namespace DataMigrationApi.Core.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ISqlServerRepository SqlServerRepository { get; }
        IMongoDbRepository MongoDbRepository { get; }
        void Save();
    }
}
