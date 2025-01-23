using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;

namespace ProductsInventory.Infraestructure.Repository
{
    public class CustomerRepository : IGenericRepository<Customer, int>
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext _context)
        {
            context = _context;
        }

        public Customer AddEntity(Customer entity)
        {
            context.Customers.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public void EditEntity(Customer entity)
        {
            context.Customers.Update(entity);
            context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var customer = context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }
    }
}