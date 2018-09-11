using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCVBackend.Domain.Entities;

namespace SCVBackend.Domain.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                .IsRequired();

            builder.Property(o => o.Street)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.State)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.Country)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.PostalCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.CreditCardNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.VerificationCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(o => o.Order)
                .WithOne(o => o.OrderDetails)
                .HasForeignKey<OrderDetails>(o => o.OrderId);
        }
    }
}