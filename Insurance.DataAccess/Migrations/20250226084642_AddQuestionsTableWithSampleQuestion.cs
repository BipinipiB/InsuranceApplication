using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Insurance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionsTableWithSampleQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionLabel = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    Step = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypeEntities_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypeEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsActive", "QuestionLabel", "QuestionTypeId", "Step" },
                values: new object[,]
                {
                    { 1, true, "What Kind o Procedure are you making a claim for?", 3, "MakeAClaim" },
                    { 2, true, "Do you have other insurance policy you could claim against?", 4, "MakeAClaim" },
                    { 3, true, "ClaimAmount", 3, "PaymentInfo" },
                    { 4, true, "Enter Credit Card Number", 3, "PaymentInfo" },
                    { 5, true, "ExpiryDate", 3, "Expiry Date" },
                    { 6, true, "ExpiryDate", 3, "CCV" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
