using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace msparcela.Migrations
{
    /// <inheritdoc />
    public partial class NadmetanjeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<Guid>(
                name: "NadmetanjeId",
                table: "DeoParceles",
                type: "uniqueidentifier",
                nullable: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "NadmetanjeId",
                table: "DeoParceles");

           
        }
    }
}
