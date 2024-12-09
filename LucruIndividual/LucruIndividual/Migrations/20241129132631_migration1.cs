using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "friendRelations",
                columns: table => new
                {
                    idUser1 = table.Column<int>(type: "int", nullable: false),
                    idUser2 = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendRelations", x => new { x.idUser1, x.idUser2 });
                    table.ForeignKey(
                        name: "FK_friendRelations_users_idUser1",
                        column: x => x.idUser1,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_friendRelations_users_idUser2",
                        column: x => x.idUser2,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idUser = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    postText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_posts_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    birthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    aboutUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imageProfilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surrname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sex = table.Column<int>(type: "int", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.userId);
                    table.ForeignKey(
                        name: "FK_profiles_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_idUser2",
                table: "friendRelations",
                column: "idUser2");

            migrationBuilder.CreateIndex(
                name: "IX_posts_idUser",
                table: "posts",
                column: "idUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendRelations");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
