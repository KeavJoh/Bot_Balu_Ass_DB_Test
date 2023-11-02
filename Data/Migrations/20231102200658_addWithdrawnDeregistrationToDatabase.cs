using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class addWithdrawnDeregistrationToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WithdrawnDeregistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    DeregistrationReason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeregistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfDeregistrationAction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfWithdrawn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeregistrationPerformedFromParents = table.Column<bool>(type: "bit", nullable: false),
                    WithdrawnPerformedFromParents = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawnDeregistrations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WithdrawnDeregistrations");
        }
    }
}
