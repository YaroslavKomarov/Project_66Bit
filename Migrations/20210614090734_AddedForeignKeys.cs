using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_66_bit.Migrations
{
    public partial class AddedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Projects_ProjectId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Modules_moduleId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "moduleId",
                table: "Tasks",
                newName: "ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_moduleId",
                table: "Tasks",
                newName: "IX_Tasks_ModuleId");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Customers",
                newName: "Email");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Projects_ProjectId",
                table: "Modules",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Modules_ModuleId",
                table: "Tasks",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Projects_ProjectId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Modules_ModuleId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "Tasks",
                newName: "moduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ModuleId",
                table: "Tasks",
                newName: "IX_Tasks_moduleId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "Mail");

            migrationBuilder.AlterColumn<int>(
                name: "moduleId",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Modules",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Projects_ProjectId",
                table: "Modules",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Modules_moduleId",
                table: "Tasks",
                column: "moduleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
