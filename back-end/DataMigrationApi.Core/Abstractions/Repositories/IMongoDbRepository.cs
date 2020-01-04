using DataMigrationApi.Core.Entities.NoSQL_Entities;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IMongoDbRepository : IBaseRepository<User, string>
    {
    }
}
