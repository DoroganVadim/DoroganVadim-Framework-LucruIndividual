using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "posts");

            migrationBuilder.AddColumn<int>(
                name: "idImage",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "postImages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postImages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_idImage",
                table: "posts",
                column: "idImage");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_postImages_idImage",
                table: "posts",
                column: "idImage",
                principalTable: "postImages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_postImages_idImage",
                table: "posts");

            migrationBuilder.DropTable(
                name: "postImages");

            migrationBuilder.DropIndex(
                name: "IX_posts_idImage",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "idImage",
                table: "posts");

            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
