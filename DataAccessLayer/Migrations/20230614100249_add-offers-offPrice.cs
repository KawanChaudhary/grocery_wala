using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryWala.DataAccessLayer.Migrations
{
    public partial class addoffersoffPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OffPrice",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec7bd39f-346c-4994-ae53-73e07e358ea2", "AQAAAAEAACcQAAAAEJmvJrhBX0AgygPUGB42gWahrJ8DlQwRg8F/S2879+H4u36kHB9rQQG2aq5jrVJNZw==", "2f4d67c4-3bdc-44fd-ae35-dca3d60b7a56" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OffPrice",
                table: "Offers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "937df2f6-9649-4e66-a4ae-2e942900a1fa", "AQAAAAEAACcQAAAAEE7Lcsf2+E8mqCuAUNgWtzdotu/lC8SPvBCbacuyZOIye1GzdpL3Zs8qvQUmT8NC6Q==", "88727831-5368-4474-a5c7-e616f35a11c5" });
        }
    }
}
