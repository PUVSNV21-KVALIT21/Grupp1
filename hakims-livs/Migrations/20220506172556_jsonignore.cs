using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hakims_livs.Migrations
{
    public partial class jsonignore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "1e38760a-0510-41bd-8aad-545ec9a9bf55");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "64c8f36a-8812-4516-bd32-fc0f129f4209", "AQAAAAEAACcQAAAAEA427Ur0EbGuo2eETYNqsmQWIyhALZUmhcq4pr2IkUHHByAF4atXx/kYs0kYd7cNYg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
