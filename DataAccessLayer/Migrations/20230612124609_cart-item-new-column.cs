using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryWala.DataAccessLayer.Migrations
{
    public partial class cartitemnewcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c0adc0d-b603-45f0-a143-93ff617ae360", "AQAAAAEAACcQAAAAEGevKuXITBW39TfJhwEimLduDOrs+ACJPGPCP2N0b0EHodAqPUQtoeYpG7wke1sjdA==", "c5dc3593-cf85-4c04-9269-1030701f1434" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21a5851b-943e-463f-9d2d-026957638c88", "AQAAAAEAACcQAAAAEB3bBpTzyML0vRK+moKUCE+2KbOCwQ2eTkCijNpVpnh+4wRkEFkuABd1w10jNOqI5g==", "1aa3e06e-ad00-4da7-94f1-6ede0c487c4b" });
        }
    }
}
