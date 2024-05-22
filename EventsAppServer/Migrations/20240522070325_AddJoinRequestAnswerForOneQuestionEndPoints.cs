using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinRequestAnswerForOneQuestionEndPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoinRequestAnswerToOneQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinRequestAnswerToOneQuestion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinRequestAnswerToOneQuestion");
        }
    }
}
