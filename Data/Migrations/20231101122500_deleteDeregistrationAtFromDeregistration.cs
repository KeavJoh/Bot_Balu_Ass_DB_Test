using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class deleteDeregistrationAtFromDeregistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeregistrationAt",
                table: "Deregistrations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeregistrationFrom",
                table: "Deregistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeregistrationFrom",
                table: "Deregistrations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeregistrationAt",
                table: "Deregistrations",
                type: "datetime2",
                nullable: true);
        }
    }
}
