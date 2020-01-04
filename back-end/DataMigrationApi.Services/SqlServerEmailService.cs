using DataMigrationApi.Core.Abstractions;
using DataMigrationApi.Core.Abstractions.Services;
using DataMigrationApi.Core.Entities.SQL_Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.Services
{
    public class SqlServerEmailService : ISqlServerEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SqlServerEmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Email> Get() =>
            _unitOfWork.SqlServerEmailRepository.GetAll().ToList();

        public List<Email> GetAllUserEmails(string id) =>
            _unitOfWork.SqlServerEmailRepository.GetAllUserEmails(id).ToList();

        public Email Get(int id) =>
            _unitOfWork.SqlServerEmailRepository.GetById(id);

        public Email Insert(Email entity)
        {
            var inserted = _unitOfWork.SqlServerEmailRepository.Insert(entity);
            return inserted;
        }

        public Email Update(Email entity)
        {
            var updated = _unitOfWork.SqlServerEmailRepository.Update(entity);
            return updated;
        }

        public void Delete(int id)
        {
            _unitOfWork.SqlServerEmailRepository.Delete(id);
        }
    }
}
