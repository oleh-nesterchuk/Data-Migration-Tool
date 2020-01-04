using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities.NoSQL_Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataMigrationApi.DAL.Repositories
{
    public class MongoDbUserRepository : IMongoDbUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly ProjectionDefinition<User> _projection;

        public MongoDbUserRepository(string connection)
        {
            var client = new MongoClient(connection);
            var database = client.GetDatabase("MigrationTool");

            _users = database.GetCollection<User>("Users");

            var userBuilder = Builders<User>.IndexKeys;
            var indexModel = new CreateIndexModel<User>(userBuilder.Ascending(u => u.Identity));
            _users.Indexes.CreateOne(indexModel);

            _projection = Builders<User>.Projection.Exclude(u => u.Emails);
        }

        public IEnumerable<User> GetAll() =>
            _users.Find(x => true).Project<User>(_projection).ToEnumerable();

        public IEnumerable<User> GetAll(Expression<Func<User, bool>> predicate) =>
            _users.Find(predicate).Project<User>(_projection).ToEnumerable();

        public User GetById(string id) =>
            _users.Find(u => u.ID == id).FirstOrDefault();

        public User Insert(User entity)
        {
            entity.ID = new Guid().ToString();
            _users.InsertOne(entity);
            return entity;
        }

        public User Update(User entity)
        {
            var filter = Builders<User>.Filter.Where(u => u.ID == entity.ID);
            _users.ReplaceOne(filter, entity);
            return entity;
        }

        public void Delete(string id)
        {
            var filter = Builders<User>.Filter.Where(u => u.ID == id);
            _users.DeleteOne(filter);
        }

        public IEnumerable<Email> GetEmails()
        {
            foreach(var user in _users.Find(u => true).ToList())
            {
                foreach (var email in user.Emails)
                {
                    yield return email;
                }
            }
        }

        public IEnumerable<Email> GetAllUserEmails(string id) =>
            _users.Find(u => u.ID == id).FirstOrDefault().Emails;

        public Email GetEmailById(int id)
        {
            // POTENTIALLY A SOURCE OF ERROR //
            var filter = Builders<User>.Filter.ElemMatch(u => u.Emails, e => e.ID == id);
            var user = _users.Find(filter).FirstOrDefault();
            return user.Emails.Find(e => e.ID == id);
        }

        public Email InsertEmail(Email email)
        {
            var user = _users.Find(u => u.ID == email.UserID).FirstOrDefault();
            user.Emails.Add(email);
            return email;
        }

        public Email UpdateEmail(Email email)
        {
            var user = _users.Find(u => u.ID == email.UserID).FirstOrDefault();
            var oldEmail = user.Emails.Find(e => e.ID == email.ID);
            oldEmail = email;
            return oldEmail;
        }

        public void DeleteEmail(int id)
        {
            var filter = Builders<User>.Filter.ElemMatch(u => u.Emails, e => e.ID == id);
            var user = _users.Find(filter).FirstOrDefault();
            user.Emails.Remove(user.Emails.Find(e => e.ID == id));
        }
    }
}
