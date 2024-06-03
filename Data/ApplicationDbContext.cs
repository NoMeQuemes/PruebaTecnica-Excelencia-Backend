using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Api;
using Prueba_Tecnica_Api.Models;

namespace Prueba_Tecnica_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso()
                {
                    idPermiso = 1,
                    tipo = "Crear"
                },
                new Permiso()
                {
                    idPermiso = 2,
                    tipo = "Leer"
                },
                new Permiso()
                {
                    idPermiso = 3,
                    tipo = "Eliminar"
                },
                new Permiso()
                {
                    idPermiso = 4,
                    tipo = "Actualizar"
                }
                );

            modelBuilder.Entity<Roles>().HasData(
                new Roles()
                {
                    idRol = 1,
                    nombre = "Administrador",
                    descripcion = "Soy administrador",
                    idPermiso = 1
                },
                new Roles()
                {
                    idRol = 2,
                    nombre = "Medico",
                    descripcion = "Soy médico",
                    idPermiso = 2
                },
                new Roles()
                {
                    idRol = 3,
                    nombre = "Enfermero",
                    descripcion = "Soy enfermero",
                    idPermiso = 2
                }
                );

            modelBuilder.Entity<Especialidad>().HasData(
                new Especialidad()
                {
                    idEspecialidad = 1,
                    nombre = "Clínico",
                    descripcion = "Soy un clínico"
                },
                new Especialidad()
                {
                    idEspecialidad = 2,
                    nombre = "Oftalmólogo",
                    descripcion = "Soy un oftalmólogo"
                },
                new Especialidad()
                {
                    idEspecialidad = 3,
                    nombre = "Cirujano",
                    descripcion = "Soy un cirujano"
                }
                );
            modelBuilder.Entity<Estado>().HasData(
                new Estado()
                {
                    idEstado = 1,
                    nombre = "Completa"
                },
                new Estado()
                {
                    idEstado = 2,
                    nombre = "Programada"
                },
                new Estado()
                {
                    idEstado = 3,
                    nombre = "Cancelada"
                }
                );
            modelBuilder.Entity<Genero>().HasData(
                new Genero()
                {
                    idGenero = 1,
                    nombre = "Masculino"
                },
                new Genero()
                {
                    idGenero = 2,
                    nombre = "Femenino"
                },
                new Genero()
                {
                    idGenero = 3,
                    nombre = "Prefiero no decirlo"
                }
                );
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    idUsuario = 1,
                    nombreUsuario = "Fernando",
                    password = "123",
                    idRoles = 1,
                    estadoUsuario = null
                },
                new Usuario()
                {
                    idUsuario = 2,
                    nombreUsuario = "Hernan",
                    password = "123",
                    idRoles = 2,
                    estadoUsuario = null
                }
                );

            //Estas lineas de código agregan el valor a la columna computada "esActivo"

            modelBuilder.Entity<RefreshToken>()
                .Property(o => o.esActivo)
                .HasComputedColumnSql("IIF(fechaExpiracion < GETDATE(), CONVERT(BIT, 0), CONVERT(BIT, 1))");

            // Configuración de la relación entre Doctor y Especialidad
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Especialidades)
                .WithMany(e => e.Doctores)
                .HasForeignKey(d => d.idEspecialidad);

            // Configuración de la relación entre Paciente y Genero
            modelBuilder.Entity<Paciente>()
                .HasOne(d => d.Generos)
                .WithMany(e => e.Pacientes)
                .HasForeignKey(d => d.idGenero);

            // Configuración de la relación entre Cita y Paciente
            modelBuilder.Entity<Cita>()
                .HasOne(d => d.Pacientes)
                .WithMany(e => e.Citas)
                .HasForeignKey(d => d.idPaciente);

            // Configuración de la relación entre Cita y Doctor
            modelBuilder.Entity<Cita>()
                .HasOne(d => d.Doctores)
                .WithMany(e => e.Citas)
                .HasForeignKey(d => d.idDoctor);

            // Configuración de la relación entre Cita y Estado
            modelBuilder.Entity<Cita>()
                .HasOne(d => d.Estados)
                .WithMany(e => e.Citas)
                .HasForeignKey(d => d.idEstado);



            base.OnModelCreating(modelBuilder);
        }
    }
}
