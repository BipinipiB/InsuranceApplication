using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateClaimsRemoveSubmitterIdAndUpdaterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AspNetUsers_SubmitterId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AspNetUsers_UpdaterId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SubmitterId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_UpdaterId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SubmitterId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "Claims");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SubmittedBy",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SubmittedBy",
                table: "Claims",
                column: "SubmittedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UpdatedBy",
                table: "Claims",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AspNetUsers_SubmittedBy",
                table: "Claims",
                column: "SubmittedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AspNetUsers_UpdatedBy",
                table: "Claims",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AspNetUsers_SubmittedBy",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_AspNetUsers_UpdatedBy",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SubmittedBy",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_UpdatedBy",
                table: "Claims");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SubmittedBy",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SubmitterId",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdaterId",
                table: "Claims",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SubmitterId",
                table: "Claims",
                column: "SubmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UpdaterId",
                table: "Claims",
                column: "UpdaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AspNetUsers_SubmitterId",
                table: "Claims",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_AspNetUsers_UpdaterId",
                table: "Claims",
                column: "UpdaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
