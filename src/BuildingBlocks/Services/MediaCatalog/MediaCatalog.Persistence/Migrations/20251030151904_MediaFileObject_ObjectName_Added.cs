using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaCatalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MediaFileObject_ObjectName_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObjectName",
                table: "MediaFileObjects",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectName",
                table: "MediaFileObjects");
        }
    }
}
