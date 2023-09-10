using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class V12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sessionDurationMinutes",
                table: "DoctorShifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "shiftTitle",
                table: "DoctorShifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DoctorShiftDays",
                columns: table => new
                {
                    doctorShiftDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    days = table.Column<DateTime>(type: "datetime2", nullable: false),
                    weekDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctorShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShiftDays", x => x.doctorShiftDayId);
                    table.ForeignKey(
                        name: "FK_DoctorShiftDays_DoctorShifts_doctorShiftId",
                        column: x => x.doctorShiftId,
                        principalTable: "DoctorShifts",
                        principalColumn: "doctorShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorShiftDayTimes",
                columns: table => new
                {
                    doctorShiftDayTimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    toTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    doctorShiftDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShiftDayTimes", x => x.doctorShiftDayTimeId);
                    table.ForeignKey(
                        name: "FK_DoctorShiftDayTimes_DoctorShiftDays_doctorShiftDayId",
                        column: x => x.doctorShiftDayId,
                        principalTable: "DoctorShiftDays",
                        principalColumn: "doctorShiftDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftDays_doctorShiftId",
                table: "DoctorShiftDays",
                column: "doctorShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftDayTimes_doctorShiftDayId",
                table: "DoctorShiftDayTimes",
                column: "doctorShiftDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorShiftDayTimes");

            migrationBuilder.DropTable(
                name: "DoctorShiftDays");

            migrationBuilder.DropColumn(
                name: "sessionDurationMinutes",
                table: "DoctorShifts");

            migrationBuilder.DropColumn(
                name: "shiftTitle",
                table: "DoctorShifts");
        }
    }
}
