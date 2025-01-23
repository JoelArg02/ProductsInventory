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
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext db;

        public CategoriesController(AppDbContext _db)
        {
            this.db = _db;
        }

        private CategoryService CreateService()
        {
            var categoryRepo = new CategoryRepository(db);
            var service = new CategoryService(categoryRepo);
            return service;
        }

        [HttpPost]
        [Route("AddCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddCategory([FromBody] CategoryDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Category data cannot be null.");

                var result = CreateService().AddEntity(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(typeof(List<Category>), StatusCodes.Status200OK)]
        public IActionResult GetAllCategories()
        {
            var result = CreateService().GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Route("EditCategory")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult EditCategory([FromBody] CategoryDto category)
        {
            CreateService().EditEntity(category);
            return Ok("Category updated successfully."); 
        }

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            var service = CreateService();
            var category = service.GetAll().FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            service.DeleteEntity(id);
            return Ok("Category deleted successfully.");
        }
    }
}