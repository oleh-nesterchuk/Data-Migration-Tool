using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities.SQL_Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Services
{
    public class SqlServerUserService : ISqlServerUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlServerUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<User> Get()
        {
            var users = _unitOfWork.SqlServerUserRepository.GetAll().ToList();
            return users;
        }

        public User Get(string id)
        {
            var user = _unitOfWork.SqlServerUserRepository.GetById(id);
            return user;
        }

        public User Insert(User user)
        {
            _unitOfWork.SqlServerUserRepository.Insert(user);
            return user;
        }

        public User Update(User user)
        {
            var userToUpdate = _unitOfWork.SqlServerUserRepository.Update(user);
            return userToUpdate;
        }

        public void Delete(string id)
        {
            _unitOfWork.SqlServerUserRepository.Delete(id);
        }
    }
}
