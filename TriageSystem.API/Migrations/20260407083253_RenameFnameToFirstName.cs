using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriageSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameFnameToFirstName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lname",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Fname",
                table: "AspNetUsers",
                newName: "FirstName");
        }

        /// <inheritdoc />
       protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "FirstName",
            table: "AspNetUsers",
            newName: "Fname");

            migrationBuilder.RenameColumn(
            name: "LastName",
            table: "AspNetUsers",
            newName: "Lname");
            }
    }
}
