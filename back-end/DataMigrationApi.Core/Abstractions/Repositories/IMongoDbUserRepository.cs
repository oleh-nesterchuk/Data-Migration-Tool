using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface IMongoDbUserRepository : IBaseRepository<User, string, UserParameters>
    {
        IEnumerable<User> GetAllWithoutProjection(UserParameters parameters);
        IEnumerable<Email> GetEmails(EmailParameters parameters);
        IEnumerable<Email> GetAllUserEmails(string id);
        IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters);
        Email GetEmailById(int id);
        Email InsertEmail(Email email, string userId);
        Email UpdateEmail(Email email, string userId);
        void DeleteEmail(int id);
    }
}
