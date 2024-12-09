using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucruIndividual.Migrations
{
    /// <inheritdoc />
    public partial class migration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friendRelations_users_idUser1",
                table: "friendRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_friendRelations_users_idUser2",
                table: "friendRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_friendRelations",
                table: "friendRelations");

            migrationBuilder.DropIndex(
                name: "IX_friendRelations_idUser2",
                table: "friendRelations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthDay",
                table: "profiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "friendRelations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "user1id",
                table: "friendRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "user2id",
                table: "friendRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_friendRelations",
                table: "friendRelations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_user1id",
                table: "friendRelations",
                column: "user1id");

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_user2id",
                table: "friendRelations",
                column: "user2id");

            migrationBuilder.AddForeignKey(
                name: "FK_friendRelations_users_user1id",
                table: "friendRelations",
                column: "user1id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);  // Changed to NoAction

            migrationBuilder.AddForeignKey(
                name: "FK_friendRelations_users_user2id",
                table: "friendRelations",
                column: "user2id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);  // Changed to NoAction
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friendRelations_users_user1id",
                table: "friendRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_friendRelations_users_user2id",
                table: "friendRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_friendRelations",
                table: "friendRelations");

            migrationBuilder.DropIndex(
                name: "IX_friendRelations_user1id",
                table: "friendRelations");

            migrationBuilder.DropIndex(
                name: "IX_friendRelations_user2id",
                table: "friendRelations");

            migrationBuilder.DropColumn(
                name: "id",
                table: "friendRelations");

            migrationBuilder.DropColumn(
                name: "user1id",
                table: "friendRelations");

            migrationBuilder.DropColumn(
                name: "user2id",
                table: "friendRelations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthDay",
                table: "profiles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_friendRelations",
                table: "friendRelations",
                columns: new[] { "idUser1", "idUser2" });

            migrationBuilder.CreateIndex(
                name: "IX_friendRelations_idUser2",
                table: "friendRelations",
                column: "idUser2");

            migrationBuilder.AddForeignKey(
                name: "FK_friendRelations_users_idUser1",
                table: "friendRelations",
                column: "idUser1",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_friendRelations_users_idUser2",
                table: "friendRelations",
                column: "idUser2",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
