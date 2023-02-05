using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nadmetanjemicroserviceDAL.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etape",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VremePocetka = table.Column<TimeSpan>(type: "time", nullable: false),
                    VremeZavrsetka = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etape", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nadmetanja",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CenaPoHektaru = table.Column<double>(type: "float", nullable: false),
                    DuzinaZakupa = table.Column<int>(type: "int", nullable: false),
                    RedniBroj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EtapaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KrugNadmetanja = table.Column<int>(type: "int", nullable: false),
                    StatusDrugiKrug = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nadmetanja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nadmetanja_Etape_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "Etape",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nadmetanja_EtapaId",
                table: "Nadmetanja",
                column: "EtapaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nadmetanja");

            migrationBuilder.DropTable(
                name: "Etape");
        }
    }
}
