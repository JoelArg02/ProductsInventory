using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Domain.Repository
{
    public interface ICustomerService<TEntity, TEntityId>
    {
        TEntity AddEntity(CustomerDto entity);
        List<TEntity> GetAll();
        void EditEntity(CustomerDto entity);
        void DeleteEntity(TEntityId id);
    }
}