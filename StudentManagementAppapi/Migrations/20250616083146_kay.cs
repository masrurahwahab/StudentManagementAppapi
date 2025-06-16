using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementAppapi.Migrations
{
    /// <inheritdoc />
    public partial class kay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 8, 31, 45, 120, DateTimeKind.Utc).AddTicks(4422), "767FC54CCB90AB36769929EEE96F07B46F29819877D80EA95A03FFD2DD131719", new byte[] { 138, 254, 242, 123, 171, 224, 9, 131, 44, 168, 34, 175, 181, 105, 99, 47 }, new DateTime(2025, 6, 16, 8, 31, 45, 120, DateTimeKind.Utc).AddTicks(4424) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 8, 28, 16, 710, DateTimeKind.Utc).AddTicks(4985), "1EFA8F5A99CDF065E185858CD00C0E9C3254942E74BC534DF66FA79404129B20", new byte[] { 150, 127, 126, 9, 185, 245, 41, 40, 177, 152, 99, 247, 42, 247, 233, 105 }, new DateTime(2025, 6, 16, 8, 28, 16, 710, DateTimeKind.Utc).AddTicks(4986) });
        }
    }
}
