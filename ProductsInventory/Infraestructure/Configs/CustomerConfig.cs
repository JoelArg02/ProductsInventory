namespace ProductsInventory.Infraestructure.Configs
{
    using ProductsInventory.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired();
            builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired();
            builder.Property(x => x.Email).HasColumnName("Email").IsRequired();
            builder.Property(x => x.Phone).HasColumnName("Phone").IsRequired();
        }
    }
}