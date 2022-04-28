using Microsoft.EntityFrameworkCore.Migrations;

namespace VoyageAPI.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 0, "1d5df416-5416-4baa-8e2e-05d7e49b8024", "jim@test.com", false, false, null, "JIM@TEST.COM", "JIM@TEST.COM", "AQAAAAEAACcQAAAAEHIp0nkQdH/V3JW85ysxj1d9RUrBMSkD32iRWnBS50c76cx6p4ZYy+tmKbmwwzObWA==", null, false, "a0d77d5c-8feb-4d5e-b329-f90e089c30e5", false, "jim@test.com" },
                    { "51e6e638-55ea-41ab-829d-7129226e1200", 0, "96845fca-1b21-4bd4-86a0-35e5f797fbbf", "tom@test.com", false, false, null, "TOM@TEST.COM", "TOM@TEST.COM", "AQAAAAEAACcQAAAAEPAA9rtgoNGlbePdmruG4PtJC4CGt2W+dLGIrxSbemdi4L/LPmUtU3KwCbDRRegpfg==", null, false, "b0d3c529-20fe-41db-9ab9-37d6355d5d48", false, "tom@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Voyages",
                columns: new[] { "Id", "Pays", "Photo", "Public", "UsersCount" },
                values: new object[,]
                {
                    { 1, "Amerique", "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg", true, 0 },
                    { 2, "Brazil", "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg", false, 0 },
                    { 3, "Chine", "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg", false, 0 },
                    { 4, "Algerie", "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg", true, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserVoyage",
                columns: new[] { "UsersId", "VoyagesId" },
                values: new object[,]
                {
                    { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 1 },
                    { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 2 },
                    { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 3 },
                    { "51e6e638-55ea-41ab-829d-7129226e1200", 3 },
                    { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 4 },
                    { "51e6e638-55ea-41ab-829d-7129226e1200", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "51e6e638-55ea-41ab-829d-7129226e1200", 3 });

            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "51e6e638-55ea-41ab-829d-7129226e1200", 4 });

            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 1 });

            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 2 });

            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 3 });

            migrationBuilder.DeleteData(
                table: "UserVoyage",
                keyColumns: new[] { "UsersId", "VoyagesId" },
                keyValues: new object[] { "9cdbec30-c419-4f10-9cd4-0e764aee839e", 4 });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51e6e638-55ea-41ab-829d-7129226e1200");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9cdbec30-c419-4f10-9cd4-0e764aee839e");

            migrationBuilder.DeleteData(
                table: "Voyages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Voyages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Voyages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Voyages",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
