using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetService.Migrations
{
    public partial class GenerateUserSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "created_by", "created_by_username", "deleted_at", "deleted_by", "deleted_by_username", "email", "name", "password", "updated_at", "updated_by", "updated_by_username" },
                values: new object[] { new Guid("7430300f-f214-4ae2-9a9a-cfbe18243823"), new DateTime(2024, 8, 20, 13, 51, 51, 710, DateTimeKind.Local).AddTicks(1881), null, "", null, null, "", "admin@admin.com", "Admin", "$2a$11$nNLYuqxG3fpx4v6Twkfa/O9APmbfRGpT3pum./tHa4J6Xnp1sIwjK", new DateTime(2024, 8, 20, 13, 51, 51, 710, DateTimeKind.Local).AddTicks(1908), null, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("7430300f-f214-4ae2-9a9a-cfbe18243823"));
        }
    }
}
