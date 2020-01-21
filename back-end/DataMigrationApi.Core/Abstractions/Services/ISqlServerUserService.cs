using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface ISqlServerUserService : IBaseService<User, string, UserParameters>
    {
    }
}
