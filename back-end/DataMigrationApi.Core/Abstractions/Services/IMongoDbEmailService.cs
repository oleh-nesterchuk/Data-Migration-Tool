using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface IMongoDbEmailService
    {
        int GetSize(string id);
        IEnumerable<Email> GetAllUserEmails(string id);
        IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters);
        IEnumerable<Email> Get(EmailParameters parameters);
        Email Get(int id);
        Email Insert(Email entity, string userId);
        Email Update(Email entity, string userId);
        void Delete(int id);
    }
}
