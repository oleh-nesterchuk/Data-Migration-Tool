using DataMigrationApi.Core.Entities.SQL_Entities;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface ISqlServerUserRepository : IBaseRepository<User, string>
    {
    }
}
