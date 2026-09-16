using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WMS.Entities;

public partial class WarehouseDbContext : DbContext
{
    public WarehouseDbContext()
    {
    }

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Instore> Instores { get; set; }

    public virtual DbSet<InstoreOrder> InstoreOrders { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Outstore> Outstores { get; set; }

    public virtual DbSet<OutstoreOrder> OutstoreOrders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.ToTable("Cargo");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Tag).HasMaxLength(550);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Company).HasMaxLength(250);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Tag).HasMaxLength(550);
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Instore>(entity =>
        {
            entity.ToTable("Instore");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Tag).HasMaxLength(550);
        });

        modelBuilder.Entity<InstoreOrder>(entity =>
        {
            entity.ToTable("InstoreOrder");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Password)
                .HasMaxLength(120)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Outstore>(entity =>
        {
            entity.ToTable("Outstore");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Tag).HasMaxLength(550);
        });

        modelBuilder.Entity<OutstoreOrder>(entity =>
        {
            entity.ToTable("OutstoreOrder");

            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Company).HasMaxLength(120);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(120);
            entity.Property(e => e.Tag).HasMaxLength(550);
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("Warehouse");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InsertDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Tag).HasMaxLength(550);
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
