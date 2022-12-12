using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class NoPropsInInkProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "props",
                table: "ink_products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "props",
                table: "ink_products",
                type: "jsonb",
                nullable: true);
        }
    }
}
