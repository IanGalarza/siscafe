using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeVentasCafe.Models;

public partial class DbapiContext : DbContext
{
    public DbapiContext()
    {
    }

    public DbapiContext(DbContextOptions<DbapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Facturaproducto> Facturaproductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //ModelBuilder Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__D59466421629B4E8");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        //ModelBuilder Factura
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__FACTURA__50E7BAF1153F5D44");

            entity.ToTable("FACTURA");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrecioTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.fCliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_IDCLIENTE");
        });

        //ModelBuilder Factura Productos
        modelBuilder.Entity<Facturaproducto>(entity =>
        {
            entity.HasKey(e => e.IdFacturaProductos).HasName("PK__FACTURAP__36F787A4DE48DE03");

            entity.ToTable("FACTURAPRODUCTOS");

            entity.HasOne(d => d.fFactura).WithMany(p => p.Lista_De_Productos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_IDFACTURA");

            entity.HasOne(d => d.fProducto).WithMany(p => p.Facturaproductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_IDPRODUCTO");
        });

        //ModelBuilder Producto
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__0988921094777190");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaVencimiento).HasColumnName("fechaVencimiento");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
