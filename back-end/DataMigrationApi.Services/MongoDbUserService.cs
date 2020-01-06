using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities.NoSQL_Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Services
{
    public class MongoDbUserService : IMongoDbUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MongoDbUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> Get() =>
            _unitOfWork.MongoDbRepository.GetAll();
            
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
