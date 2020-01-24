using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities;
using DataMigrationApi.Core.Paging;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMigrationApi.DAL.Repositories
{
    public partial class MongoDbRepository : IMongoDbUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly ProjectionDefinition<User, User> _emailProjection;
        private readonly ProjectionDefinition<User, User> _ageProjection;

        static MongoDbRepository()
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

        public MongoDbRepository(IMongoDBSettings mongoSettings)
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

        public int GetSize() =>
            (int)_users.CountDocuments(FilterDefinition<User>.Empty);

        public IEnumerable<User> GetAll(UserParameters parameters) =>
            _users.Find(FilterDefinition< User>.Empty)
                .SortBy(u => u.LastName)
                .Project(_emailProjection)
                .Project(_ageProjection)
                .ToEnumerable()
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

        public IEnumerable<User> GetAllWithoutProjection(UserParameters parameters) =>
            _users.Find(FilterDefinition<User>.Empty)
                .Project(_emailProjection)
                .Project(_ageProjection)
                .ToEnumerable()
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

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
            return entity.CalculateAge();
        }

        public User Update(User entity)
        {
            var filter = Builders<User>.Filter.Where(u => u.ID == entity.ID);
            var user = _users.Find(filter).FirstOrDefault();
            entity.Identity = user.Identity;
            entity.Emails = user.Emails;
            _users.ReplaceOne(filter, entity);
            return entity.CalculateAge();
        }

        public void Delete(string id) =>
            _users.DeleteOne(u => u.ID == id);
        

        private int FindMaxUserIdentity()
        {
            if (_users.EstimatedDocumentCount() == 0)
            {
                return 0;
            }
            var max = _users.AsQueryable().Max(u => u.Identity);
            return max;
        }
    }
}
