using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryWala.DataAccessLayer.Migrations
{
    public partial class updateorderColumnsforName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84135fb3-17ce-430e-9621-64acb904d3f4", "AQAAAAEAACcQAAAAEIX6NFP9dVKcaNKNJO5XiukumrCwWNUPwdpanLaLCAV3l8D71gMHp16pwbStjSeqAA==", "6ed4c0f3-db88-4680-bbd3-4aeb2df66672" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18e99d7b-db40-4cbd-b305-43941cd5d9d7", "AQAAAAEAACcQAAAAEBAcI/wxUucN8GzFEswMP7HPPYpWA5P4YqX7/Mof63PrW1xG3BfgIYrL6eEdbBR6uQ==", "a6d504b6-6417-45cf-8abc-a7398330bd5f" });
        }
    }
}
