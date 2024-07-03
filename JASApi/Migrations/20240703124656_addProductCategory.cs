using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JASApi.Migrations
{
    /// <inheritdoc />
    public partial class addProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.EnsureSchema(
                name: "Reference");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "JASProduct",
                newSchema: "Reference");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Reference",
                table: "JASProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Reference",
                table: "JASProduct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "Reference",
                table: "JASProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                schema: "Reference",
                table: "JASProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JASProduct",
                schema: "Reference",
                table: "JASProduct",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Featured = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JASProduct_ProductCategoryId",
                schema: "Reference",
                table: "JASProduct",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JASProduct_ProductCategory_ProductCategoryId",
                schema: "Reference",
                table: "JASProduct",
                column: "ProductCategoryId",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JASProduct_ProductCategory_ProductCategoryId",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JASProduct",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.DropIndex(
                name: "IX_JASProduct_ProductCategoryId",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "Reference",
                table: "JASProduct");

            migrationBuilder.RenameTable(
                name: "JASProduct",
                schema: "Reference",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }
    }
}
