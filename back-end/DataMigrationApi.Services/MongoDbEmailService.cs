using System.Collections.Generic;
using System.Linq;
using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities.NoSQL_Entities;

namespace DataMigrationApi.Services
{
    public class MongoDbEmailService : IMongoDbEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MongoDbEmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Email> Get() =>
            _unitOfWork.MongoDbRepository.GetEmails().ToList();

        public List<Email> GetAllUserEmails(string id) =>
            _unitOfWork.MongoDbRepository.GetAllUserEmails(id).ToList();

        public Email Get(int id) =>
            _unitOfWork.MongoDbRepository.GetEmailById(id);

        public Email Insert(Email entity)
        {
            var inserted = _unitOfWork.MongoDbRepository.InsertEmail(entity);
            return inserted;
        }

        public Email Update(Email entity)
        {
            var updated = _unitOfWork.MongoDbRepository.UpdateEmail(entity);
            return updated;
        }

        public void Delete(int id) =>
            _unitOfWork.MongoDbRepository.DeleteEmail(id);
    }
}
