using Microsoft.EntityFrameworkCore.Migrations;

namespace TqlPoWebApi.Migrations
{
    public partial class removecolumnfrompolines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "polines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "polines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
