using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CodeFirstTask.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Employess",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    EmployessEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectsProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => new { x.EmployessEmployeeId, x.ProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employess_EmployessEmployeeId",
                        column: x => x.EmployessEmployeeId,
                        principalTable: "Employess",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "HR" },
                    { 2, "IT" },
                    { 3, "Finance" },
                    { 4, "Marketing" },
                    { 5, "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "EndDate", "ProjectName", "StartDate" },
                values: new object[,]
                {
                    { 1, null, "ERP System", new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6722) },
                    { 2, null, "Website Redesign", new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6775) },
                    { 3, null, "Mobile App", new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6779) },
                    { 4, null, "CRM Integration", new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6783) },
                    { 5, null, "AI Chatbot", new DateTime(2025, 8, 17, 23, 55, 23, 577, DateTimeKind.Local).AddTicks(6786) }
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

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectsProjectId",
                table: "EmployeeProjects",
                column: "ProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employess_DepartmentId",
                table: "Employess",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Employess");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
