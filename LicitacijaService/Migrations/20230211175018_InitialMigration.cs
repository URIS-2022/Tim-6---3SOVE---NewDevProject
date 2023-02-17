using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LicitacijaService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramEntitet",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaksimalnaPovrsina = table.Column<int>(type: "int", nullable: false),
                    KrugLicitacije = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEntitet", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Licitacija",
                columns: table => new
                {
                    LicitacijaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrojLicitacije = table.Column<int>(type: "int", nullable: false),
                    GodinaLicitacije = table.Column<int>(type: "int", nullable: false),
                    OgranicenjeLicitacije = table.Column<int>(type: "int", nullable: false),
                    RokLicitacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorakCeneLicitacije = table.Column<int>(type: "int", nullable: false),
                    ProgramEntitetProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacija", x => x.LicitacijaId);
                    table.ForeignKey(
                        name: "FK_Licitacija_ProgramEntitet_ProgramEntitetProgramId",
                        column: x => x.ProgramEntitetProgramId,
                        principalTable: "ProgramEntitet",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProgramEntitet",
                columns: new[] { "ProgramId", "KrugLicitacije", "MaksimalnaPovrsina" },
                values: new object[,]
                {
                    { new Guid("31511a3e-60de-4d24-80c4-00947314092d"), 1, 200 },
                    { new Guid("58b116a2-d458-4f72-abe8-be6135ece89e"), 2, 500 },
                    { new Guid("6942fde3-13e0-4832-bfbf-8a9a10572b70"), 1, 300 }
                });

            migrationBuilder.InsertData(
                table: "Licitacija",
                columns: new[] { "LicitacijaId", "BrojLicitacije", "GodinaLicitacije", "KorakCeneLicitacije", "OgranicenjeLicitacije", "ProgramEntitetProgramId", "RokLicitacije" },
                values: new object[,]
                {
                    { new Guid("684c392b-7871-4d1c-a93d-9d8661adf9e9"), 42, 2012, 3, 12, new Guid("58b116a2-d458-4f72-abe8-be6135ece89e"), new DateTime(2021, 9, 29, 18, 50, 18, 510, DateTimeKind.Local).AddTicks(8883) },
                    { new Guid("98992fb8-8ba8-4586-a7a3-6e2b5472897b"), 42, 2012, 3, 12, new Guid("31511a3e-60de-4d24-80c4-00947314092d"), new DateTime(2021, 9, 29, 18, 50, 18, 510, DateTimeKind.Local).AddTicks(8815) },
                    { new Guid("9d70d72c-7f57-4323-9eb7-1444ef348220"), 42, 2012, 3, 12, new Guid("6942fde3-13e0-4832-bfbf-8a9a10572b70"), new DateTime(2021, 9, 29, 18, 50, 18, 510, DateTimeKind.Local).AddTicks(8879) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licitacija_ProgramEntitetProgramId",
                table: "Licitacija",
                column: "ProgramEntitetProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitacija");

            migrationBuilder.DropTable(
                name: "ProgramEntitet");
        }
    }
}
