using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using System;
using System.Collections.Generic;

namespace DataMigrationApi.Services
{
    public class SqlServerUserService : ISqlServerUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlServerUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> Get()
        {
            var users = _unitOfWork.SqlServerUserRepository.GetAll();
            return users;
        }

        public User Get(string id)
        {
            var user = _unitOfWork.SqlServerUserRepository.GetById(id);
            return user;
        }

        public User Insert(User entity)
        {
            if (!Guid.TryParse(entity.ID, out _) || Get(entity.ID) != null)
            {
                entity.ID = Guid.NewGuid().ToString();
            }

            _unitOfWork.SqlServerUserRepository.Insert(entity);
            _unitOfWork.Save();
            return entity;
        }

        public User Update(User entity)
        {
            var user = Get(entity.ID);
            if (user == null)
            {
                return null;
            }

            entity.Identity = user.Identity;
            var userToUpdate = _unitOfWork.SqlServerUserRepository.Update(entity);

            _unitOfWork.Save();
            return userToUpdate;
        }

        public void Delete(string id)
        {
            var user = Get(id);
            if (user == null)
            {
                return;
            }
            _unitOfWork.SqlServerUserRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
