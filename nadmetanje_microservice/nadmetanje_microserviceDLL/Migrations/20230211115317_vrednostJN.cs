using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nadmetanjemicroserviceDAL.Migrations
{
    /// <inheritdoc />
    public partial class vrednostJN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "VrednostJavnogNadmetanja",
                table: "Nadmetanja",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VrednostJavnogNadmetanja",
                table: "Nadmetanja");
        }
    }
}
