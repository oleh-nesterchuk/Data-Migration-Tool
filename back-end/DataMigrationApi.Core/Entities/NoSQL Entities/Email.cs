using MongoDB.Bson.Serialization.Attributes;

namespace DataMigrationApi.Core.Entities.NoSQL_Entities
{
    public class Email : IEntity<int>
    {
        [BsonId]
        public int ID { get; set; }
        public string Value { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
