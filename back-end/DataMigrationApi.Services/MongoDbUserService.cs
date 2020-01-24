using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Services
{
    public class MongoDbUserService : IMongoDbUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MongoDbUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GetSize() =>
            _unitOfWork.MongoDbRepository.GetSize();

        public IEnumerable<User> Get(UserParameters parameters) =>
            _unitOfWork.MongoDbRepository.GetAll(parameters);

        public User Get(string id) =>
            _unitOfWork.MongoDbRepository.GetById(id);

        public User Insert(User user)
        {
            var inserted = _unitOfWork.MongoDbRepository.Insert(user);
            return inserted;
        }

        public User Update(User user)
        {
            var updated = _unitOfWork.MongoDbRepository.Update(user);
            return updated;
        }

        public void Delete(string id) =>
            _unitOfWork.MongoDbRepository.Delete(id);
    }
}
