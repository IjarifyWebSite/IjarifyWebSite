using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IjarifySystemDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCatigoryInAmenity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Catigory",
                table: "amenities",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catigory",
                table: "amenities");
        }
    }
}
