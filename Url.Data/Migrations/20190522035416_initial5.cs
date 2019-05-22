using Microsoft.EntityFrameworkCore.Migrations;

namespace Url.Data.Migrations
{
    public partial class initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_url_Users_userid",
                table: "url");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "url",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_url_Users_userid",
                table: "url",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_url_Users_userid",
                table: "url");

            migrationBuilder.AlterColumn<int>(
                name: "userid",
                table: "url",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_url_Users_userid",
                table: "url",
                column: "userid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
