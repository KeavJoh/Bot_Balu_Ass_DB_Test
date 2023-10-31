using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class changeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_deregistrations",
                table: "deregistrations");

            migrationBuilder.RenameTable(
                name: "deregistrations",
                newName: "Deregistrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deregistrations",
                table: "Deregistrations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Deregistrations",
                table: "Deregistrations");

            migrationBuilder.RenameTable(
                name: "Deregistrations",
                newName: "deregistrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deregistrations",
                table: "deregistrations",
                column: "Id");
        }
    }
}
