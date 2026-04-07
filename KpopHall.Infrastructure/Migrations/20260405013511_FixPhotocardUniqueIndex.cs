using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KpopHall.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPhotocardUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photocards_Version_AlbumId",
                table: "Photocards");

            migrationBuilder.CreateIndex(
                name: "IX_Photocards_Version_AlbumId_MemberId",
                table: "Photocards",
                columns: new[] { "Version", "AlbumId", "MemberId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photocards_Version_AlbumId_MemberId",
                table: "Photocards");

            migrationBuilder.CreateIndex(
                name: "IX_Photocards_Version_AlbumId",
                table: "Photocards",
                columns: new[] { "Version", "AlbumId" },
                unique: true);
        }
    }
}
