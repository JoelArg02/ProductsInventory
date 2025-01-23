using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;

namespace ProductsInventory.Infraestructure.Repository
{
    public class ProductRepository : IGenericRepository<Product, int>
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext _context)
        {
            context = _context;
        }

        public Product AddEntity(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public void EditEntity(Product entity)
        {
            context.Products.Update(entity);
            context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}