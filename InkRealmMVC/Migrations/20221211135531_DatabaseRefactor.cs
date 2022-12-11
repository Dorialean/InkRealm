using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients_needs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_master_reviews",
                table: "master_reviews");

            migrationBuilder.DropColumn(
                name: "master_review_id",
                table: "master_reviews");

            migrationBuilder.AddColumn<string>(
                name: "photo_link",
                table: "studios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "finished_date",
                table: "orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "garantee_to_add_days",
                table: "ink_products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "photo_link",
                table: "ink_clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_masters_supplies",
                table: "masters_supplies",
                columns: new[] { "master_id", "supl_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_masters_services",
                table: "masters_services",
                columns: new[] { "master_id", "service_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_master_reviews",
                table: "master_reviews",
                columns: new[] { "client_id", "master_id" });

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

            migrationBuilder.CreateTable(
                name: "ink_client_services",
                columns: table => new
                {
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: false),
                    serviceid = table.Column<int>(name: "service_id", type: "integer", nullable: false),
                    servicedate = table.Column<DateTime>(name: "service_date", type: "timestamp without time zone", nullable: false),
                    servicefinished = table.Column<DateTime>(name: "service_finished", type: "timestamp without time zone", nullable: true),
                    progress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_client_services", x => new { x.clientid, x.masterid, x.serviceid });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ink_client_orders");

            migrationBuilder.DropTable(
                name: "ink_client_services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_masters_supplies",
                table: "masters_supplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_masters_services",
                table: "masters_services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_master_reviews",
                table: "master_reviews");

            migrationBuilder.DropColumn(
                name: "photo_link",
                table: "studios");

            migrationBuilder.DropColumn(
                name: "finished_date",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "garantee_to_add_days",
                table: "ink_products");

            migrationBuilder.DropColumn(
                name: "photo_link",
                table: "ink_clients");

            migrationBuilder.AddColumn<int>(
                name: "master_review_id",
                table: "master_reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_master_reviews",
                table: "master_reviews",
                column: "master_review_id");

            migrationBuilder.CreateTable(
                name: "clients_needs",
                columns: table => new
                {
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: true),
                    orderid = table.Column<Guid>(name: "order_id", type: "uuid", nullable: true),
                    servicedate = table.Column<DateTime>(name: "service_date", type: "timestamp without time zone", nullable: true),
                    serviceid = table.Column<int>(name: "service_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients_needs", x => x.clientid);
                });
        }
    }
}
