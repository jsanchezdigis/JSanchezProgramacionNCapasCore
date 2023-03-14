using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JsanchezProgramacionNcapasContext : DbContext
{
    public JsanchezProgramacionNcapasContext()
    {
    }

    public JsanchezProgramacionNcapasContext(DbContextOptions<JsanchezProgramacionNcapasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aseguradora> Aseguradoras { get; set; }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Dependiente> Dependientes { get; set; }

    public virtual DbSet<DependienteTipo> DependienteTipos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EmpresaPoliza> EmpresaPolizas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<MovimientoDetalle> MovimientoDetalles { get; set; }

    public virtual DbSet<MovimientoTipo> MovimientoTipos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<SubPoliza> SubPolizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vigencium> Vigencia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= JSanchezProgramacionNCapas; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Aseguradora>(entity =>
        {
            entity.HasKey(e => e.IdAseguradora).HasName("PK__Asegurad__8FA1C5970F79A0A4");

            entity.ToTable("Aseguradora");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Aseguradoras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_UsuarioAseguradora");
        });

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__A1580F666D7CE9DE");

            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("fk_MunicipioColonia");
        });

        modelBuilder.Entity<Dependiente>(entity =>
        {
            entity.HasKey(e => e.IdDependiente).HasName("PK__Dependie__366D07717F357134");

            entity.ToTable("Dependiente");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDependienteTipoNavigation).WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.IdDependienteTipo)
                .HasConstraintName("fk_DependienteTipoDependiente");

            entity.HasOne(d => d.NumeroEmpleadoNavigation).WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.NumeroEmpleado)
                .HasConstraintName("fk_EmpleadoDependiente");
        });

        modelBuilder.Entity<DependienteTipo>(entity =>
        {
            entity.HasKey(e => e.IdDependienteTipo).HasName("PK__Dependie__2C220C6222E77000");

            entity.ToTable("DependienteTipo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C763A5B30E3");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("fk_ColoniaDireccion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_UsuarioDireccion");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.NumeroEmpleado).HasName("PK__Empleado__44F848FC28CAC288");

            entity.ToTable("Empleado");

            entity.HasIndex(e => e.NumeroEmpleado, "UNQ__Empleado__NumeroEmpleado").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Empleado__A9D10534D0BFED88").IsUnique();

            entity.Property(e => e.NumeroEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Rfc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("fk_EmpresaEmpleado");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033EE211DE3A");

            entity.ToTable("Empresa");

            entity.Property(e => e.DireccionWeb)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmpresaPoliza>(entity =>
        {
            entity.HasKey(e => e.IdAseguradoraPoliza).HasName("PK__EmpresaP__BD84C302A3FACB58");

            entity.ToTable("EmpresaPoliza");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdAseguradoraNavigation).WithMany(p => p.EmpresaPolizas)
                .HasForeignKey(d => d.IdAseguradora)
                .HasConstraintName("fk_Aseguradora");

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.EmpresaPolizas)
                .HasForeignKey(d => d.IdPoliza)
                .HasConstraintName("fk_PolizaEmpresaPoliza");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.EmpresaPolizas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_UsuarioEmpresaPoliza");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1AF0B8307");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Estados)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("fk_PaisEstado");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__881A6AE0AD94571A");

            entity.ToTable("Movimiento");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdMovimientoTipoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdMovimientoTipo)
                .HasConstraintName("fk_MovimientoTipoMovimiento");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_UsuarioMovimiento");
        });

        modelBuilder.Entity<MovimientoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdMovimientoDetalle).HasName("PK__Movimien__91770702BBC2D908");

            entity.ToTable("MovimientoDetalle");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.MovimientoDetalles)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("fk_StatusMovimientoDetalle");
        });

        modelBuilder.Entity<MovimientoTipo>(entity =>
        {
            entity.HasKey(e => e.IdMovimientoTipo).HasName("PK__Movimien__BB71BBFED3358E7A");

            entity.ToTable("MovimientoTipo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__610059785DB8DA67");

            entity.ToTable("Municipio");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("fk_EstadoMunicipio");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Pais__FC850A7B36783FF3");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.IdPoliza).HasName("PK__Poliza__8E3943B340EC2C2A");

            entity.ToTable("Poliza");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPoliza)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSubPolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdSubPoliza)
                .HasConstraintName("fk_SubPolizaPoliza");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_UsuarioPoliza");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C3CDF3795");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__Status__B450643A6E6B567C");

            entity.ToTable("Status");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubPoliza>(entity =>
        {
            entity.HasKey(e => e.IdSubPoliza).HasName("PK__SubPoliz__91F56A9897155AE7");

            entity.ToTable("SubPoliza");

            entity.Property(e => e.IdSubPoliza).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF973A48C0E2");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.UserNombre, "UQ__Usuario__22ECD761D881E2A9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534ADC865DB").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserNombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__4BAC3F29");
        });

        modelBuilder.Entity<Vigencium>(entity =>
        {
            entity.HasKey(e => e.IdVigencia).HasName("PK__Vigencia__C8CFC7722491FF97");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.Vigencia)
                .HasForeignKey(d => d.IdPoliza)
                .HasConstraintName("fk_PolizaVigencia");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Vigencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("fk_UsuarioVigencia");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
