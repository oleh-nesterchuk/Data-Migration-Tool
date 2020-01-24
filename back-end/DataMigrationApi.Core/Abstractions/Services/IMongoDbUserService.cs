using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IMongoDbUserService : IBaseService<User, string, UserParameters>
    {
    }
}
