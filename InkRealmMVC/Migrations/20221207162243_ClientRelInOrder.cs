using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class ClientRelInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_id",
                table: "orders");
        }
    }
}
