using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cargo4You.Models;

public partial class Cargo4youContext : DbContext
{
    public Cargo4youContext()
    {
    }

    public Cargo4youContext(DbContextOptions<Cargo4youContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Courier> Couriers { get; set; }

    public virtual DbSet<Quotation> Quotations { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=cargo4you;Username=cargo4you;Password=Admin!1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Courier>(entity =>
        {
            entity.HasKey(e => e.CourierId).HasName("courier_pkey");

            entity.ToTable("courier");

            entity.Property(e => e.CourierId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("courier_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.QuotationId).HasName("quotations_pkey");

            entity.ToTable("quotations");

            entity.Property(e => e.QuotationId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("quotation_id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .HasColumnName("client_name");
            entity.Property(e => e.CourierId).HasColumnName("courier_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Width).HasColumnName("width");
			entity.Property(e => e.Height).HasColumnName("height");
			entity.Property(e => e.Depth).HasColumnName("depth");
			entity.Property(e => e.Weight).HasColumnName("weight");
			entity.Property(e => e.ShipmentConfirm).HasColumnName("shipment_confirm");
			entity.Property(e => e.Address).HasColumnName("address");
			entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

			entity.HasOne(d => d.Courier).WithMany(p => p.Quotations)
                .HasForeignKey(d => d.CourierId)
                .HasConstraintName("fk_customer");
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("prices_pkey");

            entity.ToTable("rules");

            entity.Property(e => e.PriceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("price_id");
            entity.Property(e => e.CourierId).HasColumnName("courier_id");
            entity.Property(e => e.ExtraKg).HasColumnName("extra_kg");
            entity.Property(e => e.ExtraValue).HasColumnName("extra_value");
            entity.Property(e => e.IsDimension).HasColumnName("is_dimension");
            entity.Property(e => e.Max)
                .HasMaxLength(20)
                .HasColumnName("max");
            entity.Property(e => e.Min).HasColumnName("min");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Courier).WithMany(p => p.Rules)
                .HasForeignKey(d => d.CourierId)
                .HasConstraintName("fk_customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
