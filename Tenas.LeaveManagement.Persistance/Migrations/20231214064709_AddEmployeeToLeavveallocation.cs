using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tenas.LeaveManagement.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeToLeavveallocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("0effa21e-3607-4ce6-9493-cf3ada696caa"));

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("aec4258d-89dd-4439-8d3d-116f561d18b8"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "LeaveAllocation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefaultDays", "DeletedAt", "DeletedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("c1e5e5af-a564-4d23-aca5-780071664d85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 39, null, null, "Vacation", null, null },
                    { new Guid("e4e423a5-23ed-40d9-8d7c-9766d0cd335e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 44, null, null, "Sick", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("c1e5e5af-a564-4d23-aca5-780071664d85"));

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: new Guid("e4e423a5-23ed-40d9-8d7c-9766d0cd335e"));

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveAllocation");

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DefaultDays", "DeletedAt", "DeletedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0effa21e-3607-4ce6-9493-cf3ada696caa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 13, null, null, "Vacation", null, null },
                    { new Guid("aec4258d-89dd-4439-8d3d-116f561d18b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, null, "Sick", null, null }
                });
        }
    }
}
