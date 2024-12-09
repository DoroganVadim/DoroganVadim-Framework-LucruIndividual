using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_postImages_profileImageid",
                table: "profiles");

            migrationBuilder.AlterColumn<string>(
                name: "postText",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProfileImage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImage", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_ProfileImage_profileImageid",
                table: "profiles",
                column: "profileImageid",
                principalTable: "ProfileImage",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_ProfileImage_profileImageid",
                table: "profiles");

            migrationBuilder.DropTable(
                name: "ProfileImage");

            migrationBuilder.AlterColumn<string>(
                name: "postText",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_postImages_profileImageid",
                table: "profiles",
                column: "profileImageid",
                principalTable: "postImages",
                principalColumn: "id");
        }
    }
}
