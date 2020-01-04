using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities.NoSQL_Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Services
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MongoDbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<User> Get()
        {
            var users = _unitOfWork.MongoDbRepository.GetAll().ToList();
            return users;
        }

        public User Get(string id)
        {
            var user = _unitOfWork.MongoDbRepository.GetById(id);
            return user;
        }

        public User Insert(User user)
        {
            _unitOfWork.MongoDbRepository.Insert(user);
            return user;
        }

        public User Update(User user)
        {
            var userToUpdate = _unitOfWork.MongoDbRepository.Update(user);
            return userToUpdate;
        }

        public void Delete(string id)
        {
            _unitOfWork.MongoDbRepository.Delete(id);
        }
    }
}
