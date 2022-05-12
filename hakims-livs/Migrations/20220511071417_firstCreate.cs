using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hakims_livs.Migrations
{
    public partial class firstCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "1d4d7eed-3a14-4565-b30c-f112e15e0421");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cca16963-6fb2-4db7-a655-696504bc65c0", "AQAAAAEAACcQAAAAEPuuR1+96O1/+A88K0xO7os7BYOGhcQqriwmS02giDWZhMdi3dEL0NSW7GcJ99qNhQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "ebd53da7-27c5-4f86-9e9d-ceb9d2f55497");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47b3c1ed-9538-458f-9608-f239e6f8f216", "AQAAAAEAACcQAAAAEAEmAiWpgOOqhhFsyZGYEHA0QpBkcqndVf8R3EgJPAHgiiBithfnMA7jPzJrx6yfsw==" });
        }
    }
}
