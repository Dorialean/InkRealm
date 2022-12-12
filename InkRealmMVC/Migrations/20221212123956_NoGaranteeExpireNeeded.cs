using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class NoGaranteeExpireNeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "garantee_to_add_days",
                table: "ink_products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "garantee_to_add_days",
                table: "ink_products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
