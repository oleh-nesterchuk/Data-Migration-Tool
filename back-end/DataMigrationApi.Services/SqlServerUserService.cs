using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
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

        public User Insert(User user)
        {
            _unitOfWork.SqlServerUserRepository.Insert(user);
            _unitOfWork.Save();
            return user;
        }

        public User Update(User user)
        {
            var userToUpdate = _unitOfWork.SqlServerUserRepository.Update(user);
            _unitOfWork.Save();
            return userToUpdate;
        }

        public void Delete(string id)
        {
            _unitOfWork.SqlServerUserRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
