using ProductsInventory.Controllers.DTO;
using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;

namespace ProductsInventory.Application.Service
{
    public class CategoryService : ICategoryService<Category, int>
    {
        private readonly IGenericRepository<Category, int> categoryRepository;

        public CategoryService(IGenericRepository<Category, int> _categoryRepository)
        {
            this.categoryRepository = _categoryRepository;
        }

        public Category AddEntity(CategoryDto entity)
        {
            var category = new Category
            {
                Name = entity.Name
            };

            return categoryRepository.AddEntity(category);
        }

        public List<Category> GetAll()
        {
            return categoryRepository.GetAll();
        }

        public void EditEntity(CategoryDto entity)
        {
            var existingCategory = categoryRepository.GetAll().FirstOrDefault(c => c.Id == entity.Id);

            if (existingCategory == null)
            {
                throw new InvalidOperationException("Category not found.");
            }

            existingCategory.Name = entity.Name;

            categoryRepository.EditEntity(existingCategory);
        }

        public void DeleteEntity(int id)
        {
            categoryRepository.DeleteEntity(id);
        }
    }
}