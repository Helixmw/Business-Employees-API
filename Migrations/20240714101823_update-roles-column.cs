using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employees_API.Migrations
{
    /// <inheritdoc />
    public partial class updaterolescolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Departments_DepartmentId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "EmployeeRoles");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_DepartmentId",
                table: "EmployeeRoles",
                newName: "IX_EmployeeRoles_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoles_Departments_DepartmentId",
                table: "EmployeeRoles",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoles_Departments_DepartmentId",
                table: "EmployeeRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles");

            migrationBuilder.RenameTable(
                name: "EmployeeRoles",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoles_DepartmentId",
                table: "Roles",
                newName: "IX_Roles_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Departments_DepartmentId",
                table: "Roles",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
