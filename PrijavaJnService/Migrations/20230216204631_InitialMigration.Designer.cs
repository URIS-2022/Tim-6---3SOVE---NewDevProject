﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrijavaJnService.Entities.DataContext;

#nullable disable

namespace PrijavaJnService.Migrations
{
    [DbContext(typeof(PrijavaJnContext))]
    [Migration("20230216204631_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrijavaJnService.Entities.PrijavaJn", b =>
                {
                    b.Property<Guid>("PrijavaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojPrijave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumPrijave")
                        .HasColumnType("datetime2");

                    b.Property<string>("DokFizickaLica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DokPravnaLica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("KupacId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MestoPrijave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SatPrijave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ZatvorenaPonuda")
                        .HasColumnType("bit");

                    b.HasKey("PrijavaId");

                    b.ToTable("PrijavaJn");

                    b.HasData(
                        new
                        {
                            PrijavaId = new Guid("3040da81-b4b5-47bd-a47c-f1474341f162"),
                            BrojPrijave = "B22",
                            DatumPrijave = new DateTime(2023, 2, 16, 21, 46, 31, 86, DateTimeKind.Local).AddTicks(9688),
                            DokFizickaLica = "prijava za fizicka lica",
                            DokPravnaLica = "prijava za pravna lica obrazac 4",
                            MestoPrijave = "Mesto 1",
                            SatPrijave = "21:46",
                            ZatvorenaPonuda = true
                        },
                        new
                        {
                            PrijavaId = new Guid("a370bc58-2cb2-4d8d-9cfb-b444841aeb80"),
                            BrojPrijave = "B255",
                            DatumPrijave = new DateTime(2023, 2, 16, 21, 46, 31, 86, DateTimeKind.Local).AddTicks(9840),
                            DokFizickaLica = "prijava, odjava",
                            DokPravnaLica = "prijava za pravna lica",
                            MestoPrijave = "Mesto 2",
                            SatPrijave = "21:46",
                            ZatvorenaPonuda = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}