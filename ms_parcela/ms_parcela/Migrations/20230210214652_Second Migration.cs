using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace msparcela.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("8384f173-3e39-498d-b546-74fa71c427d4"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("f30ff623-1d7c-4d54-a119-aa21417db20f"));

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    zonaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    brojZona = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.zonaID);
                });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("0175bfdf-65cb-4d91-8be2-6e6d2fdbfa29"), "Cantavir" },
                    { new Guid("193ffe57-5126-41a7-985b-866892f6488d"), "Bajmok" }
                });

            migrationBuilder.InsertData(
                table: "Zonas",
                columns: new[] { "zonaID", "brojZona" },
                values: new object[,]
                {
                    { new Guid("5db1f103-8139-4a92-a8e9-25907921298c"), "zona3" },
                    { new Guid("a7adccb9-aed1-4f53-9817-cfc781f1598c"), "zona4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("0175bfdf-65cb-4d91-8be2-6e6d2fdbfa29"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("193ffe57-5126-41a7-985b-866892f6488d"));

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("8384f173-3e39-498d-b546-74fa71c427d4"), "Cantavir" },
                    { new Guid("f30ff623-1d7c-4d54-a119-aa21417db20f"), "Bajmok" }
                });
        }
    }
}
