using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1Attempt4.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsGraded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGraded",
                table: "StudentSubmitsAssignment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGraded",
                table: "StudentSubmitsAssignment");
        }
    }
}
