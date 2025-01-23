namespace ProductsInventory.Domain.Entities
{
    public class Customer
    {
        public int? Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public ICollection<Sale>? Sales { get; set; }
    }
}