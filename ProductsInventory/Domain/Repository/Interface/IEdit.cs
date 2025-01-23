namespace ProductsInventory.Domain.Repository.Interface
{
    public interface IEdit<in TEntity>
    {
        void EditEntity(TEntity entity);
    }
}
