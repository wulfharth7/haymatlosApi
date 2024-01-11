using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haymatlosApi.Migrations
{
    /// <inheritdoc />
    public partial class tttttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "commenterUsername",
                table: "comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "commenterUsername",
                table: "comments");
        }
    }
}
