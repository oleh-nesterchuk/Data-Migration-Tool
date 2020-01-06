using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities.SQL_Entities;
using System;
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

        public IEnumerable<User> GetAll() =>
            _userContext.Users;


        public User GetById(string id) =>
            _userContext.Users.Find(id);

        public User Insert(User entity)
        {
            entity.ID = Guid.NewGuid().ToString();
            _userContext.Users.Add(entity);
            return entity;
        }

        public User Update(User entity)
        {
            var user = GetById(entity.ID);
            if (user == null)
            {
                return null;
            }

            _userContext.Entry(user).CurrentValues.SetValues(entity);
            return entity;
        }

        public void Delete(string id)
        {
            var user = GetById(id);
            if (user == null)
            {
                return;
            }

            _userContext.Users.Remove(user);
        }
    }
}
