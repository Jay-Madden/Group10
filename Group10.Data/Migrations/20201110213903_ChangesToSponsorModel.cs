using Microsoft.EntityFrameworkCore.Migrations;

namespace Group10.Data.Migrations
{
    public partial class ChangesToSponsorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_AspNetUsers_AppUserId1",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Sponsors_SponsorId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_AspNetUsers_AppUserId1",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_AppUserId1",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_AppUserId1",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_SponsorId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "SponsorId",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Sponsors",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Drivers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "DriverSponsor",
                columns: table => new
                {
                    DriversId = table.Column<int>(type: "integer", nullable: false),
                    SponsorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverSponsor", x => new { x.DriversId, x.SponsorsId });
                    table.ForeignKey(
                        name: "FK_DriverSponsor_Drivers_DriversId",
                        column: x => x.DriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverSponsor_Sponsors_SponsorsId",
                        column: x => x.SponsorsId,
                        principalTable: "Sponsors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_AppUserId",
                table: "Sponsors",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AppUserId",
                table: "Drivers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverSponsor_SponsorsId",
                table: "DriverSponsor",
                column: "SponsorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_AspNetUsers_AppUserId",
                table: "Drivers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_AspNetUsers_AppUserId",
                table: "Sponsors",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_AspNetUsers_AppUserId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_AspNetUsers_AppUserId",
                table: "Sponsors");

            migrationBuilder.DropTable(
                name: "DriverSponsor");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_AppUserId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_AppUserId",
                table: "Drivers");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Sponsors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Sponsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Sponsors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sponsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sponsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Sponsors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Drivers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Drivers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SponsorId",
                table: "Drivers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_AppUserId1",
                table: "Sponsors",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_AppUserId1",
                table: "Drivers",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_SponsorId",
                table: "Drivers",
                column: "SponsorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_AspNetUsers_AppUserId1",
                table: "Drivers",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Sponsors_SponsorId",
                table: "Drivers",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_AspNetUsers_AppUserId1",
                table: "Sponsors",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
