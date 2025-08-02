using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JASApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class applicationuser_addps_073025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "ApplicationUsers",
                table: "JasApplicationUser",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "ApplicationUsers",
                table: "JasApplicationUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "ApplicationUsers",
                table: "JasApplicationUser");

            migrationBuilder.RenameColumn(
                name: "Street",
                schema: "ApplicationUsers",
                table: "JasApplicationUser",
                newName: "Address");
        }
    }
}
