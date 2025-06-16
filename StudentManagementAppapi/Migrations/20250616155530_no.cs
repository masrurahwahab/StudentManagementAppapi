using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementAppapi.Migrations
{
    /// <inheritdoc />
    public partial class no : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Results");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 15, 55, 29, 319, DateTimeKind.Utc).AddTicks(7182), "22F9DBE1F571B1AFF5F0D3F3C87B7FAB6B49B3A4B0CEDF2DECC3AA03CE8388C2", new byte[] { 237, 124, 139, 16, 87, 248, 48, 223, 222, 57, 253, 241, 93, 173, 165, 81 }, new DateTime(2025, 6, 16, 15, 55, 29, 319, DateTimeKind.Utc).AddTicks(7182) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Results",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 8, 31, 45, 120, DateTimeKind.Utc).AddTicks(4422), "767FC54CCB90AB36769929EEE96F07B46F29819877D80EA95A03FFD2DD131719", new byte[] { 138, 254, 242, 123, 171, 224, 9, 131, 44, 168, 34, 175, 181, 105, 99, 47 }, new DateTime(2025, 6, 16, 8, 31, 45, 120, DateTimeKind.Utc).AddTicks(4424) });
        }
    }
}
