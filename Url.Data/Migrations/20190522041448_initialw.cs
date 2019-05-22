using Microsoft.EntityFrameworkCore.Migrations;

namespace Url.Data.Migrations
{
    public partial class initialw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_url_Users_userid",
                table: "url");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_url_userid",
                table: "url");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.AddColumn<int>(
                name: "usersid",
                table: "url",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_url_usersid",
                table: "url",
                column: "usersid");

            migrationBuilder.AddForeignKey(
                name: "FK_url_users_usersid",
                table: "url",
                column: "usersid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_url_users_usersid",
                table: "url");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_url_usersid",
                table: "url");

            migrationBuilder.DropColumn(
                name: "usersid",
                table: "url");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_url_userid",
                table: "url",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_url_Users_userid",
                table: "url",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
