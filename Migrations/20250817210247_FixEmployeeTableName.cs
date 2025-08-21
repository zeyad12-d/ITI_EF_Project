using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodeFirstTask.Migrations
{
    /// <inheritdoc />
    public partial class FixEmployeeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Employess_EmployessEmployeeId",
                table: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Employess");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "FullName" },
                values: new object[,]
                {
                    { 1, 1, "Ahmed Ali" },
                    { 2, 2, "Sara Mohamed" },
                    { 3, 3, "Omar Hassan" },
                    { 4, 4, "Mona Ibrahim" },
                    { 5, 5, "Khaled Mahmoud" }
                });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 18, 0, 2, 45, 107, DateTimeKind.Local).AddTicks(5849));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 8, 18, 0, 2, 45, 107, DateTimeKind.Local).AddTicks(5893));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2025, 8, 18, 0, 2, 45, 107, DateTimeKind.Local).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2025, 8, 18, 0, 2, 45, 107, DateTimeKind.Local).AddTicks(5912));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2025, 8, 18, 0, 2, 45, 107, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Employees_EmployessEmployeeId",
                table: "EmployeeProjects",
                column: "EmployessEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjects_Employees_EmployessEmployeeId",
                table: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.CreateTable(
                name: "Employess",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employess", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employess_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employess",
                columns: new[] { "EmployeeId", "DepartmentId", "FullName" },
                values: new object[,]
                {
                    { 1, 1, "Ahmed Ali" },
                    { 2, 2, "Sara Mohamed" },
                    { 3, 3, "Omar Hassan" },
                    { 4, 4, "Mona Ibrahim" },
                    { 5, 5, "Khaled Mahmoud" }
                });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6722));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6775));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 3,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6779));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 4,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6783));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 5,
                column: "StartDate",
                value: new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6786));

            migrationBuilder.CreateIndex(
                name: "IX_Employess_DepartmentId",
                table: "Employess",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjects_Employess_EmployessEmployeeId",
                table: "EmployeeProjects",
                column: "EmployessEmployeeId",
                principalTable: "Employess",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
