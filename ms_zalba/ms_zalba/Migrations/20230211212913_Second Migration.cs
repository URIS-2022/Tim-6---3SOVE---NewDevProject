using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mszalba.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipZalbes",
                columns: table => new
                {
                    idTipZalbe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazivTipaZalbe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipZalbes", x => x.idTipZalbe);
                });

            migrationBuilder.CreateTable(
                name: "Zalbas",
                columns: table => new
                {
                    zalbaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tipZalbe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    datumPodnosenjaZalbe = table.Column<DateTime>(type: "date", nullable: false),
                    razlogZalbe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    obrazlozenje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumResenja = table.Column<DateTime>(type: "date", nullable: false),
                    brojResenja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    statusZalbe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    brojNadmetanja = table.Column<int>(type: "int", nullable: false),
                    radnjaNaOsnovuZalbe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    podnosilacZalbe = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalbas", x => x.zalbaID);
                    table.ForeignKey(
                    name: "FK_Zalba_TipZalbe",
                    column: x => x.tipZalbe,
                    principalTable: "TipZalbes",
                    principalColumn: "idTipZalbe",
                    onDelete: ReferentialAction.Cascade);
                });
        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipZalbes");

            migrationBuilder.DropTable(
                name: "Zalbas");
        }
    }
}
