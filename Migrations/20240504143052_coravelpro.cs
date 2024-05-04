using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoravelExample.Migrations
{
    /// <inheritdoc />
    public partial class coravelpro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coravel_JobHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypeFullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Failed = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coravel_JobHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coravel_ScheduledJobHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypeFullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Failed = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coravel_ScheduledJobHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coravel_ScheduledJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvocableFullPath = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CronExpression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Days = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventOverlapping = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TimeZoneInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunOnDedicatedThread = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coravel_ScheduledJobs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coravel_JobHistory_EndedAt",
                table: "Coravel_JobHistory",
                column: "EndedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Coravel_ScheduledJobHistory_EndedAt",
                table: "Coravel_ScheduledJobHistory",
                column: "EndedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Coravel_ScheduledJobs_Active",
                table: "Coravel_ScheduledJobs",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_Coravel_ScheduledJobs_InvocableFullPath",
                table: "Coravel_ScheduledJobs",
                column: "InvocableFullPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coravel_JobHistory");

            migrationBuilder.DropTable(
                name: "Coravel_ScheduledJobHistory");

            migrationBuilder.DropTable(
                name: "Coravel_ScheduledJobs");
        }
    }
}
