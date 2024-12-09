using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_postImages_idImage",
                table: "posts");

            migrationBuilder.AlterColumn<int>(
                name: "idImage",
                table: "posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_postImages_idImage",
                table: "posts",
                column: "idImage",
                principalTable: "postImages",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_postImages_idImage",
                table: "posts");

            migrationBuilder.AlterColumn<int>(
                name: "idImage",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_postImages_idImage",
                table: "posts",
                column: "idImage",
                principalTable: "postImages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
