using DataMigrationApi.Core.Entities.NoSQL_Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IMongoDbEmailService
    {
        IEnumerable<Email> GetAllUserEmails(string id);
        IEnumerable<Email> Get();
        Email Get(int id);
        Email Insert(Email entity, string userId);
        Email Update(Email entity, string userId);
        void Delete(int id);
    }
}
