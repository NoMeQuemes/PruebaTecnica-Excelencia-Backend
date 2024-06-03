﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prueba_Tecnica_Api.Data;

#nullable disable

namespace Prueba_Tecnica_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240603201501_UltimaUltimaActualizacion")]
    partial class UltimaUltimaActualizacion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Cita", b =>
                {
                    b.Property<int>("idCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCita"));

                    b.Property<DateTime?>("fechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaCita")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("fechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("idDoctor")
                        .HasColumnType("int");

                    b.Property<int>("idEstado")
                        .HasColumnType("int");

                    b.Property<int>("idPaciente")
                        .HasColumnType("int");

                    b.Property<string>("motivo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idCita");

                    b.HasIndex("idDoctor");

                    b.HasIndex("idEstado");

                    b.HasIndex("idPaciente");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Doctor", b =>
                {
                    b.Property<int>("idDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idDoctor"));

                    b.Property<string>("apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("fechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("fechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("idEspecialidad")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numCelular")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idDoctor");

                    b.HasIndex("idEspecialidad");

                    b.ToTable("Doctores");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Especialidad", b =>
                {
                    b.Property<int>("idEspecialidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEspecialidad"));

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idEspecialidad");

                    b.ToTable("Especialidades");

                    b.HasData(
                        new
                        {
                            idEspecialidad = 1,
                            descripcion = "Soy un clínico",
                            nombre = "Clínico"
                        },
                        new
                        {
                            idEspecialidad = 2,
                            descripcion = "Soy un oftalmólogo",
                            nombre = "Oftalmólogo"
                        },
                        new
                        {
                            idEspecialidad = 3,
                            descripcion = "Soy un cirujano",
                            nombre = "Cirujano"
                        });
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Estado", b =>
                {
                    b.Property<int>("idEstado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEstado"));

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idEstado");

                    b.ToTable("Estados");

                    b.HasData(
                        new
                        {
                            idEstado = 1,
                            nombre = "Completa"
                        },
                        new
                        {
                            idEstado = 2,
                            nombre = "Programada"
                        },
                        new
                        {
                            idEstado = 3,
                            nombre = "Cancelada"
                        });
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Genero", b =>
                {
                    b.Property<int>("idGenero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idGenero"));

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idGenero");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            idGenero = 1,
                            nombre = "Masculino"
                        },
                        new
                        {
                            idGenero = 2,
                            nombre = "Femenino"
                        },
                        new
                        {
                            idGenero = 3,
                            nombre = "Prefiero no decirlo"
                        });
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Paciente", b =>
                {
                    b.Property<int>("idPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPaciente"));

                    b.Property<string>("apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("fechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("fechaEliminacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("idGenero")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numCelular")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idPaciente");

                    b.HasIndex("idGenero");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Permiso", b =>
                {
                    b.Property<int>("idPermiso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPermiso"));

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idPermiso");

                    b.ToTable("Permisos");

                    b.HasData(
                        new
                        {
                            idPermiso = 1,
                            tipo = "Crear"
                        },
                        new
                        {
                            idPermiso = 2,
                            tipo = "Leer"
                        },
                        new
                        {
                            idPermiso = 3,
                            tipo = "Eliminar"
                        },
                        new
                        {
                            idPermiso = 4,
                            tipo = "Actualizar"
                        });
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.RefreshToken", b =>
                {
                    b.Property<int>("idRefreshToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idRefreshToken"));

                    b.Property<bool>("esActivo")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit")
                        .HasComputedColumnSql("IIF(fechaExpiracion < GETDATE(), CONVERT(BIT, 0), CONVERT(BIT, 1))");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaExpiracion")
                        .HasColumnType("datetime2");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.Property<string>("refreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idRefreshToken");

                    b.HasIndex("idUsuario");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Roles", b =>
                {
                    b.Property<int>("idRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idRol"));

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPermiso")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idRol");

                    b.HasIndex("idPermiso");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            idRol = 1,
                            descripcion = "Soy administrador",
                            idPermiso = 1,
                            nombre = "Administrador"
                        },
                        new
                        {
                            idRol = 2,
                            descripcion = "Soy médico",
                            idPermiso = 2,
                            nombre = "Medico"
                        },
                        new
                        {
                            idRol = 3,
                            descripcion = "Soy enfermero",
                            idPermiso = 2,
                            nombre = "Enfermero"
                        });
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Usuario", b =>
                {
                    b.Property<int>("idUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUsuario"));

                    b.Property<DateTime?>("estadoUsuario")
                        .HasColumnType("datetime2");

                    b.Property<int?>("idRoles")
                        .HasColumnType("int");

                    b.Property<string>("nombreUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUsuario");

                    b.HasIndex("idRoles");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            idUsuario = 1,
                            idRoles = 1,
                            nombreUsuario = "Fernando",
                            password = "123"
                        },
                        new
                        {
                            idUsuario = 2,
                            idRoles = 2,
                            nombreUsuario = "Hernan",
                            password = "123"
                        });
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Cita", b =>
                {
                    b.HasOne("Prueba_Tecnica_Api.Models.Doctor", "Doctores")
                        .WithMany("Citas")
                        .HasForeignKey("idDoctor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prueba_Tecnica_Api.Models.Estado", "Estados")
                        .WithMany("Citas")
                        .HasForeignKey("idEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prueba_Tecnica_Api.Models.Paciente", "Pacientes")
                        .WithMany("Citas")
                        .HasForeignKey("idPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctores");

                    b.Navigation("Estados");

                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Doctor", b =>
                {
                    b.HasOne("Prueba_Tecnica_Api.Models.Especialidad", "Especialidades")
                        .WithMany("Doctores")
                        .HasForeignKey("idEspecialidad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidades");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Paciente", b =>
                {
                    b.HasOne("Prueba_Tecnica_Api.Models.Genero", "Generos")
                        .WithMany("Pacientes")
                        .HasForeignKey("idGenero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Generos");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.RefreshToken", b =>
                {
                    b.HasOne("Prueba_Tecnica_Api.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Roles", b =>
                {
                    b.HasOne("Prueba_Tecnica_Api.Models.Permiso", "Permiso")
                        .WithMany()
                        .HasForeignKey("idPermiso");

                    b.Navigation("Permiso");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Usuario", b =>
                {
                    b.HasOne("Prueba_Tecnica_Api.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("idRoles");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Doctor", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Especialidad", b =>
                {
                    b.Navigation("Doctores");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Estado", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Genero", b =>
                {
                    b.Navigation("Pacientes");
                });

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Paciente", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
