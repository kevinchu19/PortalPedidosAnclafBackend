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
        public virtual DbSet<Vendedores> Vendedores { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Transportistasredespacho> Transportistasredespachos { get; set; }

        public virtual DbSet<Bonificacion> Bonificaciones { get; set; }

        public virtual DbSet<CuentaCorriente> Cuentascorrientes { get; set; }

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

                entity.Property(e => e.TransportistaRedespacho)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");


                entity.Property(e => e.GrupoBonificacion)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
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
                
                entity.Property(e => e.TransportistaRedespacho)
                   .HasColumnType("varchar(6)")
                   .HasCharSet("utf8")
                   .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Clientesdireccionesentregas)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DireccionesEntrega_Clientes");
            });

            modelBuilder.Entity<Listasdeprecio>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Idproducto, e.Fecha })
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

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Idproducto)
                    .HasColumnType("varchar(40)")
                    .HasColumnName("idproducto")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

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

                entity.Property(e => e.TransportistaRedespacho)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
                    
                entity.Property(e => e.RetiradeFabrica)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");
                
                entity.Property(e => e.Transferido)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Observacion)
                        .HasColumnType("longtext")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");
                    
                entity.Property(e => e.ObservacionLogistica)
                        .HasColumnType("longtext")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");
                    
                entity.Property(e => e.EsBarrioCerrado)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");


                entity.Property(e => e.PagoEnEfectivo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Acopio)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.DireccionModificada)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.IdVendedor)
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Telefono)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");


                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.FechaDeEntrega)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.IdUsuario)
                   .HasColumnType("varchar(100)")
                   .HasCharSet("utf8")
                   .HasCollation("utf8_bin");


                entity.HasOne(d => d.Cliente)
                            .WithMany(p => p.Pedidos)
                            .HasForeignKey(d => d.IdCliente)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_Pedidos_Cliente");
                
                entity.HasOne(d => d.ProvinciaEntregaNavigation)
                            .WithMany(p => p.IdPedidoNavigation)
                            .HasForeignKey(d => d.ProvinciaEntrega)
                            .HasConstraintName("FK_Pedidos_Provincias");

                entity.HasOne(d => d.IdEntregaNavigation)
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
                
                entity.Property(e => e.Bonificacion1).HasPrecision(10);
                
                entity.Property(e => e.Bonificacion2).HasPrecision(10);
                
                entity.Property(e => e.Bonificacion3).HasPrecision(10);

                entity.Property(e => e.Bonificacion4).HasPrecision(10);

                entity.Property(e => e.Bonificacion).HasPrecision(10);

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Items)
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

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.TipoProducto)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
                
                entity.Property(e => e.Rubro1)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Rubro2)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.ClienteExclusivo)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Visibilidad)
                    .HasColumnType("char(1)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

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

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
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

            modelBuilder.Entity<Vendedores>(entity =>
            {
                entity.ToTable("vendedores");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("id")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasColumnName("descripcion")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("descripcion")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Idcliente)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("idcliente")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Idvendedor)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("idvendedor")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("password")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuarios_Clientes");
                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Idvendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuarios_Vendedores");
            });

            modelBuilder.Entity<Transportistasredespacho>(entity =>
            {
                entity.ToTable("transportistasredespacho");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("id")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasColumnName("descripcion")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
            });

            modelBuilder.Entity<Bonificacion>(entity =>
            {
                entity.ToTable("bonificaciones");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bonificacion1)
                    .HasPrecision(10, 2)
                    .HasColumnName("bonificacion1");

                entity.Property(e => e.Bonificacion2)
                    .HasPrecision(10, 2)
                    .HasColumnName("bonificacion2");

                entity.Property(e => e.Bonificacion3)
                    .HasPrecision(10, 2)
                    .HasColumnName("bonificacion3");


                entity.Property(e => e.Idgrupobonificacion)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("idgrupobonificacion")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Idnumerorubro).HasColumnName("idnumerorubro");

                entity.Property(e => e.Tipoproducto)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("tipoproducto")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Valorrubro)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("valorrubro")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Activo)
                        .HasColumnType("smallint")
                        .HasCharSet("utf8")
                        .HasCollation("utf8_bin");

                entity.Property(e => e.Created_At)
                    .HasColumnType("datetime")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");
            });

            modelBuilder.Entity<CuentaCorriente>(entity =>
            {
                
                entity.ToTable("cuentascorrientes");

                entity.HasComment("		");

                entity.Property(e => e.Empresa)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("empresa")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Codigoformulario)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("codigoformulario")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Numeroformulario).HasColumnName("numeroformulario");

                entity.Property(e => e.Empresaaplicacion)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("empresaaplicacion")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Formularioaplicacion)
                    .HasColumnType("varchar(6)")
                    .HasColumnName("formularioaplicacion")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Numeroformularioaplicacion).HasColumnName("numeroformularioaplicacion");

                entity.Property(e => e.Cuota).HasColumnName("cuota");

                entity.Property(e => e.Fechamovimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamovimiento");

                entity.Property(e => e.Fechavencimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechavencimiento");

                entity.Property(e => e.Idcliente)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasColumnName("idcliente")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Importeextranjera)
                    .HasPrecision(18, 2)
                    .HasColumnName("importeextranjera");

                entity.Property(e => e.Importenacional)
                    .HasPrecision(18, 2)
                    .HasColumnName("importenacional");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
