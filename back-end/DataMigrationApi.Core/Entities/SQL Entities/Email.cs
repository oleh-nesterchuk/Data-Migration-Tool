using System.Text.Json.Serialization;

namespace DataMigrationApi.Core.Entities.SQL_Entities
{
    public class Email : IEntity<int>
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public bool IsConfirmed { get; set; }

        public string UserID { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
