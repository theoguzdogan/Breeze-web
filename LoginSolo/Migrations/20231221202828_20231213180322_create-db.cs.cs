using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSolo.Migrations
{
    /// <inheritdoc />
    public partial class _20231213180322_createdbcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cities",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cities",
                table: "AspNetUsers");
        }
    }
}
