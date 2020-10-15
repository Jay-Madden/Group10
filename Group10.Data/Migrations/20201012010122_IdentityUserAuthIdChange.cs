using Microsoft.EntityFrameworkCore.Migrations;

namespace Group10.Data.Migrations
{
    public partial class IdentityUserAuthIdChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "AspNetUsers");
        }
    }
}
