using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class implementDeregistrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deregistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    DeregistrationAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeregistrationFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeregistrationTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeregistrationForOneDay = table.Column<bool>(type: "bit", nullable: false),
                    DeregistrationPerformedFromParents = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deregistrations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deregistrations");
        }
    }
}
