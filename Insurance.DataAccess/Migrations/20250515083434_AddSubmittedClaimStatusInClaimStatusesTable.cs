using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmittedClaimStatusInClaimStatusesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClaimStatuses",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 4, true, "Submitted" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClaimStatuses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
