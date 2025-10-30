using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaCatalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MediaCatalogObject_Generic_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "MediaFileObjects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "MediaFileObjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "MediaFileObjects",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "MediaFileObjects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MediaFileObjects");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "MediaFileObjects");
        }
    }
}
