using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.Migrations
{
    public partial class temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostClaps_BlogPosts_BlogPostId",
                table: "BlogPostClaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostClaps",
                table: "BlogPostClaps");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostClaps_BlogPostId",
                table: "BlogPostClaps");

            migrationBuilder.DropColumn(
                name: "BlogPostClapsId",
                table: "BlogPostClaps");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "BlogPostClaps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostClaps",
                table: "BlogPostClaps",
                columns: new[] { "BlogPostId", "ClapId" });

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FollowersUserId = table.Column<int>(type: "int", nullable: false),
                    FollowingUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FollowersUserId, x.FollowingUserId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_FollowersUserId",
                        column: x => x.FollowersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_FollowingUserId",
                        column: x => x.FollowingUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_FollowingUserId",
                table: "UserUser",
                column: "FollowingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostClaps_BlogPosts_BlogPostId",
                table: "BlogPostClaps",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_BlogPostId",
                table: "Comments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostClaps_BlogPosts_BlogPostId",
                table: "BlogPostClaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_BlogPostId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BlogPostId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostClaps",
                table: "BlogPostClaps");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "BlogPostClaps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostClapsId",
                table: "BlogPostClaps",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostClaps",
                table: "BlogPostClaps",
                column: "BlogPostClapsId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostClaps_BlogPostId",
                table: "BlogPostClaps",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostClaps_BlogPosts_BlogPostId",
                table: "BlogPostClaps",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
