using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "friendRelations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user1Id = table.Column<int>(type: "int", nullable: false),
                    user2Id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendRelations", x => x.id);
                    table.ForeignKey(
                        name: "FK_friendRelations_users_user1Id",
                        column: x => x.user1Id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_friendRelations_users_user2Id",
                        column: x => x.user2Id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_user1Id",
                table: "friendRelations",
                column: "user1Id");

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_user2Id",
                table: "friendRelations",
                column: "user2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendRelations");
        }
    }
}
