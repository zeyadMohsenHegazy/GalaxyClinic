using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class V23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "DoctorShifts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "DoctorShiftDayTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isCancelled",
                table: "DoctorShiftDays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "DoctorShifts");

            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "isCancelled",
                table: "DoctorShiftDays");
        }
    }
}
