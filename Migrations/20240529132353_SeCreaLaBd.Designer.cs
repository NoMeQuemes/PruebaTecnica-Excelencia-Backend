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
    [Migration("20240529132353_SeCreaLaBd")]
    partial class SeCreaLaBd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Permiso", b =>
                {
                    b.Property<int>("idPermiso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPermiso"));

                    b.Property<string>("tipo")
                        .IsRequired()
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

            modelBuilder.Entity("Prueba_Tecnica_Api.Models.Roles", b =>
                {
                    b.Property<int>("idRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idRol"));

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPermiso")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUsuario");

                    b.HasIndex("idRoles");

                    b.ToTable("Usuarios");
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
#pragma warning restore 612, 618
        }
    }
}