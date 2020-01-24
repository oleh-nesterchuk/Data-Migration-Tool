using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.DAL.Repositories
{
    public class SqlServerEmailRepository : ISqlServerEmailRepository
    {
        private readonly UserContext _userContext;

        public SqlServerEmailRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public int GetSize() =>
            _userContext.Emails.Count();

        public int GetSize(string id) =>
            _userContext.Emails.Where(e => e.UserID == id).Count();

        public IEnumerable<Email> GetAll(EmailParameters parameters) =>
            _userContext.Emails
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        public IEnumerable<Email> GetAllUserEmails(string id)
        {
            var user = _userContext.Users
                .Include(u => u.Emails)
                .ToList()
                .Find(u => u.ID == id);
            return user.Emails;
        }

        public IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters)
        {
            var user = _userContext.Users
                .Include(u => u.Emails)
                .ToList()
                .Find(u => u.ID == id);
            return user.Emails
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);
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

            _userContext.Entry(email).CurrentValues.SetValues(entity);
            return email;
        }

        public void Delete(int id)
        {
            var email = GetById(id);
            _userContext.Emails.Remove(email);
        }
    }
}
