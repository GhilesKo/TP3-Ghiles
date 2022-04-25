using Microsoft.EntityFrameworkCore.Migrations;

namespace VoyageAPI.Migrations
{
    public partial class addedPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Voyages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Voyages");
        }
    }
}
