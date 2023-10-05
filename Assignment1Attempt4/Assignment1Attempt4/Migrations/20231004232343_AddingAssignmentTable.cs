using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1Attempt4.Migrations
{
    /// <inheritdoc />
    public partial class AddingAssignmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
                /*name: "profOrStudent",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");*/

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPoints = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            /*migrationBuilder.DropColumn(
                name: "profOrStudent",
                table: "AspNetUsers");*/
        }
    }
}
