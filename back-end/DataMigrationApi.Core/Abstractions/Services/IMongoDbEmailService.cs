using DataMigrationApi.Core.Entities.NoSQL_Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IMongoDbEmailService : IBaseService<Email, int>
    {
        List<Email> GetAllUserEmails(string id);
    }
}
