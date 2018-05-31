using System;
using CatsSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CatsSystem
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        { }

        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Cats> Cats { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=root;Database=cats_system");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carts>(entity =>
            {
                entity.ToTable("carts");

                entity.HasIndex(e => e.CatId)
                    .HasName("cat_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CatId)
                    .HasColumnName("cat_id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10)");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("carts_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("carts_ibfk_1");
            });

            modelBuilder.Entity<Cats>(entity =>
            {
                entity.ToTable("cats");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasColumnName("photo")
                    .HasMaxLength(255);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.RoleId)
                    .HasName("role_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(10)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("users_ibfk_1");
            });
        }
    }
}
