using Microsoft.EntityFrameworkCore.Migrations;

namespace VoyageAPI.Migrations
{
    public partial class addedVoyagePublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Voyages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserVoyage",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoyagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoyage", x => new { x.UsersId, x.VoyagesId });
                    table.ForeignKey(
                        name: "FK_UserVoyage_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoyage_Voyages_VoyagesId",
                        column: x => x.VoyagesId,
                        principalTable: "Voyages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVoyage_VoyagesId",
                table: "UserVoyage",
                column: "VoyagesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVoyage");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "Voyages");
        }
    }
}
