using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JASApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class update_order_orderitem_072925 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JasOrderItems_JASProducts_ProductId",
                schema: "Orders",
                table: "JasOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_JASOrders_JASProducts_ProductId",
                schema: "Products",
                table: "JASOrders");

            migrationBuilder.DropIndex(
                name: "IX_JASOrders_ProductId",
                schema: "Products",
                table: "JASOrders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Products",
                table: "JASOrders");

            migrationBuilder.RenameColumn(
                name: "ProductPurchaseId",
                schema: "Products",
                table: "JASOrders",
                newName: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                schema: "Orders",
                table: "JasOrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JasOrderItems_JASProducts_ProductId",
                schema: "Orders",
                table: "JasOrderItems",
                column: "ProductId",
                principalSchema: "Products",
                principalTable: "JASProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JasOrderItems_JASProducts_ProductId",
                schema: "Orders",
                table: "JasOrderItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Products",
                table: "JASOrders",
                newName: "ProductPurchaseId");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "Products",
                table: "JASOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                schema: "Orders",
                table: "JasOrderItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_JASOrders_ProductId",
                schema: "Products",
                table: "JASOrders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_JasOrderItems_JASProducts_ProductId",
                schema: "Orders",
                table: "JasOrderItems",
                column: "ProductId",
                principalSchema: "Products",
                principalTable: "JASProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JASOrders_JASProducts_ProductId",
                schema: "Products",
                table: "JASOrders",
                column: "ProductId",
                principalSchema: "Products",
                principalTable: "JASProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
