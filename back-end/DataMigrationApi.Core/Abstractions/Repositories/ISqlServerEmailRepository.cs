using DataMigrationApi.Core.Entities.SQL_Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Repositories
{
    public interface ISqlServerEmailRepository : IBaseRepository<Email, int>
    {
        IEnumerable<Email> GetAllUserEmails(string id);
    }
}
