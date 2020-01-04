using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities.SQL_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataMigrationApi.DAL.Repositories
{
    public class SqlServerEmailRepository : ISqlServerEmailRepository
    {
        private readonly UserContext _userContext;

        public SqlServerEmailRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public IEnumerable<Email> GetAll() =>
            _userContext.Emails;

        public IEnumerable<Email> GetAllUserEmails(string id) =>
            _userContext.Users.Find(id).Emails;

        public Email GetById(int id) =>
            _userContext.Emails.Find(id);

        public Email Insert(Email entity)
        {
            _userContext.Emails.Add(entity);
            return entity;
        }

        public Email Update(Email entity)
        {
            var email = _userContext.Emails.Find(entity.ID);
            if (email == null)
            {
                return null;
            }

            _userContext.Entry(email).CurrentValues.SetValues(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var email = GetById(id);
            if (email == null)
            {
                return;
            }

            _userContext.Emails.Remove(email);
        }
    }
}
