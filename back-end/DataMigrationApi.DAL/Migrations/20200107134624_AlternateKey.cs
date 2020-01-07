using Microsoft.EntityFrameworkCore.Migrations;

namespace DataMigrationApi.DAL.Migrations
{
    public partial class AlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Identity",
                table: "Users",
                column: "Identity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Identity",
                table: "Users");
        }
    }
}
