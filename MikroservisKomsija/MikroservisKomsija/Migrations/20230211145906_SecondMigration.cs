using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikroservisKomsija.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Koms",
                columns: table => new
                {
                    IDKomsije = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeKomisije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ovlascenje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OznakaKomisije = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koms", x => x.IDKomsije);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Koms");
        }
    }
}
