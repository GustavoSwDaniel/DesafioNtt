using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioNtt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCityField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Address",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Address");
        }
    }
}
