using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class NoPKeyOnMastersUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_masters_supplies",
                table: "masters_supplies");

            migrationBuilder.AlterColumn<int>(
                name: "master_id",
                table: "masters_supplies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "masters_services",
                columns: table => new
                {
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: false),
                    serviceid = table.Column<int>(name: "service_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "masters_services");

            migrationBuilder.AlterColumn<int>(
                name: "master_id",
                table: "masters_supplies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_masters_supplies",
                table: "masters_supplies",
                column: "master_id");
        }
    }
}
