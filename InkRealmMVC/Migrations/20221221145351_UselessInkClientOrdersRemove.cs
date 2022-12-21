using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class UselessInkClientOrdersRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ink_client_orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ink_client_orders",
                columns: table => new
                {
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    orderid = table.Column<Guid>(name: "order_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_client_orders", x => new { x.clientid, x.orderid });
                });
        }
    }
}
