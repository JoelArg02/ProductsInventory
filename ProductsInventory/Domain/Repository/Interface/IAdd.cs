namespace ProductsInventory.Domain.Repository.Interface
{
    public interface IAdd<TEntidad>
    {
        TEntidad AddEntity(TEntidad entity);
    }
}
