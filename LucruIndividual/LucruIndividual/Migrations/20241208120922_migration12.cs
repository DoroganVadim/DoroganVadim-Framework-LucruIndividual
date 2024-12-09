using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendRelations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "friendRelations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user1id = table.Column<int>(type: "int", nullable: false),
                    user2id = table.Column<int>(type: "int", nullable: false),
                    idUser1 = table.Column<int>(type: "int", nullable: false),
                    idUser2 = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendRelations", x => x.id);
                    table.ForeignKey(
                        name: "FK_friendRelations_users_user1id",
                        column: x => x.user1id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_friendRelations_users_user2id",
                        column: x => x.user2id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_user1id",
                table: "friendRelations",
                column: "user1id");

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_user2id",
                table: "friendRelations",
                column: "user2id");
        }
    }
}
