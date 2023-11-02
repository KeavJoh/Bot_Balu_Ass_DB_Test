using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class changeDeregistrationsTableToNewStructureOfData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeregistrationForOneDay",
                table: "Deregistrations");

            migrationBuilder.DropColumn(
                name: "DeregistrationTo",
                table: "Deregistrations");

            migrationBuilder.RenameColumn(
                name: "DeregistrationFrom",
                table: "Deregistrations",
                newName: "DeregistrationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeregistrationDate",
                table: "Deregistrations",
                newName: "DeregistrationFrom");

            migrationBuilder.AddColumn<bool>(
                name: "DeregistrationForOneDay",
                table: "Deregistrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeregistrationTo",
                table: "Deregistrations",
                type: "datetime2",
                nullable: true);
        }
    }
}
