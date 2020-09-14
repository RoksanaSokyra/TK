using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class AddLastnameToContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Contacts");
        }
    }
}
