using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class V13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DoctorShiftDayTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DoctorShiftDayTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DoctorShiftDayTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "DoctorShiftDayTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "DoctorShiftDayTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DoctorShiftDayTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DoctorShiftDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DoctorShiftDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DoctorShiftDays",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "DoctorShiftDays",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "DoctorShiftDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DoctorShiftDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DoctorShiftDayTimes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DoctorShiftDays");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoctorShiftDays");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DoctorShiftDays");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "DoctorShiftDays");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "DoctorShiftDays");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DoctorShiftDays");
        }
    }
}
