using BankOperations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Persistence.Configuration
{
    public class Movimientoconfig : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Movimien__3214EC07DD5034B8");

            builder.Property(e => e.Fecha).HasColumnType("datetime");
            builder.Property(e => e.Saldo)
                .HasColumnType("int")
                .HasColumnName("saldo");
            builder.Property(e => e.TipoMovimiento).HasColumnName("Tipo_movimiento");
            builder.Property(e => e.Valor)
                .HasColumnType("int")
                .HasColumnName("valor");

            builder.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Movimientos_Cliente");

            builder.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Movimientos_Cuenta");
        }
    }
}
