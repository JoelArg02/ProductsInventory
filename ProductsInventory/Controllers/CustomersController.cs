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
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext db;

        public CustomersController(AppDbContext _db)
        {
            this.db = _db;
        }

        private CustomerService CreateService()
        {
            var customerRepo = new CustomerRepository(db);
            var service = new CustomerService(customerRepo);
            return service;
        }

        [HttpPost]
        [Route("AddCustomer")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddCustomer([FromBody] CustomerDto entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest("Customer data cannot be null.");

                var result = CreateService().AddEntity(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        public IActionResult GetAllCustomers()
        {
            var result = CreateService().GetAll();
            return Ok(result);
        }

        [HttpPut]
        [Route("EditCustomer")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult EditCustomer([FromBody] CustomerDto customer)
        {
            CreateService().EditEntity(customer);
            return Ok("Customer updated successfully."); 
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult DeleteCustomer(int id)
        {
            var service = CreateService();
            var customer = service.GetAll().FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            service.DeleteEntity(id);
            return Ok("Customer deleted successfully.");
        }
    }
}