using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Domain.Repository
{
    public interface ICategoryService<TEntity, TEntityId>
    {
        TEntity AddEntity(CategoryDto entity);
        List<TEntity> GetAll();
        void EditEntity(CategoryDto entity);
        void DeleteEntity(TEntityId id);
    }
}