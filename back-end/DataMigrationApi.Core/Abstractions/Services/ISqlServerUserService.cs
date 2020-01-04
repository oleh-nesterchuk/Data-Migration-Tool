using DataMigrationApi.Core.Entities.SQL_Entities;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface ISqlServerUserService : IBaseService<User, string>
    {
    }
}
