using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nadmetanjemicroserviceDAL.Migrations
{
    /// <inheritdoc />
    public partial class removeVrednostJN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VrednostJavnogNadmetanja",
                table: "Nadmetanja");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "VrednostJavnogNadmetanja",
                table: "Nadmetanja",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
