using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Services
{
    public class MongoDbEmailService : IMongoDbEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MongoDbEmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Email> Get(EmailParameters parameters) =>
            _unitOfWork.MongoDbRepository.GetEmails(parameters);

        public IEnumerable<Email> GetAllUserEmails(string id) =>
            _unitOfWork.MongoDbRepository.GetAllUserEmails(id);

        public IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters) =>
            _unitOfWork.MongoDbRepository.GetAllUserEmails(id, parameters);

        public Email Get(int id) =>
            _unitOfWork.MongoDbRepository.GetEmailById(id);

        public Email Insert(Email entity, string userId)
        {
            var inserted = _unitOfWork.MongoDbRepository.InsertEmail(entity, userId);
            return inserted;
        }

        public Email Update(Email entity, string userId)
        {
            var updated = _unitOfWork.MongoDbRepository.UpdateEmail(entity, userId);
            return updated;
        }

        public void Delete(int id) =>
            _unitOfWork.MongoDbRepository.DeleteEmail(id);
    }
}
