using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KorisnikSistemaServis.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipKorisnika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "KorisnikId", "Ime", "KorisnickoIme", "Lozinka", "Prezime", "TipKorisnika" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Sanja", "sanja123", "test123", "Tica", 0 },
                    { new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"), "Jovana", "jovanaj", "jovanatest", "Jovanovic", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
