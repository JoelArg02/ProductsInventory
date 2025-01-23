using ProductsInventory.Domain.Entities;
using ProductsInventory.Domain.Repository;
using ProductsInventory.Controllers.DTO;

namespace ProductsInventory.Application.Service
{
    public class SaleService : ISaleService<Sale, int>
    {
        private readonly IGenericRepository<Sale, int> saleRepository;

        public SaleService(IGenericRepository<Sale, int> _saleRepository)
        {
            this.saleRepository = _saleRepository;
        }

        public Sale AddEntity(SaleDto entity)
        {
            var sale = new Sale
            {

                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                SaleDate = entity.SaleDate,
                Total = entity.Total,
                CustomerId = entity.CustomerId,
            };

            return saleRepository.AddEntity(sale);
        }

        public List<Sale> GetAll()
        {
            return saleRepository.GetAll();
        }

        public void EditEntity(SaleDto entity)
        {
            var existingSale = saleRepository.GetAll().FirstOrDefault(s => s.Id == entity.Id);

            if (existingSale == null)
            {
                throw new InvalidOperationException("Sale not found.");
            }

            existingSale.ProductId = entity.ProductId;
            existingSale.Quantity = entity.Quantity;
            existingSale.SaleDate = entity.SaleDate;

            saleRepository.EditEntity(existingSale);
        }

        public void DeleteEntity(int id)
        {
            saleRepository.DeleteEntity(id);
        }
    }
}