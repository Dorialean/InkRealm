using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class NoClientNeedsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_clients_needs",
                table: "clients_needs");

            migrationBuilder.DropColumn(
                name: "client_needs_id",
                table: "ink_clients");

            migrationBuilder.DropColumn(
                name: "clients_needs_id",
                table: "clients_needs");

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "clients_needs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients_needs",
                table: "clients_needs",
                column: "client_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_clients_needs",
                table: "clients_needs");

            migrationBuilder.AddColumn<Guid>(
                name: "client_needs_id",
                table: "ink_clients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "clients_needs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "clients_needs_id",
                table: "clients_needs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients_needs",
                table: "clients_needs",
                column: "clients_needs_id");
        }
    }
}
