using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrijavaJnService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrijavaJn",
                columns: table => new
                {
                    PrijavaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojPrijave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MestoPrijave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SatPrijave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZatvorenaPonuda = table.Column<bool>(type: "bit", nullable: false),
                    DokFizickaLica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DokPravnaLica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaJn", x => x.PrijavaId);
                });

            migrationBuilder.InsertData(
                table: "PrijavaJn",
                columns: new[] { "PrijavaId", "BrojPrijave", "DatumPrijave", "DokFizickaLica", "DokPravnaLica", "KupacId", "MestoPrijave", "SatPrijave", "ZatvorenaPonuda" },
                values: new object[,]
                {
                    { new Guid("3040da81-b4b5-47bd-a47c-f1474341f162"), "B22", new DateTime(2023, 2, 16, 21, 46, 31, 86, DateTimeKind.Local).AddTicks(9688), "prijava za fizicka lica", "prijava za pravna lica obrazac 4", null, "Mesto 1", "21:46", true },
                    { new Guid("a370bc58-2cb2-4d8d-9cfb-b444841aeb80"), "B255", new DateTime(2023, 2, 16, 21, 46, 31, 86, DateTimeKind.Local).AddTicks(9840), "prijava, odjava", "prijava za pravna lica", null, "Mesto 2", "21:46", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrijavaJn");
        }
    }
}
