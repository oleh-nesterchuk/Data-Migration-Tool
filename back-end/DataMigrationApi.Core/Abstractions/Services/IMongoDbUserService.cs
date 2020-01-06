using DataMigrationApi.Core.Entities;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IMongoDbUserService : IBaseService<User, string>
    {
    }
}
