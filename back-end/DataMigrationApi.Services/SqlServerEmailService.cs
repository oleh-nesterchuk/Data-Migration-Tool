using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using System.Collections.Generic;

namespace DataMigrationApi.Services
{
    public class SqlServerEmailService : ISqlServerEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SqlServerEmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GetSize() =>
            _unitOfWork.SqlServerEmailRepository.GetSize();

        public int GetSize(string id) =>
            _unitOfWork.SqlServerEmailRepository.GetSize(id);

        public IEnumerable<Email> Get(EmailParameters parameters) =>
            _unitOfWork.SqlServerEmailRepository.GetAll(parameters);

        public IEnumerable<Email> GetAllUserEmails(string id)
        {
            var user = _unitOfWork.SqlServerUserRepository.GetById(id);
            if (user == null)
            {
                return null;
            }

            var emails = _unitOfWork.SqlServerEmailRepository.GetAllUserEmails(id);
            return emails;
        }

        public IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters)
        {
            var user = _unitOfWork.SqlServerUserRepository.GetById(id);
            if (user == null)
            {
                return null;
            }

            var emails = _unitOfWork.SqlServerEmailRepository.GetAllUserEmails(id, parameters);
            return emails;
        }

        public Email Get(int id) =>
            _unitOfWork.SqlServerEmailRepository.GetById(id);

        public Email Insert(Email entity)
        {
            entity.ID = 0;
            var inserted = _unitOfWork.SqlServerEmailRepository.Insert(entity);
            _unitOfWork.Save();
            return inserted;
        }

        public Email Update(Email entity)
        {
            var email = Get(entity.ID);
            if (email == null)
            {
                return null;
            }

            entity.UserID = email.UserID;
            var updated = _unitOfWork.SqlServerEmailRepository.Update(entity);
            _unitOfWork.Save();
            return updated;
        }

        public void Delete(int id)
        {
            var email = Get(id);
            if (email == null)
            {
                return;
            }

            _unitOfWork.SqlServerEmailRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
