using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikroservisPrijavaNaLicitaciju.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PLics",
                columns: table => new
                {
                    IDPlic = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumPrijave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipPrijave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IznosDepozita = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLics", x => x.IDPlic);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLics");
        }
    }
}
