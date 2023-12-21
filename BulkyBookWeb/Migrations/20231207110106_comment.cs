using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookWeb.Migrations
{
    public partial class comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BookViews_BookViewId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "BookViews");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BookViewId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BookViewId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookViewId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookViews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookViewId",
                table: "Comments",
                column: "BookViewId");

            migrationBuilder.CreateIndex(
                name: "IX_BookViews_BookId",
                table: "BookViews",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BookViews_BookViewId",
                table: "Comments",
                column: "BookViewId",
                principalTable: "BookViews",
                principalColumn: "Id");
        }
    }
}
