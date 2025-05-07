using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingCOnfig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2025, 5, 7, 9, 57, 1, 510, DateTimeKind.Utc).AddTicks(8548));

            migrationBuilder.UpdateData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2,
                column: "created_date",
                value: new DateTime(2025, 5, 7, 9, 57, 1, 510, DateTimeKind.Utc).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_date",
                value: new DateTime(2025, 5, 7, 9, 57, 1, 511, DateTimeKind.Utc).AddTicks(4484));
        }
    }
}
