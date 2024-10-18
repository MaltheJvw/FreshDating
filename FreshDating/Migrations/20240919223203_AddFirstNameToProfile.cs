using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshDating.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstNameToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_MatchedUserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchDate",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "MatchedUserId",
                table: "Matches",
                newName: "Profile2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_MatchedUserId",
                table: "Matches",
                newName: "IX_Matches_Profile2Id");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Profile1Id",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "MatchId");

            migrationBuilder.CreateTable(
                name: "ProfileLikes",
                columns: table => new
                {
                    FromProfileId = table.Column<int>(type: "int", nullable: false),
                    ToProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileLikes", x => new { x.FromProfileId, x.ToProfileId });
                    table.ForeignKey(
                        name: "FK_ProfileLikes_Profiles_FromProfileId",
                        column: x => x.FromProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileLikes_Profiles_ToProfileId",
                        column: x => x.ToProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Profile1Id",
                table: "Matches",
                column: "Profile1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLikes_ToProfileId",
                table: "ProfileLikes",
                column: "ToProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Profiles_Profile1Id",
                table: "Matches",
                column: "Profile1Id",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Profiles_Profile2Id",
                table: "Matches",
                column: "Profile2Id",
                principalTable: "Profiles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Profiles_Profile1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Profiles_Profile2Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "ProfileLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_Profile1Id",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Profile1Id",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Profile2Id",
                table: "Matches",
                newName: "MatchedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Profile2Id",
                table: "Matches",
                newName: "IX_Matches_MatchedUserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDate",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                columns: new[] { "UserId", "MatchedUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_MatchedUserId",
                table: "Matches",
                column: "MatchedUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
