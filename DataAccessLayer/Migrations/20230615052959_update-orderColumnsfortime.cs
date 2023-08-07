using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryWala.DataAccessLayer.Migrations
{
    public partial class updateorderColumnsfortime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18e99d7b-db40-4cbd-b305-43941cd5d9d7", "AQAAAAEAACcQAAAAEBAcI/wxUucN8GzFEswMP7HPPYpWA5P4YqX7/Mof63PrW1xG3BfgIYrL6eEdbBR6uQ==", "a6d504b6-6417-45cf-8abc-a7398330bd5f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f13ee7ae-4ba8-479c-a64e-73e4a36c2591", "AQAAAAEAACcQAAAAEKXtA/Ex/6/pgT+QxUUvxOPeTQb3hxyOeFakwwG3lVQIP1jCAMW1/K5dDYuWkMXKHg==", "b460449f-9a92-43d1-ade9-d35c49dece3c" });
        }
    }
}
