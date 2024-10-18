using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshDating.Migrations
{
    /// <inheritdoc />
    public partial class AddFindPartnerLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderId", "GenderName" },
                values: new object[] { 3, "Other" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "GenderId",
                keyValue: 3);
        }
    }
}
