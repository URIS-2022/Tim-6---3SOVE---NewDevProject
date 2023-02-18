using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace msparcela.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KatastarskaOpstinas",
                columns: table => new
                {
                    brojKatastarskeOpstine = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazivKatastarskeOpstine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatastarskaOpstinas", x => x.brojKatastarskeOpstine);
                });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("8384f173-3e39-498d-b546-74fa71c427d4"), "Cantavir" },
                    { new Guid("f30ff623-1d7c-4d54-a119-aa21417db20f"), "Bajmok" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KatastarskaOpstinas");
        }
    }
}
