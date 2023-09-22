using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1Attempt4.Migrations
{
    /// <inheritdoc />
    public partial class profOrStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profOrStudent",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profOrStudent",
                table: "AspNetUsers");
        }
    }
}
