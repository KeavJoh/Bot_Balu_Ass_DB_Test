using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    /// <inheritdoc />
    public partial class implementParentsStringForChildModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Father",
                table: "Children",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mother",
                table: "Children",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Father",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Mother",
                table: "Children");
        }
    }
}
