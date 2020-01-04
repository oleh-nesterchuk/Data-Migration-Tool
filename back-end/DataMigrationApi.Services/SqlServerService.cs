using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities.SQL_Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Services
{
    public class SqlServerService : ISqlServerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlServerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<User> Get()
        {
            var users = _unitOfWork.SqlServerRepository.GetAll().ToList();
            return users;
        }

        public User Get(string id)
        {
            var user = _unitOfWork.SqlServerRepository.GetById(id);
            return user;
        }

        public User Insert(User user)
        {
            _unitOfWork.SqlServerRepository.Insert(user);
            return user;
        }

        public User Update(User user)
        {
            var userToUpdate = _unitOfWork.SqlServerRepository.Update(user);
            return userToUpdate;
        }

        public void Delete(string id)
        {
            _unitOfWork.SqlServerRepository.Delete(id);
        }
    }
}
