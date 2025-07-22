using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 1, "dsfsdfsd", "fghfghfg" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 3, 2, "The animal zoo", "zooPark" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 2, "The animal zoo", "zooPark" });
        }
    }
}
