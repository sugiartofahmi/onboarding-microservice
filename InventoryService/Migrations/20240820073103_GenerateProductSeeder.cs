using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetService.Migrations
{
    public partial class GenerateProductSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_at", "description", "name", "price", "stock", "updated_at" },
                values: new object[] { new Guid("0eebdf48-1e0c-4738-8913-7c8f0342f886"), new DateTime(2024, 8, 20, 14, 31, 2, 695, DateTimeKind.Local).AddTicks(4293), "High-performance laptop for professionals", "Laptop", 1500000, 50, new DateTime(2024, 8, 20, 14, 31, 2, 695, DateTimeKind.Local).AddTicks(4306) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_at", "description", "name", "price", "stock", "updated_at" },
                values: new object[] { new Guid("af482c65-3638-457c-9e82-b2d840adb9f7"), new DateTime(2024, 8, 20, 14, 31, 2, 695, DateTimeKind.Local).AddTicks(4316), "Latest model smartphone with advanced features", "Smartphone", 800000, 100, new DateTime(2024, 8, 20, 14, 31, 2, 695, DateTimeKind.Local).AddTicks(4319) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_at", "description", "name", "price", "stock", "updated_at" },
                values: new object[] { new Guid("dad6ca7f-b6a6-46d8-a724-870673ec6822"), new DateTime(2024, 8, 20, 14, 31, 2, 695, DateTimeKind.Local).AddTicks(4344), "Wireless noise-cancelling headphones", "Headphones", 300000, 75, new DateTime(2024, 8, 20, 14, 31, 2, 695, DateTimeKind.Local).AddTicks(4347) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("0eebdf48-1e0c-4738-8913-7c8f0342f886"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("af482c65-3638-457c-9e82-b2d840adb9f7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("dad6ca7f-b6a6-46d8-a724-870673ec6822"));
        }
    }
}
