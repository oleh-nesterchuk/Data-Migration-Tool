using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities.SQL_Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataMigrationApi.DAL.Repositories
{
    public class SqlServerRepository : ISqlServerRepository
    {
        private readonly UserContext _userContext;

        public SqlServerRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public IQueryable<User> GetAll() =>
            _userContext.Users;

        public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate) =>
            _userContext.Users.Where(predicate);

        public User GetById(string id) =>
            _userContext.Users.Find(id);

        public User Insert(User entity)
        {
            entity.ID = new Guid().ToString();
            _userContext.Users.Add(entity);
            return entity;
        }

        public User Update(User entity)
        {
            var user = _userContext.Users.Find(entity.ID);
            if (user == null)
            {
                return null;
            }

            _userContext.Entry(user).CurrentValues.SetValues(entity);
            return entity;
        }

        public void Delete(string id)
        {
            var user = _userContext.Users.Find(id);
            if (user == null)
            {
                return;
            }

            _userContext.Users.Remove(user);
        }
    }
}
