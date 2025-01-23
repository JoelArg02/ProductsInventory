namespace ProductsInventory.Infraestructure.Configs
{
    using ProductsInventory.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.Price).HasColumnName("Price").IsRequired();
            builder.Property(x => x.Stock).HasColumnName("Stock").IsRequired();
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId").IsRequired();

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}