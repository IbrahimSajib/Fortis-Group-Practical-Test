using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Task_01.Models;

public partial class ItemDbContext : DbContext
{
    public ItemDbContext()
    {
    }

    public ItemDbContext(DbContextOptions<ItemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ItemDb;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B8BA4F559");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(30);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E838B6281C2D2");

            entity.ToTable("Item");

            entity.HasIndex(e => e.ItemName, "UQ__Item__4E4373F70798159D").IsUnique();

            entity.Property(e => e.ItemName).HasMaxLength(30);

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Item__CategoryId__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
