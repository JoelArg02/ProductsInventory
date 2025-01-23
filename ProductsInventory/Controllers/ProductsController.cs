using ProductsInventory.Controllers.DTO;
using ProductsInventory.Application.Service;
using Microsoft.AspNetCore.Mvc;
using ProductsInventory.Domain.Entities;
using ProductsInventory.Infraestructure.Repository;
using ProductsInventory.Infraestructure;

namespace ProductsInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext db;

        public ProductsController(AppDbContext _db)
        {
            this.db = _db;
        }

        private ProductService CreateService()
        {
            var productRepo = new ProductRepository(db);
            var service = new ProductService(productRepo);
            return service;
        }

        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddProduct([FromBody] ProductDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Product data cannot be null.");

                var result = CreateService().AddEntity(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public IActionResult GetAllProducts()
        {
            var result = CreateService().GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Route("EditProduct")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult EditProduct([FromBody] ProductDto product)
        {
            CreateService().EditEntity(product);
            return Ok("Product updated successfully."); 
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            var service = CreateService();
            var product = service.GetAll().FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            service.DeleteEntity(id);
            return Ok("Product deleted successfully.");
        }
    }
}