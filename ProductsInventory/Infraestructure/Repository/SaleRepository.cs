using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;

namespace ProductsInventory.Infraestructure.Repository
{
    public class SaleRepository : IGenericRepository<Sale, int>
    {
        private readonly AppDbContext context;

        public SaleRepository(AppDbContext _context)
        {
            context = _context;
        }

        public Sale AddEntity(Sale entity)
        {
            context.Sales.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<Sale> GetAll()
        {
            return context.Sales.ToList();
        }

        public void EditEntity(Sale entity)
        {
            context.Sales.Update(entity);
            context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var sale = context.Sales.FirstOrDefault(x => x.Id == id);
            if (sale != null)
            {
                context.Sales.Remove(sale);
                context.SaveChanges();
            }
        }
    }
}