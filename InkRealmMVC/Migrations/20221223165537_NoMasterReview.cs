using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class NoMasterReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "master_reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "master_reviews",
                columns: table => new
                {
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_reviews", x => new { x.clientid, x.masterid });
                });
        }
    }
}
