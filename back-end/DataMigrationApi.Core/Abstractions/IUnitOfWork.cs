using DataMigrationApi.Core.Abstractions.Repositories;
using System;

namespace DataMigrationApi.Core.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ISqlServerUserRepository SqlServerUserRepository { get; }
        ISqlServerEmailRepository SqlServerEmailRepository { get; }
        IMongoDbUserRepository MongoDbRepository { get; }
        void Save();
    }
}
