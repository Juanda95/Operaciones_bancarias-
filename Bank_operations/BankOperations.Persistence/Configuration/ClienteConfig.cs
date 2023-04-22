using BankOperations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankOperations.Persistence.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Clienteid)
                .HasMaxLength(10)
                 .IsRequired();

            builder.Property(e => e.Contrasena)
                .HasMaxLength(50)
                 .IsRequired();

            builder.Property(e => e.Estado)
                .HasMaxLength(6)
                .IsRequired();
        }
    }
}
