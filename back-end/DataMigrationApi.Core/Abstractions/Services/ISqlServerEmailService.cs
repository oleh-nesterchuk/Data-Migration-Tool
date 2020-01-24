using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface ISqlServerEmailService : IBaseService<Email, int, EmailParameters>
    {
        int GetSize(string id);
        IEnumerable<Email> GetAllUserEmails(string id);
        IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters);
    }
}
