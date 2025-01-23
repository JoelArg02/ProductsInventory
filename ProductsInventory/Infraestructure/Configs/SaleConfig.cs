namespace ProductsInventory.Infraestructure.Configs
{
    using ProductsInventory.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.ProductId).HasColumnName("ProductId").IsRequired();
            builder.Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            builder.Property(x => x.SaleDate).HasColumnName("SaleDate").IsRequired();
            builder.Property(x => x.Total).HasColumnName("Total").IsRequired();
            builder.Property(x => x.CustomerId).HasColumnName("CustomerId").IsRequired();

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.Sales)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Sales)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}