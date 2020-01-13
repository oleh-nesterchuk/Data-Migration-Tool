using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities;
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

        public IEnumerable<Email> Get() =>
            _unitOfWork.SqlServerEmailRepository.GetAll();

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

        public Email Get(int id) =>
            _unitOfWork.SqlServerEmailRepository.GetById(id);

        public Email Insert(Email entity)
        {
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
