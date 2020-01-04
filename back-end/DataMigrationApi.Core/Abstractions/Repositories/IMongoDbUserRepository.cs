using DataMigrationApi.Core.Entities.NoSQL_Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IMongoDbUserRepository : IBaseRepository<User, string>
    {
        IEnumerable<Email> GetEmails();
        IEnumerable<Email> GetAllUserEmails(string id);
        Email GetEmailById(int id);
        Email InsertEmail(Email email);
        Email UpdateEmail(Email email);
        void DeleteEmail(int id);
    }
}
