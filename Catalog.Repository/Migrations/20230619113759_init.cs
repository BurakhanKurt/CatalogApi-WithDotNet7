using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Repository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7934), null, "Category 1", null },
                    { 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7951), null, "Category 2", null },
                    { 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7952), null, "Category 3", null },
                    { 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7954), null, "Category 4", null },
                    { 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7954), null, "Category 5", null },
                    { 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7957), null, "Category 6", null },
                    { 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7959), null, "Category 7", null },
                    { 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7960), null, "Category 8", null },
                    { 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7961), null, "Category 9", null },
                    { 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(7963), null, "Category 10", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Name", "Price", "Stock", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8121), null, "Product 1", 10.50m, 1000, null },
                    { 2, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8125), null, "Product 2", 10.50m, 1000, null },
                    { 3, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8126), null, "Product 3", 10.50m, 1000, null },
                    { 4, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8127), null, "Product 4", 10.50m, 1000, null },
                    { 5, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8128), null, "Product 5", 10.50m, 1000, null },
                    { 6, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8130), null, "Product 6", 10.50m, 1000, null },
                    { 7, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8132), null, "Product 7", 10.50m, 1000, null },
                    { 8, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8133), null, "Product 8", 10.50m, 1000, null },
                    { 9, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8134), null, "Product 9", 10.50m, 1000, null },
                    { 10, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8136), null, "Product 10", 10.50m, 1000, null },
                    { 11, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8137), null, "Product 11", 10.50m, 1000, null },
                    { 12, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8138), null, "Product 12", 10.50m, 1000, null },
                    { 13, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8139), null, "Product 13", 10.50m, 1000, null },
                    { 14, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8140), null, "Product 14", 10.50m, 1000, null },
                    { 15, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8141), null, "Product 15", 10.50m, 1000, null },
                    { 16, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8142), null, "Product 16", 10.50m, 1000, null },
                    { 17, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8143), null, "Product 17", 10.50m, 1000, null },
                    { 18, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8183), null, "Product 18", 10.50m, 1000, null },
                    { 19, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8184), null, "Product 19", 10.50m, 1000, null },
                    { 20, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8185), null, "Product 20", 10.50m, 1000, null },
                    { 21, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8186), null, "Product 21", 10.50m, 1000, null },
                    { 22, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8187), null, "Product 22", 10.50m, 1000, null },
                    { 23, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8188), null, "Product 23", 10.50m, 1000, null },
                    { 24, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8189), null, "Product 24", 10.50m, 1000, null },
                    { 25, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8191), null, "Product 25", 10.50m, 1000, null },
                    { 26, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8192), null, "Product 26", 10.50m, 1000, null },
                    { 27, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8193), null, "Product 27", 10.50m, 1000, null },
                    { 28, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8194), null, "Product 28", 10.50m, 1000, null },
                    { 29, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8195), null, "Product 29", 10.50m, 1000, null },
                    { 30, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8196), null, "Product 30", 10.50m, 1000, null },
                    { 31, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8197), null, "Product 31", 10.50m, 1000, null },
                    { 32, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8199), null, "Product 32", 10.50m, 1000, null },
                    { 33, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8200), null, "Product 33", 10.50m, 1000, null },
                    { 34, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8202), null, "Product 34", 10.50m, 1000, null },
                    { 35, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8203), null, "Product 35", 10.50m, 1000, null },
                    { 36, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8204), null, "Product 36", 10.50m, 1000, null },
                    { 37, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8205), null, "Product 37", 10.50m, 1000, null },
                    { 38, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8206), null, "Product 38", 10.50m, 1000, null },
                    { 39, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8207), null, "Product 39", 10.50m, 1000, null },
                    { 40, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8208), null, "Product 40", 10.50m, 1000, null },
                    { 41, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8209), null, "Product 41", 10.50m, 1000, null },
                    { 42, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8210), null, "Product 42", 10.50m, 1000, null },
                    { 43, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8212), null, "Product 43", 10.50m, 1000, null },
                    { 44, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8213), null, "Product 44", 10.50m, 1000, null },
                    { 45, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8214), null, "Product 45", 10.50m, 1000, null },
                    { 46, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8215), null, "Product 46", 10.50m, 1000, null },
                    { 47, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8216), null, "Product 47", 10.50m, 1000, null },
                    { 48, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8217), null, "Product 48", 10.50m, 1000, null },
                    { 49, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8218), null, "Product 49", 10.50m, 1000, null },
                    { 50, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8219), null, "Product 50", 10.50m, 1000, null },
                    { 51, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8220), null, "Product 51", 10.50m, 1000, null },
                    { 52, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8221), null, "Product 52", 10.50m, 1000, null },
                    { 53, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8223), null, "Product 53", 10.50m, 1000, null },
                    { 54, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8224), null, "Product 54", 10.50m, 1000, null },
                    { 55, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8225), null, "Product 55", 10.50m, 1000, null },
                    { 56, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8226), null, "Product 56", 10.50m, 1000, null },
                    { 57, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8227), null, "Product 57", 10.50m, 1000, null },
                    { 58, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8228), null, "Product 58", 10.50m, 1000, null },
                    { 59, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8229), null, "Product 59", 10.50m, 1000, null },
                    { 60, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8230), null, "Product 60", 10.50m, 1000, null },
                    { 61, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8231), null, "Product 61", 10.50m, 1000, null },
                    { 62, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8232), null, "Product 62", 10.50m, 1000, null },
                    { 63, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8233), null, "Product 63", 10.50m, 1000, null },
                    { 64, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8234), null, "Product 64", 10.50m, 1000, null },
                    { 65, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8235), null, "Product 65", 10.50m, 1000, null },
                    { 66, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8275), null, "Product 66", 10.50m, 1000, null },
                    { 67, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8276), null, "Product 67", 10.50m, 1000, null },
                    { 68, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8277), null, "Product 68", 10.50m, 1000, null },
                    { 69, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8278), null, "Product 69", 10.50m, 1000, null },
                    { 70, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8279), null, "Product 70", 10.50m, 1000, null },
                    { 71, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8280), null, "Product 71", 10.50m, 1000, null },
                    { 72, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8281), null, "Product 72", 10.50m, 1000, null },
                    { 73, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8282), null, "Product 73", 10.50m, 1000, null },
                    { 74, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8283), null, "Product 74", 10.50m, 1000, null },
                    { 75, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8284), null, "Product 75", 10.50m, 1000, null },
                    { 76, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8285), null, "Product 76", 10.50m, 1000, null },
                    { 77, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8287), null, "Product 77", 10.50m, 1000, null },
                    { 78, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8288), null, "Product 78", 10.50m, 1000, null },
                    { 79, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8289), null, "Product 79", 10.50m, 1000, null },
                    { 80, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8290), null, "Product 80", 10.50m, 1000, null },
                    { 81, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8291), null, "Product 81", 10.50m, 1000, null },
                    { 82, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8292), null, "Product 82", 10.50m, 1000, null },
                    { 83, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8293), null, "Product 83", 10.50m, 1000, null },
                    { 84, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8294), null, "Product 84", 10.50m, 1000, null },
                    { 85, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8295), null, "Product 85", 10.50m, 1000, null },
                    { 86, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8296), null, "Product 86", 10.50m, 1000, null },
                    { 87, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8297), null, "Product 87", 10.50m, 1000, null },
                    { 88, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8298), null, "Product 88", 10.50m, 1000, null },
                    { 89, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8299), null, "Product 89", 10.50m, 1000, null },
                    { 90, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8300), null, "Product 90", 10.50m, 1000, null },
                    { 91, 2, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8301), null, "Product 91", 10.50m, 1000, null },
                    { 92, 3, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8302), null, "Product 92", 10.50m, 1000, null },
                    { 93, 4, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8303), null, "Product 93", 10.50m, 1000, null },
                    { 94, 5, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8304), null, "Product 94", 10.50m, 1000, null },
                    { 95, 6, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8305), null, "Product 95", 10.50m, 1000, null },
                    { 96, 7, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8306), null, "Product 96", 10.50m, 1000, null },
                    { 97, 8, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8307), null, "Product 97", 10.50m, 1000, null },
                    { 98, 9, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8308), null, "Product 98", 10.50m, 1000, null },
                    { 99, 10, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8309), null, "Product 99", 10.50m, 1000, null },
                    { 100, 1, new DateTime(2023, 6, 19, 14, 37, 59, 637, DateTimeKind.Local).AddTicks(8311), null, "Product 100", 10.50m, 1000, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
