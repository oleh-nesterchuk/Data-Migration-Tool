using DataMigrationApi.Core.Entities;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface ISqlServerUserService : IBaseService<User, string>
    {
    }
}
