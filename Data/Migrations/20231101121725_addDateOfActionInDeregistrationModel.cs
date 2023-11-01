using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class addDateOfActionInDeregistrationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAction",
                table: "Deregistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfAction",
                table: "Deregistrations");
        }
    }
}
