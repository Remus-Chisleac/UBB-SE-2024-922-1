using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupUserEndPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostScore = table.Column<int>(type: "int", nullable: false),
                    MarketplaceScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.Id, x.UserId, x.GroupId, x.RoleId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUsers");
        }
    }
}
