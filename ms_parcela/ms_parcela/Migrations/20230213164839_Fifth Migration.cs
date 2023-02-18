using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace msparcela.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("6f40a0ba-25e3-4751-9a03-f1128e6dab96"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("e4cacf06-c34f-47b2-8a34-96961de3b833"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("053c3c95-a01b-4215-8b2b-72533fd86cc2"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("676570e6-9b4b-44af-9970-4840ebc95307"));

            migrationBuilder.CreateTable(
                name: "DeoParceles",
                columns: table => new
                {
                    deoParceleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    redniBrojDelaParcele = table.Column<int>(type: "int", nullable: false),
                    povrsinaDelaParcele = table.Column<int>(type: "int", nullable: false),
                    brojKatastraskaOpstina = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    brojParcele = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeoParceles", x => x.deoParceleID);
                    table.ForeignKey(
                        name: "FK_DeoParceles_Parcelas_ParcelabrojKatastraskaOpstina_ParcelabrojParcele",
                        columns: x => new { x.brojKatastraskaOpstina, x.brojParcele },
                        principalTable: "Parcelas",
                        principalColumns: new[] { "brojKatastraskaOpstina", "brojParcele" });
                });

            /*migrationBuilder.InsertData(
                table: "DeoParceles",
                columns: new[] { "deoParceleID", "brojKatastraskaOpstina", "brojParcele", "povrsinaDelaParcele", "redniBrojDelaParcele" },
                values: new object[,]
                {
                    { new Guid("7cb99eda-bf52-4f8e-ad07-c9b7c05c7545"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 1, 200, 3 },
                    { new Guid("c70af8ce-56d9-404e-8233-57faf4fac26c"), new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"), 1, 200, 4 }
                });*/

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("454fdd3f-958d-455c-974a-0b2bfbbfbc1f"), "Cantavir" },
                    { new Guid("cf62424e-c45f-439a-95c0-1c0eeb1128af"), "Bajmok" }
                });

            migrationBuilder.InsertData(
                table: "Zonas",
                columns: new[] { "zonaID", "brojZona" },
                values: new object[,]
                {
                    { new Guid("64ef9a3b-bf2d-4999-be8c-857ab404efd1"), "zona4" },
                    { new Guid("872e5dbd-85b5-4b27-babb-65a532b9c5f0"), "zona3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeoParceles");

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("454fdd3f-958d-455c-974a-0b2bfbbfbc1f"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("cf62424e-c45f-439a-95c0-1c0eeb1128af"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("64ef9a3b-bf2d-4999-be8c-857ab404efd1"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("872e5dbd-85b5-4b27-babb-65a532b9c5f0"));

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("6f40a0ba-25e3-4751-9a03-f1128e6dab96"), "Bajmok" },
                    { new Guid("e4cacf06-c34f-47b2-8a34-96961de3b833"), "Cantavir" }
                });

            migrationBuilder.InsertData(
                table: "Zonas",
                columns: new[] { "zonaID", "brojZona" },
                values: new object[,]
                {
                    { new Guid("053c3c95-a01b-4215-8b2b-72533fd86cc2"), "zona4" },
                    { new Guid("676570e6-9b4b-44af-9970-4840ebc95307"), "zona3" }
                });
        }
    }
}
