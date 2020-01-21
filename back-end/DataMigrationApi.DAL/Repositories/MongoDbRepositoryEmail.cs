using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.DAL.Repositories
{
    public partial class MongoDbRepository
    {
        public IEnumerable<Email> GetEmails(EmailParameters parameters)
        {
            var emailProjection = Builders<User>.Projection.Expression<IEnumerable<Email>>(u => u.Emails);
            return _users.Find(FilterDefinition<User>.Empty).Project(emailProjection)
                .ToEnumerable()
                .SelectMany(e => e)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);
        }

        public IEnumerable<Email> GetAllUserEmails(string id) =>
            _users.Find(u => u.ID == id).FirstOrDefault()?.Emails;

        public IEnumerable<Email> GetAllUserEmails(string id, EmailParameters parameters) =>
            _users.Find(u => u.ID == id)
                .FirstOrDefault()?
                .Emails
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        public Email GetEmailById(int id)
        {
            var filter = Builders<User>.Filter.ElemMatch(u => u.Emails, e => e.ID == id);
            var user = _users.Find(filter).FirstOrDefault();

            return user?.Emails.Find(e => e.ID == id);
        }

        public Email InsertEmail(Email email, string userId)
        {
            var user = _users.Find(u => u.ID == userId).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            email.ID = FindMaxEmailIdentity() + 1;
            if (user.Emails == null)
            {
                user.Emails = new List<Email>();
            }
            var update = Builders<User>.Update.AddToSet(u => u.Emails, email);
            _users.FindOneAndUpdate(u => u.ID == userId, update);
            return email;
        }

        public Email UpdateEmail(Email email, string userId)
        {
            var filter = Builders<User>.Filter.ElemMatch(u => u.Emails, e => e.ID == email.ID);
            var user = _users.Find(filter).FirstOrDefault();

            var update = Builders<User>.Update;
            var emailSetter = update.Set(u => u.Emails[-1], email);
            var result = _users.UpdateOne(filter, emailSetter);
            return result.ModifiedCount > 0 ? email : null;
        }

        public void DeleteEmail(int id)
        {
            var filter = Builders<User>.Filter.ElemMatch(u => u.Emails, e => e.ID == id);
            var user = _users.Find(filter).FirstOrDefault();
            var email = user?.Emails.Find(e => e.ID == id);

            var update = Builders<User>.Update;
            var pull = update.Pull(u => u.Emails, email);
            _users.UpdateOne(filter, pull);
        }


        private int FindMaxEmailIdentity()
        {
            int max = 0;
            foreach (var user in _users.AsQueryable())
            {
                if (user.Emails == null || user.Emails.Count == 0) continue;
                int localMax = user.Emails.Max(e => e.ID);
                if (localMax > max)
                {
                    max = localMax;
                }
            }
            return max;
        }
    }
}
