using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hakims_livs.Migrations
{
    public partial class first2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "8c87d570-6533-46b3-a103-a7eaf35bd816");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63e2922e-4f06-470e-930b-4c53c7e44604", "AQAAAAEAACcQAAAAEEr7q+EGDvjhISRwJpXNy72oqsrdD74PPGbz+TClmKX3nczkg5lZwhpfsiorPYskoA==" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsID",
                table: "CategoryProduct",
                column: "ProductsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "3a97b95d-7280-4453-afbe-9e1e2b0bc524");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5be0d4d9-237e-4730-aded-9ac40c50a4ca", "AQAAAAEAACcQAAAAEOCda1SDhNIRcH47PCWYr7swYA/vcjsM6GejAg5BtSwh5p55PKC5IaeBBjL24u4sOQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryID",
                table: "Categories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductID",
                table: "Categories",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryID",
                table: "Categories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductID",
                table: "Categories",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
