using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AlterGroup2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatorId",
                table: "Groups",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "GUID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatorId",
                table: "Groups");

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
    }
}
