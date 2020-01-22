using Microsoft.EntityFrameworkCore.Migrations;

namespace BangazonSite.Migrations
{
    public partial class MondayPM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "556b8341-36e8-4192-931c-896a11b340cf", "AQAAAAEAACcQAAAAEKzILhLGqvXyvvKa70AieuLM8sLuJZyMn0JzBon/vbdIZrA5oUpwyd39hJlJ1GcJCw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "16c2fd97-4595-49ff-8c90-1ddb198db17c", "AQAAAAEAACcQAAAAELNONgSVHnr61ioS0+C9M4WNFp23H++W4IOQ3flBXctToFRBBwzO4q8MWIZcaQLi3w==" });
        }
    }
}
