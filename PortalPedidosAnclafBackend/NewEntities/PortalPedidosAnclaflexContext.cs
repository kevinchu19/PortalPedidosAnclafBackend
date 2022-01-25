using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PortalPedidosAnclafBackend.NewEntities
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

        public virtual DbSet<Cuentascorriente> Cuentascorrientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Password1!;database=PortalPedidosAnclaflex", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.23-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuentascorriente>(entity =>
            {
                entity.HasKey(e => new { e.Empresa, e.Codigoformulario, e.Numeroformulario, e.Empresaaplicacion, e.Formularioaplicacion, e.Numeroformularioaplicacion, e.Cuota })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0, 0, 0, 0 });

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
