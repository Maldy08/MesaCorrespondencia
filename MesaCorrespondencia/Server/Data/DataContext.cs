using MesaCorrespondencia.Shared;
using Microsoft.EntityFrameworkCore;

namespace MesaCorrespondencia.Server.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<DeptosUe> DeptosUe { get; set; }
        public  DbSet<VsEmpleadosSisco> EmpleadosSisco { get; set; }
        public  DbSet<Usuario> Usuarios { get; set; }
        public  DbSet<VsUsuario> VsUsuarios { get; set; }
        public  DbSet<Oficio> Oficios { get; set; }
        public  DbSet<OficiosBitacora> OficiosBitacoras { get; set; }
        public  DbSet<OficiosEstatus> OficiosEstatuses { get; set; }
        public  DbSet<OficiosResponsable> OficiosResponsables { get; set; }
        public  DbSet<OficiosUsuext> OficiosUsuexts { get; set; }
        public  DbSet<OficiosXexpedir> OficiosXexpedirs { get; set; }
        public  DbSet<VwOficiosLista> VwOficiosListas { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("name=OracleConnection", b => b.UseOracleSQLCompatibility("11"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CEA");

            modelBuilder.Entity<DeptosUe>(entity =>
            {
                entity.HasKey(e => e.IdCea);

                entity.ToTable("DEPTOS_UE");

                entity.Property(e => e.IdCea)
                    .HasPrecision(3)
                    .HasColumnName("ID_CEA");

                entity.Property(e => e.Accion)
                    .HasPrecision(2)
                    .HasColumnName("ACCION");

                entity.Property(e => e.Accion2)
                    .HasPrecision(2)
                    .HasColumnName("ACCION2")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.AgrupaPoa)
                    .HasPrecision(3)
                    .HasColumnName("AGRUPA_POA");

                entity.Property(e => e.Agrupdir)
                    .HasPrecision(3)
                    .HasColumnName("AGRUPDIR")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.Ano)
                    .HasPrecision(4)
                    .HasColumnName("ANO")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.Cg)
                    .HasPrecision(1)
                    .HasColumnName("CG")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.EmpRespon)
                    .HasPrecision(4)
                    .HasColumnName("EMP_RESPON");

                entity.Property(e => e.Id)
                    .HasPrecision(3)
                    .HasColumnName("ID");

                entity.Property(e => e.IdCfg)
                    .HasPrecision(3)
                    .HasColumnName("ID_CFG")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.IdCp)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ID_CP")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.IdReporta)
                    .HasPrecision(3)
                    .HasColumnName("ID_REPORTA");

                entity.Property(e => e.IdShpoa)
                    .HasPrecision(3)
                    .HasColumnName("ID_SHPOA");

                entity.Property(e => e.Meta)
                    .HasPrecision(2)
                    .HasColumnName("META");

                entity.Property(e => e.Meta2)
                    .HasPrecision(2)
                    .HasColumnName("META2")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.Nivel)
                    .HasPrecision(1)
                    .HasColumnName("NIVEL");

                entity.Property(e => e.Oficial)
                    .HasPrecision(1)
                    .HasColumnName("OFICIAL");

                entity.Property(e => e.Prog)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("PROG");

                entity.Property(e => e.Prog2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("PROG2")
                    .HasDefaultValueSql("0     ");
            });
            modelBuilder.Entity<VsEmpleadosSisco>(entity =>
            {

                entity.HasNoKey();
                entity.ToView("VS_EMPLEADOS_SISCO");

                entity.Property(e => e.Activo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("ACTIVO");

                entity.Property(e => e.Empleado)
               .HasPrecision(4)
               .HasColumnName("EMPLEADO");

                entity.Property(e => e.Paterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PATERNO");

                entity.Property(e => e.Materno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MATERNO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.IdPue)
                    .HasPrecision(3)
                    .HasColumnName("ID_PUE");

                entity.Property(e => e.DescripcionPuesto)
                        .HasColumnName("DESCRIPCION_PUESTO");


                entity.Property(e => e.Deptoue)
                    .HasPrecision(4)
                    .HasColumnName("DEPTOUE")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.DescripcionDepto)
                        .HasColumnName("DESCRIPCION_DEPTO");

                entity.Property(e => e.Deptocomi)
                    .HasPrecision(4)
                    .HasColumnName("DEPTOCOMI")
                    .HasDefaultValueSql("0     ");

                entity.Property(e => e.NombreCompleto)
                     .HasColumnName("NOMBRE_COMPLETO");
            });
            modelBuilder.Entity<Oficio>(entity =>
            {
                entity.HasKey(e => new { e.Ejercicio, e.Folio, e.Eor })
                    .HasName("OFICIOS_PK");

                entity.ToTable("OFICIOS");

                entity.Property(e => e.Ejercicio)
                    .HasPrecision(4)
                    .HasColumnName("EJERCICIO");

                entity.Property(e => e.Folio)
                    .HasPrecision(6)
                    .HasColumnName("FOLIO");

                entity.Property(e => e.Eor)
                    .HasPrecision(1)
                    .HasColumnName("EOR");

                entity.Property(e => e.Depto)
                    .HasPrecision(2)
                    .HasColumnName("DEPTO");

                entity.Property(e => e.DeptoRespon)
                    .HasPrecision(2)
                    .HasColumnName("DEPTO_RESPON");

                entity.Property(e => e.DestCargo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEST_CARGO");

                entity.Property(e => e.DestDepen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEST_DEPEN");

                entity.Property(e => e.DestNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEST_NOMBRE");

                entity.Property(e => e.DestSiglas)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DEST_SIGLAS");

                entity.Property(e => e.Empqentrega)
                    .HasPrecision(4)
                    .HasColumnName("EMPQENTREGA");

                entity.Property(e => e.Estatus)
                    .HasPrecision(2)
                    .HasColumnName("ESTATUS");

                entity.Property(e => e.Fecha)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA");

                entity.Property(e => e.FechaAcuse)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ACUSE");

                entity.Property(e => e.FechaCaptura)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CAPTURA");

                entity.Property(e => e.FechaLimite)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_LIMITE");

                entity.Property(e => e.NoOficio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NO_OFICIO");

                entity.Property(e => e.Pdfpath)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PDFPATH");

                entity.Property(e => e.Relacionoficio)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RELACIONOFICIO");

                entity.Property(e => e.RemCargo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("REM_CARGO");

                entity.Property(e => e.RemDepen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("REM_DEPEN");

                entity.Property(e => e.RemNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("REM_NOMBRE");

                entity.Property(e => e.RemSiglas)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("REM_SIGLAS");

                entity.Property(e => e.Tema)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TEMA");

                entity.Property(e => e.Tipo)
                    .HasPrecision(1)
                    .HasColumnName("TIPO");
            });
            modelBuilder.Entity<OficiosBitacora>(entity =>
            {
                entity.HasKey(e => new { e.Ejercicio, e.Eor, e.Folio, e.FechaCaptura })
                    .HasName("OFICIOS_BITACORA_PK");

                entity.ToTable("OFICIOS_BITACORA");

                entity.Property(e => e.Ejercicio)
                    .HasPrecision(4)
                    .HasColumnName("EJERCICIO");

                entity.Property(e => e.Eor)
                    .HasPrecision(1)
                    .HasColumnName("EOR");

                entity.Property(e => e.Folio)
                    .HasPrecision(6)
                    .HasColumnName("FOLIO");

                entity.Property(e => e.FechaCaptura)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CAPTURA");

                entity.Property(e => e.Comentarios)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMENTARIOS");

                entity.Property(e => e.Estatus)
                    .HasPrecision(2)
                    .HasColumnName("ESTATUS");

                entity.Property(e => e.IdEmpleado)
                    .HasPrecision(4)
                    .HasColumnName("ID_EMPLEADO");
            });
            modelBuilder.Entity<OficiosEstatus>(entity =>
            {
                entity.HasKey(e => new { e.IdEstatus, e.Eor })
                    .HasName("OFICIOS_ESTATUS_PK");

                entity.ToTable("OFICIOS_ESTATUS");

                entity.Property(e => e.IdEstatus)
                    .HasPrecision(2)
                    .HasColumnName("ID_ESTATUS");

                entity.Property(e => e.Eor)
                    .HasPrecision(1)
                    .HasColumnName("EOR");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });
            modelBuilder.Entity<OficiosResponsable>(entity =>
            {
                entity.HasKey(e => new { e.Ejercicio, e.Folio, e.Eor })
                    .HasName("OFICIOS_RESPONSABLE_PK");

                entity.ToTable("OFICIOS_RESPONSABLE");

                entity.Property(e => e.Ejercicio)
                    .HasPrecision(4)
                    .HasColumnName("EJERCICIO");

                entity.Property(e => e.Folio)
                    .HasPrecision(6)
                    .HasColumnName("FOLIO");

                entity.Property(e => e.Eor)
                    .HasPrecision(1)
                    .HasColumnName("EOR");

                entity.Property(e => e.IdEmpleado)
                    .HasPrecision(4)
                    .HasColumnName("ID_EMPLEADO");

                entity.Property(e => e.Rol)
                    .HasPrecision(1)
                    .HasColumnName("ROL");
            });
            modelBuilder.Entity<OficiosUsuext>(entity =>
            {
                entity.HasKey(e => e.IdExterno)
                    .HasName("OFICIOS_USUEXT_PK");

                entity.ToTable("OFICIOS_USUEXT");

                entity.Property(e => e.IdExterno)
                    .HasPrecision(4)
                    .HasColumnName("ID_EXTERNO");

                entity.Property(e => e.Activo)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVO");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CARGO");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMPRESA");

                entity.Property(e => e.FechaCaptura)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CAPTURA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Siglas)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("SIGLAS");
            });
            modelBuilder.Entity<OficiosXexpedir>(entity =>
            {
                entity.HasKey(e => new { e.Ejercicio, e.Depto, e.NoOficio })
                    .HasName("OFICIOS_XEXPEDIR_PK");

                entity.ToTable("OFICIOS_XEXPEDIR");

                entity.Property(e => e.Ejercicio)
                    .HasPrecision(4)
                    .HasColumnName("EJERCICIO");

                entity.Property(e => e.Depto)
                    .HasPrecision(2)
                    .HasColumnName("DEPTO");

                entity.Property(e => e.NoOficio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NO_OFICIO");

                entity.Property(e => e.Estatus)
                    .HasPrecision(2)
                    .HasColumnName("ESTATUS");

                entity.Property(e => e.ExtCargo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXT_CARGO");

                entity.Property(e => e.ExtDepen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXT_DEPEN");

                entity.Property(e => e.ExtNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXT_NOMBRE");

                entity.Property(e => e.ExtSiglas)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("EXT_SIGLAS");

                entity.Property(e => e.Fecha)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA");

                entity.Property(e => e.FechaCaptura)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FECHA_CAPTURA");

                entity.Property(e => e.IdEmpleado)
                    .HasPrecision(4)
                    .HasColumnName("ID_EMPLEADO");

                entity.Property(e => e.IntCargo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("INT_CARGO");

                entity.Property(e => e.IntDepen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("INT_DEPEN");

                entity.Property(e => e.IntNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("INT_NOMBRE");

                entity.Property(e => e.IntSiglas)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("INT_SIGLAS");

                entity.Property(e => e.Tema)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TEMA");
            });
            modelBuilder.Entity<VsUsuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VS_USUARIOS");

                entity.Property(e => e.Activo)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVO");

                entity.Property(e => e.Activos)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVOS");

                entity.Property(e => e.ActivosNivel)
                    .HasPrecision(1)
                    .HasColumnName("ACTIVOS_NIVEL");

                entity.Property(e => e.Almacen)
                    .HasPrecision(1)
                    .HasColumnName("ALMACEN");

                entity.Property(e => e.AlmacenNivel)
                    .HasPrecision(1)
                    .HasColumnName("ALMACEN_NIVEL");

                entity.Property(e => e.Bd)
                    .HasPrecision(1)
                    .HasColumnName("BD");

                entity.Property(e => e.Caja)
                    .HasPrecision(1)
                    .HasColumnName("CAJA");

                entity.Property(e => e.CajaNivel)
                    .HasPrecision(1)
                    .HasColumnName("CAJA_NIVEL");

                entity.Property(e => e.Compras)
                    .HasPrecision(1)
                    .HasColumnName("COMPRAS");

                entity.Property(e => e.ComprasNivel)
                    .HasPrecision(1)
                    .HasColumnName("COMPRAS_NIVEL");

                entity.Property(e => e.Contabilidad)
                    .HasPrecision(1)
                    .HasColumnName("CONTABILIDAD");

                entity.Property(e => e.ContabilidadNivel)
                    .HasPrecision(1)
                    .HasColumnName("CONTABILIDAD_NIVEL");

                entity.Property(e => e.Depto)
                    .HasPrecision(4)
                    .HasColumnName("DEPTO");

                entity.Property(e => e.DeptoDescripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEPTO_DESCRIPCION");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.IdPue)
                    .HasPrecision(3)
                    .HasColumnName("ID_PUE");

                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LOGIN");

                entity.Property(e => e.NoEmpleado)
                    .HasPrecision(4)
                    .HasColumnName("NO_EMPLEADO");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(152)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_COMPLETO");

                entity.Property(e => e.Nominas)
                    .HasPrecision(1)
                    .HasColumnName("NOMINAS");

                entity.Property(e => e.NominasNivel)
                    .HasPrecision(1)
                    .HasColumnName("NOMINAS_NIVEL");

                entity.Property(e => e.Pass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASS");

                entity.Property(e => e.Polnom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("POLNOM");

                entity.Property(e => e.Presupuestos)
                    .HasPrecision(1)
                    .HasColumnName("PRESUPUESTOS");

                entity.Property(e => e.PresupuestosNivel)
                    .HasPrecision(1)
                    .HasColumnName("PRESUPUESTOS_NIVEL");

                entity.Property(e => e.Usuario)
                    .HasPrecision(2)
                    .HasColumnName("USUARIO");

                entity.Property(e => e.Vales)
                    .HasPrecision(1)
                    .HasColumnName("VALES");

                entity.Property(e => e.ValesNivel)
                    .HasPrecision(1)
                    .HasColumnName("VALES_NIVEL");

                entity.Property(e => e.Viaticos)
                    .HasPrecision(1)
                    .HasColumnName("VIATICOS");

                entity.Property(e => e.ViaticosNivel)
                    .HasPrecision(1)
                    .HasColumnName("VIATICOS_NIVEL");

                entity.Property(e => e.Oficios)
                     .HasPrecision(1)
                     .HasColumnName("OFICIOS");

                entity.Property(e => e.OficiosNivel)
                     .HasPrecision(1)
                     .HasColumnName("OFICIOS_NIVEL");
            });
            modelBuilder.Entity<VwOficiosLista>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_OFICIOS_LISTA_MC");

                entity.Property(e => e.Depto)
                    .HasPrecision(2)
                    .HasColumnName("DEPTO");


                entity.Property(e => e.DeptoRespon)
                    .HasPrecision(2)
                    .HasColumnName("DEPTO_RESPON");

                entity.Property(e => e.DestCargo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEST_CARGO");

                entity.Property(e => e.DestDepen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEST_DEPEN");

                entity.Property(e => e.DestNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DEST_NOMBRE");

                entity.Property(e => e.DestSiglas)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DEST_SIGLAS");

                entity.Property(e => e.Ejercicio)
                    .HasPrecision(4)
                    .HasColumnName("EJERCICIO");

                entity.Property(e => e.Empqentrega)
                    .HasPrecision(4)
                    .HasColumnName("EMPQENTREGA");

                entity.Property(e => e.Eor)
                    .HasPrecision(1)
                    .HasColumnName("EOR");

                entity.Property(e => e.Estatus)
                    .HasPrecision(2)
                    .HasColumnName("ESTATUS");

                entity.Property(e => e.Fecha)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA");

                entity.Property(e => e.FechaAcuse)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ACUSE");

                entity.Property(e => e.FechaCaptura)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CAPTURA");

                entity.Property(e => e.FechaLimite)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_LIMITE");

                entity.Property(e => e.Folio)
                    .HasPrecision(6)
                    .HasColumnName("FOLIO");

                entity.Property(e => e.IdEmpleado)
                    .HasPrecision(4)
                    .HasColumnName("ID_EMPLEADO");

                entity.Property(e => e.NoOficio)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NO_OFICIO");

                entity.Property(e => e.Pdfpath)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PDFPATH");

                entity.Property(e => e.Relacionoficio)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RELACIONOFICIO");

                entity.Property(e => e.RemCargo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("REM_CARGO");

                entity.Property(e => e.RemDepen)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("REM_DEPEN");

                entity.Property(e => e.RemNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("REM_NOMBRE");

                entity.Property(e => e.RemSiglas)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("REM_SIGLAS");

                entity.Property(e => e.Rol)
                    .HasPrecision(1)
                    .HasColumnName("ROL");

                entity.Property(e => e.Tema)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TEMA");

                entity.Property(e => e.Tipo)
                    .HasPrecision(1)
                    .HasColumnName("TIPO");
            });

           
        }
    }
}
