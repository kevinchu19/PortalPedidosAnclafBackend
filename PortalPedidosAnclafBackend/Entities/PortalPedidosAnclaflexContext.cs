using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class PortalPedidosAnclaflexContext : DbContext
    {
        public PortalPedidosAnclaflexContext()
        {
        }

        public PortalPedidosAnclaflexContext(DbContextOptions<PortalPedidosAnclaflexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Clientesdireccionesentrega> Clientesdireccionesentregas { get; set; }
        public virtual DbSet<Listasdeprecio> Listasdeprecios { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Pedidositem> Pedidositems { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        
        public virtual DbSet<Provincia> Provincias { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.CodigoPostalEntrega)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
                
                entity.Property(e => e.LocalidadEntrega)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.CodigoPostalFacturacion)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.LocalidadFacturacion)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.DireccionEntrega)
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.DireccionFacturacion)
                    .IsRequired()
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.ListaPrecios)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.NumeroDocumento)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.PaisEntrega)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.PaisFacturacion)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.ProvinciaEntrega)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.ProvinciaFacturacion)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.RazonSocial)
                    .IsRequired()
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.TipoDocumento)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
            });

            modelBuilder.Entity<Clientesdireccionesentrega>(entity =>
            {
                entity.HasKey(e => new { e.IdCliente, e.Id })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("clientesdireccionesentrega");

                entity.Property(e => e.IdCliente)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.CodigoPostalEntrega)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.LocalidadEntrega)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.DireccionEntrega)
                    .IsRequired()
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Paisentrega)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.ProvinciaEntrega)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Clientesdireccionesentregas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente");
            });

            modelBuilder.Entity<Listasdeprecio>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Fecha, e.Idproducto })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("listasdeprecio");

                entity.HasIndex(e => e.Idproducto, "FK_ListasDePrecio_Productos");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("id")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Idproducto)
                    .HasColumnType("varchar(40)")
                    .HasColumnName("idproducto")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Precio)
                    .HasPrecision(10)
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Listasdeprecios)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListasDePrecio_Productos");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedidos");

                entity.HasIndex(e => new { e.IdCliente, e.IdEntrega }, "FK_Pedidos_DireccionesEntrega");

                entity.Property(e => e.CodigoPostalEntrega)
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.DireccionEntrega)
                    .IsRequired()
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.IdCliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.IdClienteEntrega)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.IdEntrega)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.ListaPrecios)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.PaisEntrega)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.ProvinciaEntrega)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Cliente");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => new { d.IdCliente, d.IdEntrega })
                    .HasConstraintName("FK_Pedidos_DireccionesEntrega");
            });

            modelBuilder.Entity<Pedidositem>(entity =>
            {
                entity.HasKey(e => new { e.IdPedido, e.Item })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("pedidositems");

                entity.HasIndex(e => e.IdProducto, "FK_PedidosItems_Productos");

                entity.Property(e => e.Cantidad).HasPrecision(10);

                entity.Property(e => e.IdProducto)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Precio).HasPrecision(10);

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Pedidositems)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidosItems_Pedidos");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Pedidositems)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidosItems_Productos");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Bonificacion).HasPrecision(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Precio).HasPrecision(10);
            });
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("provincias");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("id")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.HasMany(d => d.IdClienteFacturacionNavigation)
                   .WithOne(p => p.ProvinciaFacturacionNavigation)
                   .HasForeignKey(d => d.ProvinciaFacturacion)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Clientes_Provincias_Facturacion");

                entity.HasMany(d => d.IdClienteEntregaNavigation)
                   .WithOne(p => p.ProvinciaEntregaNavigation)
                   .HasForeignKey(d => d.ProvinciaEntrega)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Clientes_Provincias_Entrega");

                entity.HasMany(d => d.IdClienteNavigation)
                   .WithOne(p => p.ProvinciaEntregaNavigation)
                   .HasForeignKey(d => d.ProvinciaEntrega)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_ClientesDireccionesEntrega_Provincias");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
