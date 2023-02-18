﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ms_zalba.Database;

#nullable disable

namespace mszalba.Migrations
{
    [DbContext(typeof(AuctionLandAPIContext))]
    [Migration("20230211213613_Third Migration")]
    partial class ThirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ms_zalba.Models.TipZalbeModel.TipZalbe", b =>
                {
                    b.Property<Guid>("idTipZalbe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("nazivTipaZalbe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idTipZalbe");

                    b.ToTable("TipZalbes");
                });

            modelBuilder.Entity("ms_zalba.Models.UgovorOZakupuModel.UgovorOZakupu", b =>
                {
                    b.Property<Guid>("idUgovor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("datumPotpisa")
                        .HasColumnType("date");

                    b.Property<DateTime>("datumZavodjanja")
                        .HasColumnType("date");

                    b.Property<string>("mestoPotpisivanja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("rokDospeca")
                        .HasColumnType("date");

                    b.Property<DateTime>("rokZaVracanjeZemljista")
                        .HasColumnType("date");

                    b.Property<string>("tipGarancije")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("zavodniBroj")
                        .HasColumnType("int");

                    b.HasKey("idUgovor");

                    b.ToTable("UgovorOZakupus");
                });

            modelBuilder.Entity("ms_zalba.Models.ZalbaModel.Zalba", b =>
                {
                    b.Property<Guid>("zalbaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("brojNadmetanja")
                        .HasColumnType("int");

                    b.Property<string>("brojResenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("datumPodnosenjaZalbe")
                        .HasColumnType("date");

                    b.Property<DateTime>("datumResenja")
                        .HasColumnType("date");

                    b.Property<string>("obrazlozenje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("podnosilacZalbe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("radnjaNaOsnovuZalbe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("razlogZalbe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusZalbe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("tipZalbe")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("zalbaID");

                    b.ToTable("Zalbas");
                });
#pragma warning restore 612, 618
        }
    }
}