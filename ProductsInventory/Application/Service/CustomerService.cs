using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;
using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Application.Service
{
    public class CustomerService : ICustomerService<Customer, int>
    {
        private readonly IGenericRepository<Customer, int> customerRepository;

        public CustomerService(IGenericRepository<Customer, int> _customerRepository)
        {
            this.customerRepository = _customerRepository;
        }

        public Customer AddEntity(CustomerDto entity)
        {
            var customer = new Customer
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone
            };

            return customerRepository.AddEntity(customer);
        }

        public List<Customer> GetAll()
        {
            return customerRepository.GetAll();
        }

        public void EditEntity(CustomerDto entity)
        {
            var existingCustomer = customerRepository.GetAll().FirstOrDefault(c => c.Id == entity.Id);

            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Customer not found.");
            }

            existingCustomer.FirstName = entity.FirstName;
            existingCustomer.LastName = entity.LastName;
            existingCustomer.Email = entity.Email;
            existingCustomer.Phone = entity.Phone;

            customerRepository.EditEntity(existingCustomer);
        }

        public void DeleteEntity(int id)
        {
            customerRepository.DeleteEntity(id);
        }
    }
}