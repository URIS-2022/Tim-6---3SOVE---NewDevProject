using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DokumentMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dokument",
                columns: table => new
                {
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sablon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumDonosenjaDokumenta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokument", x => x.DokumentId);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    OglasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrNadmetanjaJmbg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrNadmetanjaMaticniBrojPreduzeca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zalbaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => new { x.DokumentId, x.OglasId });
                    table.ForeignKey(
                        name: "FK_Oglas_Dokument_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokument",
                        principalColumn: "DokumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PredlogPlanaProjekta",
                columns: table => new
                {
                    PredlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZavodniBr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumPredlog = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredlogPlanaProjekta", x => new { x.DokumentId, x.PredlogId });
                    table.ForeignKey(
                        name: "FK_PredlogPlanaProjekta_Dokument_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokument",
                        principalColumn: "DokumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResenjeStrucnaKomisije",
                columns: table => new
                {
                    ResenjeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DokumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Zavodnibr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumResenje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImeClanaKomisije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrezClanaKomisije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PredsednikKomisije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResenjeStrucnaKomisije", x => new { x.DokumentId, x.ResenjeId });
                    table.ForeignKey(
                        name: "FK_ResenjeStrucnaKomisije_Dokument_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokument",
                        principalColumn: "DokumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dokument",
                columns: new[] { "DokumentId", "DatumDonosenjaDokumenta", "Sablon" },
                values: new object[,]
                {
                    { new Guid("418af1d2-483f-4461-8d82-31b257527a4f"), new DateTime(2013, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "sablondokument9" },
                    { new Guid("9a2d9ea6-d264-494a-88f8-51808a1c3196"), new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "sablondokument3" },
                    { new Guid("9e2474b3-caf5-4123-944d-11b4ab186b6b"), new DateTime(2022, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "sablondokument1" },
                    { new Guid("c8e97d45-4bfc-4dca-ba07-20d1e93049d6"), new DateTime(2022, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "sablondokument9" },
                    { new Guid("d762b24f-2730-427f-9789-3d840a5f7e39"), new DateTime(2018, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "sablondokument4" },
                    { new Guid("e4be32f2-da5e-47d5-9fbf-b860eb1d79b3"), new DateTime(2022, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "sablondokument1" }
                });

            migrationBuilder.InsertData(
                table: "Oglas",
                columns: new[] { "DokumentId", "OglasId", "BrNadmetanjaJmbg", "BrNadmetanjaMaticniBrojPreduzeca", "zalbaID" },
                values: new object[,]
                {
                    { new Guid("9a2d9ea6-d264-494a-88f8-51808a1c3196"), new Guid("011c616c-14db-4bc3-a9c9-c2f642b22f73"), "2906000855008", "24905678", new Guid("946d1fee-3863-4fb5-b76b-1970fbb4894e") },
                    { new Guid("9e2474b3-caf5-4123-944d-11b4ab186b6b"), new Guid("9151a095-2890-49c8-a3ac-f5813b92ffac"), "2906000855002", "23905678", new Guid("7d5213e5-f1bb-4226-b034-ae061c672780") }
                });

            migrationBuilder.InsertData(
                table: "PredlogPlanaProjekta",
                columns: new[] { "DokumentId", "PredlogId", "DatumPredlog", "ZavodniBr" },
                values: new object[,]
                {
                    { new Guid("d762b24f-2730-427f-9789-3d840a5f7e39"), new Guid("b708754f-b5e9-481b-8898-ca3682107e9c"), new DateTime(2023, 2, 18, 18, 2, 50, 619, DateTimeKind.Local).AddTicks(6671), "PSPG-5/2022" },
                    { new Guid("e4be32f2-da5e-47d5-9fbf-b860eb1d79b3"), new Guid("4ff44338-8d0b-415a-8b44-dfce3d4c311d"), new DateTime(2023, 2, 18, 18, 2, 50, 619, DateTimeKind.Local).AddTicks(6655), "PSPG-1/2022" }
                });

            migrationBuilder.InsertData(
                table: "ResenjeStrucnaKomisije",
                columns: new[] { "DokumentId", "ResenjeId", "DatumResenje", "ImeClanaKomisije", "PredsednikKomisije", "PrezClanaKomisije", "Zavodnibr" },
                values: new object[,]
                {
                    { new Guid("418af1d2-483f-4461-8d82-31b257527a4f"), new Guid("e3eab479-b0aa-4161-bbc9-d2281f43f332"), new DateTime(2023, 2, 18, 18, 2, 50, 619, DateTimeKind.Local).AddTicks(6724), "Luka", "PredDraganDraganovic", "Markovic", "PSPG-9/2022" },
                    { new Guid("c8e97d45-4bfc-4dca-ba07-20d1e93049d6"), new Guid("22b74319-edb5-4945-b792-76d0b1b81d88"), new DateTime(2023, 2, 18, 18, 2, 50, 619, DateTimeKind.Local).AddTicks(6711), "Marko", "PredPetarPetrovic", "Markovic", "PSPG-2/2022" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oglas_DokumentId",
                table: "Oglas",
                column: "DokumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PredlogPlanaProjekta_DokumentId",
                table: "PredlogPlanaProjekta",
                column: "DokumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResenjeStrucnaKomisije_DokumentId",
                table: "ResenjeStrucnaKomisije",
                column: "DokumentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "PredlogPlanaProjekta");

            migrationBuilder.DropTable(
                name: "ResenjeStrucnaKomisije");

            migrationBuilder.DropTable(
                name: "Dokument");
        }
    }
}
