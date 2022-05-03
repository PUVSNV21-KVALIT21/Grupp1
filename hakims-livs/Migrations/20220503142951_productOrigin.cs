using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hakims_livs.Migrations
{
    public partial class productOrigin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "8a0b4c02-32ca-4b5b-aac9-ca9289c010e7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa49ddf5-680a-4f5d-becc-20b6053ec7ce", "AQAAAAEAACcQAAAAEEkFwbobZd8wmSdxBZsInS+8w+pDNUCBdgz4DD49T737v6/xL3bNWYDb0UvNQarZpA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad376a8f-9eab-4bb9-9fca-30b01540f445",
                column: "ConcurrencyStamp",
                value: "ec7d6f80-1b36-4eb4-8d0e-48239c22dc8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "132a12df-4ee0-4b1c-8021-607729938865", "AQAAAAEAACcQAAAAEPi5mNrXrSSrgumV16MVRxeuJ3PfDBaKLTaXVJEt9XjoqdpXYaisU5yNZ7hpMqEgHw==" });
        }
    }
}
