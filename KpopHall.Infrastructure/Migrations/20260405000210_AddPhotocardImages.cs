using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KpopHall.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotocardImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BacksideImagePath",
                table: "Photocards",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrontsideImagePath",
                table: "Photocards",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BacksideImagePath",
                table: "Photocards");

            migrationBuilder.DropColumn(
                name: "FrontsideImagePath",
                table: "Photocards");
        }
    }
}
