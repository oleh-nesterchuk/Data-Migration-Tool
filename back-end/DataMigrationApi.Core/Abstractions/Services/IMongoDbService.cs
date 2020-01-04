using DataMigrationApi.Core.Entities.NoSQL_Entities;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IMongoDbService : IBaseService<User, string>
    {
    }
}
