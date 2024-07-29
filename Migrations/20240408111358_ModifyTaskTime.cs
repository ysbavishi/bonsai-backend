using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonsaiBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTaskTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTime_Task_TasksTaskId",
                table: "TaskTime");

            migrationBuilder.DropIndex(
                name: "IX_TaskTime_TasksTaskId",
                table: "TaskTime");

            migrationBuilder.DropColumn(
                name: "TasksTaskId",
                table: "TaskTime");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTime_TaskId",
                table: "TaskTime",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTime_Task_TaskId",
                table: "TaskTime",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTime_Task_TaskId",
                table: "TaskTime");

            migrationBuilder.DropIndex(
                name: "IX_TaskTime_TaskId",
                table: "TaskTime");

            migrationBuilder.AddColumn<int>(
                name: "TasksTaskId",
                table: "TaskTime",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskTime_TasksTaskId",
                table: "TaskTime",
                column: "TasksTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTime_Task_TasksTaskId",
                table: "TaskTime",
                column: "TasksTaskId",
                principalTable: "Task",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
