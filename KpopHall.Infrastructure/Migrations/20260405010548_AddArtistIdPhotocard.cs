using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KpopHall.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistIdPhotocard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ArtistId",
                table: "Photocards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Photocards_ArtistId",
                table: "Photocards",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photocards_ArtistId",
                table: "Photocards");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Photocards");
        }
    }
}
