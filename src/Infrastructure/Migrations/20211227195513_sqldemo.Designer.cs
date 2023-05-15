﻿// <auto-generated />
using ExampleApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExampleApi.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211227195513_sqldemo")]
    partial class sqldemo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExampleApi.Domain.Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("person");

                    b.HasData(
                        new
                        {
                            PersonId = 1,
                            Apellido = "apellidoTest",
                            Nombre = "nombreTest"
                        },
                        new
                        {
                            PersonId = 2,
                            Apellido = "apellidoTest2",
                            Nombre = "nombreTest2"
                        },
                        new
                        {
                            PersonId = 3,
                            Apellido = "apellidoTest3",
                            Nombre = "nombreTest3"
                        },
                        new
                        {
                            PersonId = 4,
                            Apellido = "apellidoTest4",
                            Nombre = "nombreTest4"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
