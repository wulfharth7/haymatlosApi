using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haymatlosApi.Migrations
{
    /// <inheritdoc />
    public partial class ttttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "CommentCount",
                table: "posts",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "posterUsername",
                table: "posts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "posterUsername",
                table: "posts");
        }
    }
}
