using DataMigrationApi.Core.Entities;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface ISqlServerUserRepository : IBaseRepository<User, string>
    {
    }
}
