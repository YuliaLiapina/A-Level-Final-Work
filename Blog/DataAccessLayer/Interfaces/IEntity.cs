namespace DataAccessLayer.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
