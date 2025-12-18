using API_E_Commerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_E_Commerce.Contexts;

public partial class ECommerceContext : DbContext
{
    public ECommerceContext(DbContextOptions<ECommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<Profil> Profils { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("article_pkey");

            entity.ToTable("item");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('article_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(1000, 2)
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("article_idCategory_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Total)
                .HasPrecision(1000, 2)
                .HasColumnName("total");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => new { e.IdOrder, e.IdItem }).HasName("orderLines_pkey");

            entity.ToTable("orderLines");

            entity.Property(e => e.IdOrder).HasColumnName("idOrder");
            entity.Property(e => e.IdItem).HasColumnName("idItem");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(1000, 2)
                .HasColumnName("totalPrice");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(1000, 2)
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.OrderLines)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderLines_idItem_fkey");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderLines)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderLines_idOrder_fkey");
        });

        modelBuilder.Entity<Profil>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("profil_pkey");

            entity.ToTable("profil");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Mail).HasColumnName("mail");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
