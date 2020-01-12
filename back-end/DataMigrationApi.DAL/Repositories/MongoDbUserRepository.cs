using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.DAL.Repositories
{
    public class MongoDbUserRepository : IMongoDbUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly ProjectionDefinition<User, User> _emailProjection;
        private readonly ProjectionDefinition<User, User> _ageProjection;

        static MongoDbUserRepository()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(u => u.ID).SetIdGenerator(StringObjectIdGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<Email>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(e => e.ID).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.UnmapMember(e => e.User);
                cm.UnmapMember(e => e.UserID);
            });
        }

        public MongoDbUserRepository(IMongoDBSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _users = database.GetCollection<User>(mongoSettings.UserCollectionName);

            var userBuilder = Builders<User>.IndexKeys;
            var indexModel = new CreateIndexModel<User>(userBuilder.Ascending(u => u.Identity));
            _users.Indexes.CreateOne(indexModel);

            _emailProjection = Builders<User>.Projection.Exclude(u => u.Emails);
            _ageProjection = Builders<User>.Projection.Expression(u => u.CalculateAge());
        }

        public IEnumerable<User> GetAll() =>
            _users.Find(FilterDefinition<User>.Empty)
            .Project(_emailProjection).Project(_ageProjection).Project(_ageProjection).ToEnumerable();

        public IEnumerable<User> GetAllWithoutProjection() =>
            _users.Find(FilterDefinition<User>.Empty).ToEnumerable();

        public User GetById(string id) =>
            _users.Find(u => u.ID == id).FirstOrDefault();

        public User Insert(User entity)
        {
            if (!Guid.TryParse(entity.ID, out _) || GetById(entity.ID) != null)
            {
                entity.ID = Guid.NewGuid().ToString();
            }
            entity.Identity = FindMaxUserIdentity() + 1;
            for (int i = 0; i < entity.Emails.Count; ++i)
            {
                entity.Emails[i].ID = FindMaxEmailIdentity() + i + 1;
            }
            _users.InsertOne(entity);
            return entity;
        }

        public User Update(User entity)
        {
            var filter = Builders<User>.Filter.Where(u => u.ID == entity.ID);
            var user = _users.Find(filter).FirstOrDefault();
            entity.Identity = user.Identity;
            _users.ReplaceOne(filter, entity);
            return entity;
        }

        public void Delete(string id) =>
            _users.DeleteOne(u => u.ID == id);
        


        public IEnumerable<Email> GetEmails()
        {
            var emailProjection = Builders<User>.Projection.Expression<IEnumerable<Email>>(u => u.Emails);
            return _users.Find(FilterDefinition<User>.Empty).Project(emailProjection)
                .ToEnumerable().SelectMany(e => e);
        }

        public IEnumerable<Email> GetAllUserEmails(string id) =>
            _users.Find(u => u.ID == id).FirstOrDefault()?.Emails;

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

        private int FindMaxUserIdentity()
        {
            if (_users.EstimatedDocumentCount() == 0)
            {
                return 0;
            }
            var max = _users.AsQueryable().Max(u => u.Identity);
            return max;
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
