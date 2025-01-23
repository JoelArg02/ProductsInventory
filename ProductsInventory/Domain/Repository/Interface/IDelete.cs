namespace ProductsInventory.Domain.Repository.Interface
{
    public interface IDelete<in TEntityId>
    {
        void Delete(TEntityId entityId);
    }
}
