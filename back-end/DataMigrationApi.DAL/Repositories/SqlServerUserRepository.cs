using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.DAL.Repositories
{
    public class SqlServerUserRepository : ISqlServerUserRepository
    {
        private readonly UserContext _userContext;

        public SqlServerUserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public IEnumerable<User> GetAll(UserParameters parameters) =>
            _userContext.Users
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        public User GetById(string id) =>
            _userContext.Users.Find(id);

        public User Insert(User entity)
        {
            _userContext.Users.Add(entity);
            return entity;
        }

        public User Update(User entity)
        {
            var user = GetById(entity.ID);
            _userContext.Entry(user).CurrentValues.SetValues(entity);
            return GetById(entity.ID);
        }

        public void Delete(string id)
        {
            var user = GetById(id);
            _userContext.Users.Remove(user);
        }
    }
}
