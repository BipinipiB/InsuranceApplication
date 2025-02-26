using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Insurance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuestionTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Policies_PolicyTypes_PolicyTypeId",
            //    table: "Policies");

            //migrationBuilder.DropTable(
            //    name: "PolicyTypes");

            //migrationBuilder.CreateTable(
            //    name: "PolicyType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PolicyType", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "QuestionTypeEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypeEntities", x => x.Id);
                });

            //migrationBuilder.InsertData(
            //    table: "PolicyType",
            //    columns: new[] { "Id", "IsActive", "Name" },
            //    values: new object[,]
            //    {
            //        { 1, true, "Individual Health Insurance" },
            //        { 2, true, "Family Health Insurance" },
            //        { 3, true, "Group Health Insurance" }
            //    });

            migrationBuilder.InsertData(
                table: "QuestionTypeEntities",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, false, "Multiple Choice" },
                    { 2, false, "True/False" },
                    { 3, false, "Text" }
                });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Policies_PolicyType_PolicyTypeId",
            //    table: "Policies",
            //    column: "PolicyTypeId",
            //    principalTable: "PolicyType",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_PolicyType_PolicyTypeId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "PolicyType");

            migrationBuilder.DropTable(
                name: "QuestionTypeEntities");

            migrationBuilder.CreateTable(
                name: "PolicyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTypes", x => x.Id);
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

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_PolicyTypes_PolicyTypeId",
                table: "Policies",
                column: "PolicyTypeId",
                principalTable: "PolicyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
