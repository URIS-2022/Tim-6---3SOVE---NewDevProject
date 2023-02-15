﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MikroservisPrijavaNaLicitaciju.Data;

#nullable disable

namespace MikroservisPrijavaNaLicitaciju.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MikroservisPrijavaNaLicitaciju.Model.PrijavaNaLicitaciju", b =>
                {
                    b.Property<Guid>("IDPlic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DatumPrijave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IznosDepozita")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipPrijave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDPlic");

                    b.ToTable("PLics");
                });
#pragma warning restore 612, 618
        }
    }
}