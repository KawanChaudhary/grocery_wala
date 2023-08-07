using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryWala.DataAccessLayer.Migrations
{
    public partial class updateorderColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "UserOrders");

            migrationBuilder.DropColumn(
                name: "ImageAddress",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f13ee7ae-4ba8-479c-a64e-73e4a36c2591", "AQAAAAEAACcQAAAAEKXtA/Ex/6/pgT+QxUUvxOPeTQb3hxyOeFakwwG3lVQIP1jCAMW1/K5dDYuWkMXKHg==", "b460449f-9a92-43d1-ade9-d35c49dece3c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                table: "UserOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ImageAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed2c244e-b7c3-46c7-a2c6-6928d035d46c", "AQAAAAEAACcQAAAAEF0m4uCa8Fss8zPMnp0fSXMehNkICbDu0/d6yH9hiegru1EMxEjEtzSlK1YGopxFNQ==", "6fb4ad4a-bf44-4f77-bf2a-90cbf3a5589f" });
        }
    }
}
