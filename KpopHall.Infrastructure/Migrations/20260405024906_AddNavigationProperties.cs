using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KpopHall.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Photocards_MemberId",
                table: "Photocards",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photocards_Artists_ArtistId",
                table: "Photocards",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photocards_Members_MemberId",
                table: "Photocards",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photocards_Artists_ArtistId",
                table: "Photocards");

            migrationBuilder.DropForeignKey(
                name: "FK_Photocards_Members_MemberId",
                table: "Photocards");

            migrationBuilder.DropIndex(
                name: "IX_Photocards_MemberId",
                table: "Photocards");
        }
    }
}
