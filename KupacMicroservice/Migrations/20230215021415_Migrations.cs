using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KupacMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kupac",
                columns: table => new
                {
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdresaKupac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OstvarenaPovrsina = table.Column<double>(type: "float", nullable: false),
                    ImaZabranu = table.Column<bool>(type: "bit", nullable: false),
                    DatumPocetkaZabrane = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzinaTrajanjaZabraneGod = table.Column<int>(type: "int", nullable: false),
                    BrojTelefona1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojRacuna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IznosUplata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioritet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.KupacId);
                });

            migrationBuilder.CreateTable(
                name: "Liciter",
                columns: table => new
                {
                    LiciterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeLiciter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezimeLiciter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JmbgLiciter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brojpasosa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzavastranac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresaLiciter = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liciter", x => x.LiciterId);
                });

            migrationBuilder.CreateTable(
                name: "FizickoLice",
                columns: table => new
                {
                    FizickoliceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresaFizickoLice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FizickoLice", x => new { x.KupacId, x.FizickoliceId });
                    table.ForeignKey(
                        name: "FK_FizickoLice_Kupac_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupac",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PravnoLice",
                columns: table => new
                {
                    PravnoliceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaticniBroj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontaktOsobaIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontaktOsobaPrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresaPravnoLice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravnoLice", x => new { x.KupacId, x.PravnoliceId });
                    table.ForeignKey(
                        name: "FK_PravnoLice_Kupac_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupac",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OvlascenoLice",
                columns: table => new
                {
                    LiciterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KupacId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvlascenoLice", x => new { x.KupacId, x.LiciterId });
                    table.ForeignKey(
                        name: "FK_OvlascenoLice_Kupac_KupacId",
                        column: x => x.KupacId,
                        principalTable: "Kupac",
                        principalColumn: "KupacId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvlascenoLice_Liciter_LiciterId",
                        column: x => x.LiciterId,
                        principalTable: "Liciter",
                        principalColumn: "LiciterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kupac",
                columns: new[] { "KupacId", "AdresaKupac", "BrojRacuna", "BrojTelefona1", "BrojTelefona2", "DatumPocetkaZabrane", "DuzinaTrajanjaZabraneGod", "Email", "ImaZabranu", "IznosUplata", "OstvarenaPovrsina", "Prioritet" },
                values: new object[,]
                {
                    { new Guid("16a17928-85a4-43a0-a4f1-a48a0788bdfb"), "Nis", "902 ‑ 11501 ‑ 97", "0617290600", "0647089023", new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 2, "skijac@gmail.com", false, "123908", 600.0, "Vlasnik sistema za navodnjavanje" },
                    { new Guid("32c2d78f-adc0-41bf-83f4-b7f4e13da966"), "Beocin", "908 ‑ 10501 ‑ 97", "0637240699", "0617569013", new DateTime(2018, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, "toplana@gmail.com", false, "220000", 700.0, "Vlasnik zemljista koje se granici sa zemljistem koje se daje u zakup" },
                    { new Guid("4f602e62-fa2f-425d-80c5-56d3fe1fc6d4"), "Beocin", "908 ‑ 10501 ‑ 97", "0637240699", "0617569013", new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, "biohemija@gmail.com", true, "220000", 700.0, "Vlasnik zemljista koje je najblize zemljistu koje se daje u zakup" },
                    { new Guid("e65bada3-210e-4fad-9d4e-14cb41cc1ae1"), "Zrenjanin", "903 ‑ 12601 ‑ 97", "0697230791", "0617499014", new DateTime(2017, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, "gradskauprava@gmail.com", true, "340000", 1000.0, "Poljoprivrednik koji je upisan u registar" }
                });

            migrationBuilder.InsertData(
                table: "Liciter",
                columns: new[] { "LiciterId", "AdresaLiciter", "Brojpasosa", "Drzavastranac", "ImeLiciter", "JmbgLiciter", "PrezimeLiciter" },
                values: new object[,]
                {
                    { new Guid("895aa525-1762-4981-8d82-8874b0022a49"), "Mesto2", "NO000001", "ozankazastranudrzavu", "Nikola", "1731494293013", "Nikolic" },
                    { new Guid("b76bc440-2e4f-468d-b5e4-7529767cf9be"), "Mesto", "NO0000801", "ozankazastranudrzavu", "Luka", "0711090293012", "Lukic" }
                });

            migrationBuilder.InsertData(
                table: "FizickoLice",
                columns: new[] { "FizickoliceId", "KupacId", "AdresaFizickoLice", "Ime", "JMBG", "Prezime" },
                values: new object[,]
                {
                    { new Guid("8e914117-de7e-41bd-86b4-c529fa278817"), new Guid("16a17928-85a4-43a0-a4f1-a48a0788bdfb"), "Novi Sad", "Petar", "2906000855002", "Petrovic" },
                    { new Guid("16fd3722-e965-494f-9de6-fc4046d49f31"), new Guid("4f602e62-fa2f-425d-80c5-56d3fe1fc6d4"), "Subotica", "Marko", "1202966156142", "Markovic" }
                });

            migrationBuilder.InsertData(
                table: "OvlascenoLice",
                columns: new[] { "KupacId", "LiciterId" },
                values: new object[,]
                {
                    { new Guid("16a17928-85a4-43a0-a4f1-a48a0788bdfb"), new Guid("b76bc440-2e4f-468d-b5e4-7529767cf9be") },
                    { new Guid("32c2d78f-adc0-41bf-83f4-b7f4e13da966"), new Guid("895aa525-1762-4981-8d82-8874b0022a49") },
                    { new Guid("32c2d78f-adc0-41bf-83f4-b7f4e13da966"), new Guid("b76bc440-2e4f-468d-b5e4-7529767cf9be") }
                });

            migrationBuilder.InsertData(
                table: "PravnoLice",
                columns: new[] { "KupacId", "PravnoliceId", "AdresaPravnoLice", "Faks", "KontaktOsobaIme", "KontaktOsobaPrezime", "MaticniBroj", "Naziv" },
                values: new object[,]
                {
                    { new Guid("32c2d78f-adc0-41bf-83f4-b7f4e13da966"), new Guid("4422d9d6-b4e5-4470-a00f-520dca57118b"), "Beograd", "+1-213-9856512", "Dragan", "Draganovic", "12545690", "Toplana" },
                    { new Guid("e65bada3-210e-4fad-9d4e-14cb41cc1ae1"), new Guid("594b1f58-522c-4314-9072-7553eabb72b6"), "Kragujevac", "+1-208-9946522", "Dejan", "Lukic", "56790123", "Biohemija" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FizickoLice_KupacId",
                table: "FizickoLice",
                column: "KupacId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OvlascenoLice_LiciterId",
                table: "OvlascenoLice",
                column: "LiciterId");

            migrationBuilder.CreateIndex(
                name: "IX_PravnoLice_KupacId",
                table: "PravnoLice",
                column: "KupacId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FizickoLice");

            migrationBuilder.DropTable(
                name: "OvlascenoLice");

            migrationBuilder.DropTable(
                name: "PravnoLice");

            migrationBuilder.DropTable(
                name: "Liciter");

            migrationBuilder.DropTable(
                name: "Kupac");
        }
    }
}
