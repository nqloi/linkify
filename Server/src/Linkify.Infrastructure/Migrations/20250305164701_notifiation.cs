using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linkify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notifiation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Friendship_FriendshipId",
                table: "FriendRequest");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequest_FriendshipId",
                table: "FriendRequest");

            migrationBuilder.DropColumn(
                name: "FriendshipId",
                table: "FriendRequest");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActionUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Friendship_Id",
                table: "FriendRequest",
                column: "Id",
                principalTable: "Friendship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Friendship_Id",
                table: "FriendRequest");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.AddColumn<Guid>(
                name: "FriendshipId",
                table: "FriendRequest",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_FriendshipId",
                table: "FriendRequest",
                column: "FriendshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Friendship_FriendshipId",
                table: "FriendRequest",
                column: "FriendshipId",
                principalTable: "Friendship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
