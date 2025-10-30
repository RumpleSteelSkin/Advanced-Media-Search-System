using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaCatalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MediaFileObject_Title_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MediaFileObjects",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "MediaFileObjects");
        }
    }
}
