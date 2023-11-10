using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class changeNameFromCompleteDeregistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_completeDeregistrations",
                table: "completeDeregistrations");

            migrationBuilder.RenameTable(
                name: "completeDeregistrations",
                newName: "CompleteDeregistrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompleteDeregistrations",
                table: "CompleteDeregistrations",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompleteDeregistrations",
                table: "CompleteDeregistrations");

            migrationBuilder.RenameTable(
                name: "CompleteDeregistrations",
                newName: "completeDeregistrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_completeDeregistrations",
                table: "completeDeregistrations",
                column: "Id");
        }
    }
}
