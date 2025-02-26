using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Insurance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreatePolicyTypeTableAndAddForeignKeyInPolicyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PolicyTypes",
                table: "Policies");

            migrationBuilder.AddColumn<int>(
                name: "PolicyTypeId",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PolicyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PolicyTypes",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Individual Health Insurance" },
                    { 2, true, "Family Health Insurance" },
                    { 3, true, "Group Health Insurance" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PolicyTypeId",
                table: "Policies",
                column: "PolicyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_PolicyType_PolicyTypeId",
                table: "Policies",
                column: "PolicyTypeId",
                principalTable: "PolicyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_PolicyType_PolicyTypeId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "PolicyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Policies_PolicyTypeId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyTypeId",
                table: "Policies");

            migrationBuilder.AddColumn<string>(
                name: "PolicyTypes",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
