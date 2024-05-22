using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinRequestEndPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "JoinRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinRequests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "Id", "UserId", "GroupId", "RoleId" });
        }
    }
}
