namespace DataMigrationApi.Core.Entities.NoSQL_Entities
{
    public class Email : IEntity<int>
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
