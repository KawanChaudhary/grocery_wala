using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryWala.DataAccessLayer.Migrations
{
    public partial class cartitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21a5851b-943e-463f-9d2d-026957638c88", "AQAAAAEAACcQAAAAEB3bBpTzyML0vRK+moKUCE+2KbOCwQ2eTkCijNpVpnh+4wRkEFkuABd1w10jNOqI5g==", "1aa3e06e-ad00-4da7-94f1-6ede0c487c4b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08548908-21d5-42aa-aee4-27baf6ecf859", "AQAAAAEAACcQAAAAEEYT3HmGcZ637p1CfWmMDBaBdgZbI3HMK2w4u1LHH8vpQmhiYjU2I0ye30YH+Bn2yg==", "6c6caf9e-d740-476d-b874-51e2a405bef7" });
        }
    }
}
