using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;

namespace ProductsInventory.Infraestructure.Repository
{
    public class CategoryRepository : IGenericRepository<Category, int>
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext _context)
        {
            context = _context;
        }

        public Category AddEntity(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public void EditEntity(Category entity)
        {
            context.Categories.Update(entity);
            context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}