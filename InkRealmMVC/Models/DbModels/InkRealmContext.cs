using System;
using System.Collections.Generic;
using InkRealmMVC.Migrations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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

    public virtual DbSet<InkClientService> InkClientServices { get; set; }
    
    public virtual DbSet<InkClientOrder> InkClientOrders { get; set; }

    public virtual DbSet<InkClient> InkClients { get; set; }

    public virtual DbSet<InkMaster> InkMasters { get; set; }

    public virtual DbSet<InkProduct> InkProducts { get; set; }

    public virtual DbSet<InkService> InkServices { get; set; }

    public virtual DbSet<InkSupply> InkSupplies { get; set; }

    public virtual DbSet<MasterReviews> MasterReviews { get; set; }

    public virtual DbSet<MastersSupply> MastersSupplies { get; set; }

    public virtual DbSet<MastersServices> MastersServices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"));

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
