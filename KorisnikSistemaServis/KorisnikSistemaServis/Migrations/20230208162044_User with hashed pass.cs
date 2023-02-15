using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KorisnikSistemaServis.Migrations
{
    /// <inheritdoc />
    public partial class Userwithhashedpass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "KorisnikId",
                keyValue: new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                column: "Lozinka",
                value: "$2a$11$gv/Gl4Fa0.wR4vQk74Ijse7i/oQqGzRsVkgZW7ZdPpAkojgKRpS/2");

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "KorisnikId",
                keyValue: new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                column: "Lozinka",
                value: "$2a$11$FNI2egC0aDwjCTUdVCb2O.y3RDqBGCD2eYwhioDpLLaFmimMz4XMC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "KorisnikId",
                keyValue: new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                column: "Lozinka",
                value: "test123");

            migrationBuilder.UpdateData(
                table: "Korisnici",
                keyColumn: "KorisnikId",
                keyValue: new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                column: "Lozinka",
                value: "jovanatest");
        }
    }
}
