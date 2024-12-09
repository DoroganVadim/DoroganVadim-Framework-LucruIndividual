using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageProfilePath",
                table: "profiles");

            migrationBuilder.AddColumn<int>(
                name: "profileImageid",
                table: "profiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "extension",
                table: "postImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_profiles_profileImageid",
                table: "profiles",
                column: "profileImageid");

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_postImages_profileImageid",
                table: "profiles",
                column: "profileImageid",
                principalTable: "postImages",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_postImages_profileImageid",
                table: "profiles");

            migrationBuilder.DropIndex(
                name: "IX_profiles_profileImageid",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "profileImageid",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "extension",
                table: "postImages");

            migrationBuilder.AddColumn<string>(
                name: "imageProfilePath",
                table: "profiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
