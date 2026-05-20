using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriageSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class CompleteNormalizedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriorityAssessments_AspNetUsers_ValidatedByNurseId",
                table: "PriorityAssessments");

            migrationBuilder.AddForeignKey(
                name: "FK_PriorityAssessments_AspNetUsers_ValidatedByNurseId",
                table: "PriorityAssessments",
                column: "ValidatedByNurseId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriorityAssessments_AspNetUsers_ValidatedByNurseId",
                table: "PriorityAssessments");

            migrationBuilder.AddForeignKey(
                name: "FK_PriorityAssessments_AspNetUsers_ValidatedByNurseId",
                table: "PriorityAssessments",
                column: "ValidatedByNurseId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
