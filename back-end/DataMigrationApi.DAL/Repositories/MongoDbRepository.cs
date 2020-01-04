using DataMigrationApi.Core.Abstractions.Repositories;
using DataMigrationApi.Core.Entities.NoSQL_Entities;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataMigrationApi.DAL.Repositories
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoCollection<User> _users;

        public MongoDbRepository(string connection)
        {
            var client = new MongoClient(connection);
            var database = client.GetDatabase("MigrationTool");

            _users = database.GetCollection<User>("Users");
        }

        public IQueryable<User> GetAll() =>
            _users.AsQueryable();

        public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate) =>
            _users.AsQueryable().Where(predicate);

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
    }
}
