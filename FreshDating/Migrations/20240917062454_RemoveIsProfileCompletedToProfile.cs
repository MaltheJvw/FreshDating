using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshDating.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsProfileCompletedToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileCompleted",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Smoker",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "SmokerStatus",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmokerStatus",
                table: "Profiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileCompleted",
                table: "Profiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Smoker",
                table: "Profiles",
                type: "bit",
                nullable: true);
        }
    }
}
