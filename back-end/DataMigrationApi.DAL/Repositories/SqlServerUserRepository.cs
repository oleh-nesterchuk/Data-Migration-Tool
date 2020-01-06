using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities;
using System;
using System.Collections.Generic;

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
            if (!Guid.TryParse(entity.ID, out _) || GetById(entity.ID) != null)
            {
                entity.ID = Guid.NewGuid().ToString();
            }

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

            entity.Identity = user.Identity;
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
