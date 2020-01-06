using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities.SQL_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Email> GetAllUserEmails(string id)
        {
            var user = _userContext.Users
                .Include(u => u.Emails)
                .ToList()
                .Find(u => u.ID == id);
            if (user == null)
            {
                return null;
            }

            return user.Emails;
        }

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
