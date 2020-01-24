using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface ISqlServerEmailRepository : IBaseRepository<Email, int, EmailParameters>
    {
        int GetSize(string id);
        IEnumerable<Email> GetAllUserEmails(string id);
        IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters);
    }
}
