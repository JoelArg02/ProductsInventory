namespace ProductsInventory.Controllers.DTO
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Stock {  get; set; } 
        public required decimal Price { get; set; }
        public required int CategoryId { get; set; }
    }
}