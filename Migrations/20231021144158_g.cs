using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haymatlosApi.Migrations
{
    /// <inheritdoc />
    public partial class g : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    salt = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true),
                    token = table.Column<string>(type: "text", nullable: true),
                    regDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    pkey_uuid_post = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    fkey_uuid_user = table.Column<Guid>(type: "uuid", nullable: true),
                    regDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    like = table.Column<short>(type: "smallint", nullable: true),
                    dislike = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("posts_pkey", x => x.pkey_uuid_post);
                    table.ForeignKey(
                        name: "fkey_uuid",
                        column: x => x.fkey_uuid_user,
                        principalTable: "users",
                        principalColumn: "uuid");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    pkey_uuid_comment = table.Column<Guid>(type: "uuid", nullable: false),
                    fkey_uuid_post = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    parentComment = table.Column<Guid>(type: "uuid", nullable: true),
                    regDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    like = table.Column<short>(type: "smallint", nullable: true),
                    dislike = table.Column<short>(type: "smallint", nullable: true),
                    fkey_uuid_user = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comments_pkey", x => x.pkey_uuid_comment);
                    table.ForeignKey(
                        name: "fkey_uuid_comment",
                        column: x => x.fkey_uuid_post,
                        principalTable: "posts",
                        principalColumn: "pkey_uuid_post");
                    table.ForeignKey(
                        name: "fkey_uuid_user_comment",
                        column: x => x.fkey_uuid_user,
                        principalTable: "users",
                        principalColumn: "uuid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_fkey_uuid_post",
                table: "comments",
                column: "fkey_uuid_post");

            migrationBuilder.CreateIndex(
                name: "IX_comments_fkey_uuid_user",
                table: "comments",
                column: "fkey_uuid_user");

            migrationBuilder.CreateIndex(
                name: "IX_posts_fkey_uuid_user",
                table: "posts",
                column: "fkey_uuid_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
