using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwardTypeObj = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizerGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BannerURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeLimit = table.Column<int>(type: "int", nullable: false),
                    EntryFee = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportTypeValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => new { x.EventGUID, x.UserGUID });
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    ReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.EventGUID, x.UserGUID });
                });

            migrationBuilder.CreateTable(
                name: "UserEventRelations",
                columns: table => new
                {
                    UserGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventRelations", x => new { x.EventGUID, x.UserGUID });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.GUID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserEventRelations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
