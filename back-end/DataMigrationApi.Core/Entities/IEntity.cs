namespace DataMigrationApi.Core.Entities
{
    public interface IEntity<T>
    {
        T ID { get; set; }
    }
}
