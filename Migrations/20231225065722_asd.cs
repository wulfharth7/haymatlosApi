using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haymatlosApi.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "posts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "content",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "posts");
        }
    }
}
