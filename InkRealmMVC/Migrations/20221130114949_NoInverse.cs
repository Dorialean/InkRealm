using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InkRealmMVC.Migrations
{
    /// <inheritdoc />
    public partial class NoInverse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients_needs",
                columns: table => new
                {
                    clientsneedsid = table.Column<Guid>(name: "clients_needs_id", type: "uuid", nullable: false),
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    serviceid = table.Column<int>(name: "service_id", type: "integer", nullable: true),
                    servicedate = table.Column<DateTime>(name: "service_date", type: "timestamp without time zone", nullable: true),
                    orderid = table.Column<Guid>(name: "order_id", type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients_needs", x => x.clientsneedsid);
                });

            migrationBuilder.CreateTable(
                name: "ink_clients",
                columns: table => new
                {
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(name: "first_name", type: "character varying(30)", maxLength: 30, nullable: false),
                    surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    fathername = table.Column<string>(name: "father_name", type: "character varying(30)", maxLength: 30, nullable: true),
                    mobilephone = table.Column<string>(name: "mobile_phone", type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    clientneedsid = table.Column<Guid>(name: "client_needs_id", type: "uuid", nullable: true),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<byte[]>(type: "bytea", nullable: false),
                    registered = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_clients", x => x.clientid);
                });

            migrationBuilder.CreateTable(
                name: "ink_masters",
                columns: table => new
                {
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(name: "first_name", type: "character varying(30)", maxLength: 30, nullable: false),
                    secondname = table.Column<string>(name: "second_name", type: "character varying(30)", maxLength: 30, nullable: false),
                    fathername = table.Column<string>(name: "father_name", type: "character varying(30)", maxLength: 30, nullable: true),
                    photolink = table.Column<string>(name: "photo_link", type: "text", nullable: true),
                    experienceyears = table.Column<int>(name: "experience_years", type: "integer", nullable: true),
                    otherinfo = table.Column<string>(name: "other_info", type: "jsonb", nullable: true),
                    studioid = table.Column<Guid>(name: "studio_id", type: "uuid", nullable: false),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<byte[]>(type: "bytea", nullable: false),
                    registered = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    inkpost = table.Column<string>(name: "ink_post", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_masters", x => x.masterid);
                });

            migrationBuilder.CreateTable(
                name: "ink_products",
                columns: table => new
                {
                    productid = table.Column<Guid>(name: "product_id", type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    eachprice = table.Column<decimal>(name: "each_price", type: "money", nullable: false),
                    props = table.Column<string>(type: "jsonb", nullable: true),
                    photolink = table.Column<string>(name: "photo_link", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_products", x => x.productid);
                });

            migrationBuilder.CreateTable(
                name: "ink_services",
                columns: table => new
                {
                    serviceid = table.Column<int>(name: "service_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    minprice = table.Column<decimal>(name: "min_price", type: "money", nullable: false),
                    maxprice = table.Column<decimal>(name: "max_price", type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_services", x => x.serviceid);
                });

            migrationBuilder.CreateTable(
                name: "ink_supplies",
                columns: table => new
                {
                    suplid = table.Column<Guid>(name: "supl_id", type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ink_supplies", x => x.suplid);
                });

            migrationBuilder.CreateTable(
                name: "master_reviews",
                columns: table => new
                {
                    masterreviewid = table.Column<int>(name: "master_review_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(name: "client_id", type: "integer", nullable: false),
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    review = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_reviews", x => x.masterreviewid);
                });

            migrationBuilder.CreateTable(
                name: "masters_supplies",
                columns: table => new
                {
                    masterid = table.Column<int>(name: "master_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    suplid = table.Column<Guid>(name: "supl_id", type: "uuid", nullable: true),
                    amount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masters_supplies", x => x.masterid);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderid = table.Column<Guid>(name: "order_id", type: "uuid", nullable: false),
                    productid = table.Column<Guid>(name: "product_id", type: "uuid", nullable: true),
                    createdate = table.Column<DateTime>(name: "create_date", type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderid);
                });

            migrationBuilder.CreateTable(
                name: "studios",
                columns: table => new
                {
                    studioid = table.Column<Guid>(name: "studio_id", type: "uuid", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    rentalpricepermonth = table.Column<decimal>(name: "rental_price_per_month", type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studios", x => x.studioid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients_needs");

            migrationBuilder.DropTable(
                name: "ink_clients");

            migrationBuilder.DropTable(
                name: "ink_masters");

            migrationBuilder.DropTable(
                name: "ink_products");

            migrationBuilder.DropTable(
                name: "ink_services");

            migrationBuilder.DropTable(
                name: "ink_supplies");

            migrationBuilder.DropTable(
                name: "master_reviews");

            migrationBuilder.DropTable(
                name: "masters_supplies");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "studios");
        }
    }
}
