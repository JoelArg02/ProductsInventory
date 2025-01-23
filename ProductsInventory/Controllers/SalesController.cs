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
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext db;

        public SalesController(AppDbContext _db)
        {
            this.db = _db;
        }

        private SaleService CreateService()
        {
            var saleRepo = new SaleRepository(db);
            var service = new SaleService(saleRepo);
            return service;
        }

        [HttpPost]
        [Route("AddSale")]
        [ProducesResponseType(typeof(SaleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddSale([FromBody] SaleDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Sale data cannot be null.");

                var result = CreateService().AddEntity(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllSales")]
        [ProducesResponseType(typeof(List<Sale>), StatusCodes.Status200OK)]
        public IActionResult GetAllSales()
        {
            var result = CreateService().GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Route("EditSale")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult EditSale([FromBody] SaleDto sale)
        {
            CreateService().EditEntity(sale);
            return Ok("Sale updated successfully."); 
        }

        [HttpDelete]
        [Route("DeleteSale/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteSale(int id)
        {
            var service = CreateService();
            var sale = service.GetAll().FirstOrDefault(c => c.Id == id);
            if (sale == null)
            {
                return NotFound("Sale not found.");
            }

            service.DeleteEntity(id);
            return Ok("Sale deleted successfully.");
        }
    }
}