using Microsoft.EntityFrameworkCore.Migrations;

namespace TqlPoWebApi.Migrations
{
    public partial class PolinepropertiesandDBContectforEF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "polines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    POId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_polines_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_polines_POs_POId",
                        column: x => x.POId,
                        principalTable: "POs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_polines_ItemId",
                table: "polines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_polines_POId",
                table: "polines",
                column: "POId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "polines");
        }
    }
}
