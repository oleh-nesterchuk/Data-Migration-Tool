using DataMigrationApi.Core.Entities.SQL_Entities;
using System.Collections.Generic;

namespace DataMigrationApi.Core.Abstractions.Services
{
    public interface ISqlServerEmailService : IBaseService<Email, int>
    {
        List<Email> GetAllUserEmails(string id);
    }
}
