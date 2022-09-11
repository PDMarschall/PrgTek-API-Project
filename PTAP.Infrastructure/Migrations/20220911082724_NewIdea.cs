using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PTAP.Infrastructure.Migrations
{
    public partial class NewIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Quote");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Quote",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
