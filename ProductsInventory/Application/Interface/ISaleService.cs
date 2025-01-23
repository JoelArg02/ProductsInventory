using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Domain.Repository
{
    public interface ISaleService<TEntity, TEntityId>
    {
        TEntity AddEntity(SaleDto entity);
        List<TEntity> GetAll();
        void EditEntity(SaleDto entity);
        void DeleteEntity(TEntityId id);
    }
}