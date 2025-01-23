using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Domain.Repository
{
    public interface IProductService<TEntity, TEntityId>
    {
        TEntity AddEntity(ProductDto entity);
        List<TEntity> GetAll();
        void EditEntity(ProductDto entity);
        void DeleteEntity(TEntityId id);
    }
}