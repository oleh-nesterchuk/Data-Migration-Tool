using DataMigrationApi.Core.Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IMongoDbUserRepository : IBaseRepository<User, string>
    {
        IEnumerable<User> GetAllWithoutProjection();
        IEnumerable<Email> GetEmails();
        IEnumerable<Email> GetAllUserEmails(string id);
        Email GetEmailById(int id);
        Email InsertEmail(Email email, string userId);
        Email UpdateEmail(Email email, string userId);
        void DeleteEmail(int id);
    }
}
