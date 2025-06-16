using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementAppapi.Migrations
{
    /// <inheritdoc />
    public partial class cool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Email", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 8, 28, 16, 710, DateTimeKind.Utc).AddTicks(4985), "kkay@gmail.com", "1EFA8F5A99CDF065E185858CD00C0E9C3254942E74BC534DF66FA79404129B20", new byte[] { 150, 127, 126, 9, 185, 245, 41, 40, 177, 152, 99, 247, 42, 247, 233, 105 }, new DateTime(2025, 6, 16, 8, 28, 16, 710, DateTimeKind.Utc).AddTicks(4986) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Email", "PasswordHash", "Salt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 6, 59, 22, 421, DateTimeKind.Utc).AddTicks(7339), "kkay@school.com", "059A44B7BDBD5FBD8731906FE3F0FCADC8FF8A0871614E8FAF08FD9415D8F0F1", new byte[] { 231, 231, 252, 101, 193, 44, 241, 129, 253, 219, 235, 56, 222, 59, 74, 139 }, new DateTime(2025, 6, 16, 6, 59, 22, 421, DateTimeKind.Utc).AddTicks(7340) });
        }
    }
}
