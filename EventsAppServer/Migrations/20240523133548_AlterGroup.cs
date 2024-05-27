using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AlterGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_User_CreatorId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Groups",
                newName: "CreatorGUID");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                newName: "IX_Groups_CreatorGUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatorGUID",
                table: "Groups",
                column: "CreatorGUID",
                principalTable: "Users",
                principalColumn: "GUID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatorGUID",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "CreatorGUID",
                table: "Groups",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatorGUID",
                table: "Groups",
                newName: "IX_Groups_CreatorId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_User_CreatorId",
                table: "Groups",
                column: "CreatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
