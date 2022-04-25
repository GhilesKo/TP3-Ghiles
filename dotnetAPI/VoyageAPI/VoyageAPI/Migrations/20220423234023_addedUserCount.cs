using Microsoft.EntityFrameworkCore.Migrations;

namespace VoyageAPI.Migrations
{
    public partial class addedUserCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersCount",
                table: "Voyages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersCount",
                table: "Voyages");
        }
    }
}
