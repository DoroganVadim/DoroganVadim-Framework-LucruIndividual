using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_idUser",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "posts",
                newName: "idProfile");

            migrationBuilder.RenameIndex(
                name: "IX_posts_idUser",
                table: "posts",
                newName: "IX_posts_idProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_profiles_idProfile",
                table: "posts",
                column: "idProfile",
                principalTable: "profiles",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_profiles_idProfile",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "idProfile",
                table: "posts",
                newName: "idUser");

            migrationBuilder.RenameIndex(
                name: "IX_posts_idProfile",
                table: "posts",
                newName: "IX_posts_idUser");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_idUser",
                table: "posts",
                column: "idUser",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
