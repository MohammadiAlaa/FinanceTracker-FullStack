using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceTracker_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Icon", "Name" },
                values: new object[,]
                {
                    { new Guid("17b9c7f1-dd11-4e9b-8a2c-309a2bf3c1fa"), "bg-red-500", "🏠", "Rent" },
                    { new Guid("32e76ff3-9c3b-4237-94b7-6642734b04dc"), "bg-orange-500", "🍔", "Food & Drinks" },
                    { new Guid("55aedb6f-2883-4673-b5a2-c3d67f3f8d3f"), "bg-green-500", "💰", "Salary" },
                    { new Guid("59a8deaf-3a30-4a0c-9b75-09947fc7ad70"), "bg-blue-500", "🚗", "Transport" },
                    { new Guid("be3e6802-6951-4842-b269-50bf73a7551c"), "bg-purple-500", "🛍️", "Shopping" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("17b9c7f1-dd11-4e9b-8a2c-309a2bf3c1fa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("32e76ff3-9c3b-4237-94b7-6642734b04dc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("55aedb6f-2883-4673-b5a2-c3d67f3f8d3f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("59a8deaf-3a30-4a0c-9b75-09947fc7ad70"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("be3e6802-6951-4842-b269-50bf73a7551c"));
        }
    }
}
