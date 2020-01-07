using DataMigrationApi.Core.Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface ISqlServerEmailService : IBaseService<Email, int>
    {
        IEnumerable<Email> GetAllUserEmails(string id);
    }
}
