namespace ProductsInventory.Domain.Repository.Interface
{
    public interface IList< TEntity,in TEntityId>
    {
        int Count { get; }
        List<TEntity> ListEntity();
        TEntity? GetEntityById(TEntityId entityId);
    }
}
