using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.DAL.Repositories;
using System;

namespace DataMigrationApi.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISqlServerEmailRepository _sqlServerEmailRepository;
        private ISqlServerUserRepository _sqlServerUserRepository;
        private IMongoDbUserRepository _mongoDbRepository;
        private UserContext _userContext;
        private readonly IMongoDBSettings _mongoSettings;
        private bool _disposed = false;

        public UnitOfWork(UserContext userContext, IMongoDBSettings mongoSettings)
        {
            _userContext = userContext;
            _mongoSettings = mongoSettings;
        }

        public ISqlServerEmailRepository SqlServerEmailRepository => _sqlServerEmailRepository ??=
            new SqlServerEmailRepository(_userContext);

        public ISqlServerUserRepository SqlServerUserRepository => _sqlServerUserRepository ??=
            new SqlServerUserRepository(_userContext);

        public IMongoDbUserRepository MongoDbRepository => _mongoDbRepository ??=
            new MongoDbUserRepository(_mongoSettings);

        public void Save()
        {
            _userContext.SaveChanges();
        }

        #region Dispose Logic

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userContext.Dispose();
                }

                _userContext = null;
                _disposed = true;
            }
        }

        #endregion
    }
}
