using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;
using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Application.Service
{
    public class ProductService : IProductService<Product, int>
    {
        private readonly IGenericRepository<Product, int> productRepository;

        public ProductService(IGenericRepository<Product, int> _productRepository)
        {
            this.productRepository = _productRepository;
        }

        public Product AddEntity(ProductDto entity)
        {
            var product = new Product
            {
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Stock = entity.Stock,

                CategoryId = entity.CategoryId
            };

            return productRepository.AddEntity(product);
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public void EditEntity(ProductDto entity)
        {
            var existingProduct = productRepository.GetAll().FirstOrDefault(p => p.Id == entity.Id);

            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            existingProduct.Name = entity.Name;
            existingProduct.Price = entity.Price;
            existingProduct.CategoryId = entity.CategoryId;

            productRepository.EditEntity(existingProduct);
        }

        public void DeleteEntity(int id)
        {
            productRepository.DeleteEntity(id);
        }
    }
}