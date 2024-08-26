using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetService.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_at", "description", "name", "price", "stock", "updated_at" },
                values: new object[] { new Guid("3827ee60-87a6-4da9-a0db-11ba0499d232"), new DateTime(2024, 8, 26, 13, 53, 21, 399, DateTimeKind.Local).AddTicks(7155), "High-performance laptop for professionals", "Laptop", 1500000, 50, new DateTime(2024, 8, 26, 13, 53, 21, 399, DateTimeKind.Local).AddTicks(7168) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_at", "description", "name", "price", "stock", "updated_at" },
                values: new object[] { new Guid("b8089064-48d3-406d-8625-bb1a953c4938"), new DateTime(2024, 8, 26, 13, 53, 21, 399, DateTimeKind.Local).AddTicks(7259), "Latest model smartphone with advanced features", "Smartphone", 800000, 100, new DateTime(2024, 8, 26, 13, 53, 21, 399, DateTimeKind.Local).AddTicks(7260) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "created_at", "description", "name", "price", "stock", "updated_at" },
                values: new object[] { new Guid("f20dee07-e193-4ceb-916b-fd796501363c"), new DateTime(2024, 8, 26, 13, 53, 21, 399, DateTimeKind.Local).AddTicks(7265), "Wireless noise-cancelling headphones", "Headphones", 300000, 75, new DateTime(2024, 8, 26, 13, 53, 21, 399, DateTimeKind.Local).AddTicks(7266) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_id",
                table: "Products",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
