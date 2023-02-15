using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MikroservisKomsija.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KCs",
                columns: table => new
                {
                    IDKomsije = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDClan = table.Column<int>(type: "int", nullable: false),
                    IsPredsjednik = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCs", x => new { x.IDKomsije, x.IDClan });

                    table.ForeignKey(
                    name:"FK_KomisijaClan_Clan",
                    column: x => x.IDClan,
                    principalTable: "Clans",
                    principalColumn:"IDClan",
                    onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                    name:"FK_KomisijaClan_Komisija",
                    column: x =>x.IDKomsije,
                    principalTable: "Koms",
                    principalColumn:"IDKomsije",
                    onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KCs");
        }
    }
}
