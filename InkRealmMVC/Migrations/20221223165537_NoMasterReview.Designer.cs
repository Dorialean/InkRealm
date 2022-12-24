﻿// <auto-generated />
using System;
using InkRealmMVC.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InkRealmMVC.Migrations
{
    [DbContext(typeof(InkRealmContext))]
    [Migration("20221223165537_NoMasterReview")]
    partial class NoMasterReview
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.InkClient", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Email")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)")
                        .HasColumnName("email");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("father_name");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("first_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("login");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("mobile_phone");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text")
                        .HasColumnName("photo_link");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("registered");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("surname");

                    b.HasKey("ClientId");

                    b.ToTable("ink_clients");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.InkClientService", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<int>("MasterId")
                        .HasColumnType("integer")
                        .HasColumnName("master_id");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.Property<string>("Progress")
                        .HasColumnType("text")
                        .HasColumnName("progress");

                    b.Property<DateTime>("ServiceDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("service_date");

                    b.Property<DateTime?>("ServiceFinished")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("service_finished");

                    b.HasKey("ClientId", "MasterId", "ServiceId");

                    b.ToTable("ink_client_services");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.InkMaster", b =>
                {
                    b.Property<int>("MasterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("master_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MasterId"));

                    b.Property<int?>("ExperienceYears")
                        .HasColumnType("integer")
                        .HasColumnName("experience_years");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("father_name");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("first_name");

                    b.Property<string>("InkPost")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ink_post");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("login");

                    b.Property<string>("OtherInfo")
                        .HasColumnType("jsonb")
                        .HasColumnName("other_info");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text")
                        .HasColumnName("photo_link");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("registered");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("second_name");

                    b.Property<Guid>("StudioId")
                        .HasColumnType("uuid")
                        .HasColumnName("studio_id");

                    b.HasKey("MasterId");

                    b.ToTable("ink_masters");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.InkProduct", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("EachPrice")
                        .HasColumnType("money")
                        .HasColumnName("each_price");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text")
                        .HasColumnName("photo_link");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("title");

                    b.HasKey("ProductId");

                    b.ToTable("ink_products");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.InkService", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal?>("MaxPrice")
                        .HasColumnType("money")
                        .HasColumnName("max_price");

                    b.Property<decimal>("MinPrice")
                        .HasColumnType("money")
                        .HasColumnName("min_price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("title");

                    b.HasKey("ServiceId");

                    b.ToTable("ink_services");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.InkSupply", b =>
                {
                    b.Property<Guid>("SuplId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("supl_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("title");

                    b.HasKey("SuplId");

                    b.ToTable("ink_supplies");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.MastersServices", b =>
                {
                    b.Property<int>("MasterId")
                        .HasColumnType("integer")
                        .HasColumnName("master_id");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("service_id");

                    b.HasKey("MasterId", "ServiceId");

                    b.ToTable("masters_services");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.MastersSupply", b =>
                {
                    b.Property<int>("MasterId")
                        .HasColumnType("integer")
                        .HasColumnName("master_id");

                    b.Property<Guid>("SuplId")
                        .HasColumnType("uuid")
                        .HasColumnName("supl_id");

                    b.Property<int?>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.HasKey("MasterId", "SuplId");

                    b.ToTable("masters_supplies");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date");

                    b.Property<DateTime?>("FinishedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("finished_date");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.HasKey("OrderId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("InkRealmMVC.Models.DbModels.Studio", b =>
                {
                    b.Property<Guid>("StudioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("studio_id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text")
                        .HasColumnName("photo_link");

                    b.Property<decimal?>("RentalPricePerMonth")
                        .HasColumnType("money")
                        .HasColumnName("rental_price_per_month");

                    b.HasKey("StudioId");

                    b.ToTable("studios");
                });
#pragma warning restore 612, 618
        }
    }
}
