using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface ISqlServerUserRepository : IBaseRepository<User, string, UserParameters>
    {
    }
}
