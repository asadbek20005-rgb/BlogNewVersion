using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "genders",
                columns: new[] { "id", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 7, 9, 57, 1, 510, DateTimeKind.Utc).AddTicks(8548), "Male", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 5, 7, 9, 57, 1, 510, DateTimeKind.Utc).AddTicks(8694), "Female", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_date", "name", "updated_date" },
                values: new object[] { 1, new DateTime(2025, 5, 7, 9, 57, 1, 511, DateTimeKind.Utc).AddTicks(4484), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "genders",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "genders",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
