using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mszalba.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UgovorOZakupus",
                columns: table => new
                {
                    idUgovor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipGarancije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rokDospeca = table.Column<DateTime>(type: "date", nullable: false),
                    zavodniBroj = table.Column<int>(type: "int", nullable: false),
                    datumZavodjanja = table.Column<DateTime>(type: "date", nullable: false),
                    rokZaVracanjeZemljista = table.Column<DateTime>(type: "date", nullable: false),
                    mestoPotpisivanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumPotpisa = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UgovorOZakupus", x => x.idUgovor);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UgovorOZakupus");
        }
    }
}
