using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.DAL.Repositories;
using System;

namespace DataMigrationApi.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISqlServerUserRepository _sqlServerRepository;
        private IMongoDbUserRepository _mongoDbRepository;
        private UserContext _userContext;
        private readonly string _mongoConnectionString;
        private bool _disposed = false;

        public UnitOfWork(UserContext userContext, string mongoConnectionString)
        {
            _userContext = userContext;
            _mongoConnectionString = mongoConnectionString;
        }

        public ISqlServerUserRepository SqlServerUserRepository => _sqlServerRepository ??=
            new SqlServerUserRepository(_userContext);

        public IMongoDbUserRepository MongoDbRepository => _mongoDbRepository ??=
            new MongoDbUserRepository(_mongoConnectionString);

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
