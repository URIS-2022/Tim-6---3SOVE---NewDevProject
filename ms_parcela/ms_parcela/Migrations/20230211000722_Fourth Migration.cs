using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace msparcela.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeoParceles",
                keyColumn: "deoParceleID",
                keyValue: new Guid("a766a253-e09c-4102-8e29-a3d8c12581e0"));

            migrationBuilder.DeleteData(
                table: "DeoParceles",
                keyColumn: "deoParceleID",
                keyValue: new Guid("e70aedce-f009-48b5-ab9b-85878fff91fe"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("7fd220ae-57d7-48e0-a02a-a43bf1d5e682"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("e9092360-71c3-4dc5-8e8c-85d97c78e667"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("c2e3ebac-e1b6-4f27-9c8d-391db9abbcf5"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("f5b61a2d-d69a-476f-881e-c8b44ec9bcb9"));

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    brojParcele = table.Column<int>(type: "int", nullable: false),
                    brojKatastraskaOpstina = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    povrsina = table.Column<int>(type: "int", nullable: false),
                    kultura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    obradivost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    klasa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    oblikSvojine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    odvodnjavanje = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => new { x.brojKatastraskaOpstina, x.brojParcele });
                    table.ForeignKey(
                    name: "FK_Parcela_KatastarskaOpstina",
                    column: x => x.brojKatastraskaOpstina,
                    principalTable: "KatastarskaOpstinas",
                    principalColumn: "brojKatastarskeOpstine",
                    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeoParceles",
                columns: new[] { "deoParceleID", "povrsinaDelaParcele", "redniBrojDelaParcele" },
                values: new object[,]
                {
                    { new Guid("14d41623-6793-47d0-865d-f0780bd72bc1"), 200, 4 },
                    { new Guid("8d2314af-5e3f-4dcd-ad7a-5095e6c39ffa"), 200, 3 }
                });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("22ae87fe-847f-4f35-bbe9-18bab36f2bda"), "Cantavir" },
                    { new Guid("faea4c2a-5338-4be2-a5be-1c63bcaf232f"), "Bajmok" }
                });

            migrationBuilder.InsertData(
                table: "Zonas",
                columns: new[] { "zonaID", "brojZona" },
                values: new object[,]
                {
                    { new Guid("8f522aa3-70c2-4d67-8f3d-e23684654650"), "zona3" },
                    { new Guid("deb4cc83-aee1-4eac-a528-deb1784b80ea"), "zona4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DeleteData(
                table: "DeoParceles",
                keyColumn: "deoParceleID",
                keyValue: new Guid("14d41623-6793-47d0-865d-f0780bd72bc1"));

            migrationBuilder.DeleteData(
                table: "DeoParceles",
                keyColumn: "deoParceleID",
                keyValue: new Guid("8d2314af-5e3f-4dcd-ad7a-5095e6c39ffa"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("22ae87fe-847f-4f35-bbe9-18bab36f2bda"));

            migrationBuilder.DeleteData(
                table: "KatastarskaOpstinas",
                keyColumn: "brojKatastarskeOpstine",
                keyValue: new Guid("faea4c2a-5338-4be2-a5be-1c63bcaf232f"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("8f522aa3-70c2-4d67-8f3d-e23684654650"));

            migrationBuilder.DeleteData(
                table: "Zonas",
                keyColumn: "zonaID",
                keyValue: new Guid("deb4cc83-aee1-4eac-a528-deb1784b80ea"));

            migrationBuilder.InsertData(
                table: "DeoParceles",
                columns: new[] { "deoParceleID", "povrsinaDelaParcele", "redniBrojDelaParcele" },
                values: new object[,]
                {
                    { new Guid("a766a253-e09c-4102-8e29-a3d8c12581e0"), 200, 3 },
                    { new Guid("e70aedce-f009-48b5-ab9b-85878fff91fe"), 200, 4 }
                });

            migrationBuilder.InsertData(
                table: "KatastarskaOpstinas",
                columns: new[] { "brojKatastarskeOpstine", "nazivKatastarskeOpstine" },
                values: new object[,]
                {
                    { new Guid("7fd220ae-57d7-48e0-a02a-a43bf1d5e682"), "Cantavir" },
                    { new Guid("e9092360-71c3-4dc5-8e8c-85d97c78e667"), "Bajmok" }
                });

            migrationBuilder.InsertData(
                table: "Zonas",
                columns: new[] { "zonaID", "brojZona" },
                values: new object[,]
                {
                    { new Guid("c2e3ebac-e1b6-4f27-9c8d-391db9abbcf5"), "zona4" },
                    { new Guid("f5b61a2d-d69a-476f-881e-c8b44ec9bcb9"), "zona3" }
                });
        }
    }
}
