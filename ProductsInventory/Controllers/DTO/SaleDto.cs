namespace ProductsInventory.Controllers.DTO
{
    public class SaleDto
    {
        public int? Id { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required DateTime SaleDate { get; set; }
        public required decimal Total { get; set; }
        public required int CustomerId { get; set; }
    }
}