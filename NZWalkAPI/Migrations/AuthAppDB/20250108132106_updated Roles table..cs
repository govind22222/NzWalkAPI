using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalkAPI.Migrations.AuthAppDB
{
    /// <inheritdoc />
    public partial class updatedRolestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af962303-b468-423d-bdf4-7d8589e94687",
                column: "NormalizedName",
                value: "WRITEROLE");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e0b098-c394-495d-88e4-3420711577b9",
                column: "NormalizedName",
                value: "READROLE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af962303-b468-423d-bdf4-7d8589e94687",
                column: "NormalizedName",
                value: "WRITER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4e0b098-c394-495d-88e4-3420711577b9",
                column: "NormalizedName",
                value: "READER");
        }
    }
}
