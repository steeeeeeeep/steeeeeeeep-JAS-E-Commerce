using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JASApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class update_Order_072925 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JASOrders_JasApplicationUser_ApplicationUserId",
                schema: "Products",
                table: "JASOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                schema: "Products",
                table: "JASOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JASOrders_JasApplicationUser_ApplicationUserId",
                schema: "Products",
                table: "JASOrders",
                column: "ApplicationUserId",
                principalSchema: "ApplicationUsers",
                principalTable: "JasApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JASOrders_JasApplicationUser_ApplicationUserId",
                schema: "Products",
                table: "JASOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                schema: "Products",
                table: "JASOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JASOrders_JasApplicationUser_ApplicationUserId",
                schema: "Products",
                table: "JASOrders",
                column: "ApplicationUserId",
                principalSchema: "ApplicationUsers",
                principalTable: "JasApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
