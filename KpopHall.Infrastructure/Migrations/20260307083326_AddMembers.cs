using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KpopHall.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Photocards",
                newName: "Version");

            migrationBuilder.RenameIndex(
                name: "IX_Photocards_Name_AlbumId",
                table: "Photocards",
                newName: "IX_Photocards_Version_AlbumId");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Photocards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_ArtistId",
                table: "Members",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Name_ArtistId",
                table: "Members",
                columns: new[] { "Name", "ArtistId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Photocards");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Photocards",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Photocards_Version_AlbumId",
                table: "Photocards",
                newName: "IX_Photocards_Name_AlbumId");
        }
    }
}
