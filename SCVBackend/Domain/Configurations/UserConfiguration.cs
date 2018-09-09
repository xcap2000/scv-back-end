using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCVBackend.Domain.Entities;

namespace SCVBackend.Domain.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired();

            builder.Property(u => u.Type)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(64)
                .IsFixedLength()
                .IsRequired();

            builder.Property(u => u.Salt)
                .HasMaxLength(16)
                .IsFixedLength()
                .IsRequired();

            builder.Property(u => u.Photo)
                .IsRequired();
        }
    }
}