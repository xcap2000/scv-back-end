using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCVBackend.Domain.Entities;

namespace SCVBackend.Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.Property(p => p.BuyPrice)
                .IsRequired();

            builder.Property(p => p.SellPrice)
                .IsRequired();

            builder.Property(p => p.BrandId)
                .IsRequired();

            builder.Property(p => p.ProviderId)
                .IsRequired();

            builder.Property(p => p.Photo)
                .IsRequired();

            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.Provider)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProviderId);
        }
    }
}