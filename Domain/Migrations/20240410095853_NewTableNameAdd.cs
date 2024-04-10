using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTC.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewTableNameAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewTaskNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTaskNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TasksSaving",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: false),
                    TaskHour = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaskOpening = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaskClosing = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksSaving", x => x.TaskId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewTaskNames");

            migrationBuilder.DropTable(
                name: "TasksSaving");
        }
    }
}
