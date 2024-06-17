using Microsoft.EntityFrameworkCore;

namespace CoredbInfrastructure.Collections.Tables;

public partial class EntityFrameworkContext : DbContext
{
    public EntityFrameworkContext()
    {
    }

    public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__clientes__885457EE68168A6C");

            entity.ToTable("clientes");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.CCorreo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cCorreo");
            entity.Property(e => e.CDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cDocumento");
            entity.Property(e => e.CNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cNombre");
            entity.Property(e => e.CNumeroTelefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cNumeroTelefono");
            entity.Property(e => e.DFechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaRegistro");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("idTipoDocumento");
            entity.Property(e => e.NDiasRegistro).HasColumnName("nDiasRegistro");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__clientes__idEmpr__658C0CBD");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__empresas__75D2CED42B7C2F3F");

            entity.ToTable("empresas");

            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.CNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cNombre");
            entity.Property(e => e.DFechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaRegistro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
