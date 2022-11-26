using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InkRealmMVC.Models.DbModels;

public partial class InkRealmContext : DbContext
{
    public InkRealmContext()
    {
    }

    public InkRealmContext(DbContextOptions<InkRealmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientsNeed> ClientsNeeds { get; set; }

    public virtual DbSet<InkClient> InkClients { get; set; }

    public virtual DbSet<InkMaster> InkMasters { get; set; }

    public virtual DbSet<InkProduct> InkProducts { get; set; }

    public virtual DbSet<InkService> InkServices { get; set; }

    public virtual DbSet<InkSupply> InkSupplies { get; set; }

    public virtual DbSet<MasterReview> MasterReviews { get; set; }

    public virtual DbSet<MastersSupply> MastersSupplies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("profession", new[] { "sketch designer", "tatoo master", "pirsing master" });

        modelBuilder.Entity<ClientsNeed>(entity =>
        {
            entity.HasKey(e => e.ClientsNeedsId).HasName("clients_needs_pkey");

            entity.Property(e => e.ClientsNeedsId).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsNeeds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clients_needs_client_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.ClientsNeeds).HasConstraintName("clients_needs_order_id_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.ClientsNeeds).HasConstraintName("clients_needs_service_id_fkey");
        });

        modelBuilder.Entity<InkClient>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("ink_clients_pkey");

            entity.Property(e => e.Registered).HasDefaultValueSql("now()");

            entity.HasOne(d => d.ClientNeeds).WithMany(p => p.InkClients).HasConstraintName("fk_clients_needs_id");
        });

        modelBuilder.Entity<InkMaster>(entity =>
        {
            entity.HasKey(e => e.MasterId).HasName("ink_masters_pkey");

            entity.Property(e => e.MasterId).HasDefaultValueSql("nextval('ink_masters_seq'::regclass)");
            entity.Property(e => e.Registered).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Service).WithMany(p => p.InkMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ink_masters_service_id_fkey");

            entity.HasOne(d => d.Studio).WithMany(p => p.InkMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ink_masters_studio_id_fkey");
        });

        modelBuilder.Entity<InkProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("ink_products_pkey");

            entity.Property(e => e.ProductId).HasDefaultValueSql("gen_random_uuid()");
        });

        modelBuilder.Entity<InkService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("ink_services_pkey");
        });

        modelBuilder.Entity<InkSupply>(entity =>
        {
            entity.HasKey(e => e.SuplId).HasName("ink_supplies_pkey");

            entity.Property(e => e.SuplId).HasDefaultValueSql("gen_random_uuid()");
        });

        modelBuilder.Entity<MasterReview>(entity =>
        {
            entity.HasKey(e => e.MasterReviewId).HasName("master_reviews_pkey");

            entity.Property(e => e.MasterReviewId).HasDefaultValueSql("nextval('masters_reviews_seq'::regclass)");

            entity.HasOne(d => d.Client).WithMany(p => p.MasterReviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_reviews_client_id_fkey");

            entity.HasOne(d => d.Master).WithMany(p => p.MasterReviewMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_reviews_master_id_fkey");

            entity.HasOne(d => d.MasterReviewNavigation).WithOne(p => p.MasterReviewMasterReviewNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_reviews_master_review_id_fkey");
        });

        modelBuilder.Entity<MastersSupply>(entity =>
        {
            entity.HasKey(e => e.MasterId).HasName("masters_supplies_pkey");

            entity.Property(e => e.MasterId).ValueGeneratedNever();

            entity.HasOne(d => d.Master).WithOne(p => p.MastersSupply)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("masters_supplies_master_id_fkey");

            entity.HasOne(d => d.Supl).WithMany(p => p.MastersSupplies).HasConstraintName("masters_supplies_supl_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.Property(e => e.OrderId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.CreateDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders).HasConstraintName("orders_product_id_fkey");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("studios_pkey");

            entity.Property(e => e.StudioId).HasDefaultValueSql("gen_random_uuid()");
        });
        modelBuilder.HasSequence("ink_masters_seq");
        modelBuilder.HasSequence("masters_reviews_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
