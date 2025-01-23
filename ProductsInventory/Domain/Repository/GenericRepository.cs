

using ProductsInventory.Domain.Repository.Interface;

namespace ProductsInventory.Domain.Repository
{
    public interface IGenericRepository<TEntity, TEntityId> : IAdd<TEntity>
    {
        List<TEntity> GetAll();
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntityId id);
    }
}
