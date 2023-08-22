using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace haymatlosApi.haymatlosApi.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=postgres;User Id=user;Password=admin;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.PkeyUuidComment).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.HasIndex(e => e.FkeyUuidPost, "IX_comments_fkey_uuid_post");

            entity.Property(e => e.PkeyUuidComment)
                .ValueGeneratedNever()
                .HasColumnName("pkey_uuid_comment");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FkeyUuidPost).HasColumnName("fkey_uuid_post");
            entity.Property(e => e.IsIndexed).HasColumnName("isIndexed");
            entity.Property(e => e.ParentComment).HasColumnName("parentComment");

            entity.HasOne(d => d.FkeyUuidPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FkeyUuidPost)
                .HasConstraintName("fkey_uuid_comment");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PkeyUuidPost).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.HasIndex(e => e.FkeyUuidUser, "IX_posts_fkey_uuid_user");

            entity.Property(e => e.PkeyUuidPost)
                .ValueGeneratedNever()
                .HasColumnName("pkey_uuid_post");
            entity.Property(e => e.FkeyUuidUser).HasColumnName("fkey_uuid_user");
            entity.Property(e => e.IsIndexed).HasColumnName("isIndexed");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.FkeyUuidUserNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.FkeyUuidUser)
                .HasConstraintName("fkey_uuid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.Uuid, e.IsIndexed }, "unq").IsUnique();

            entity.Property(e => e.Uuid)
                .ValueGeneratedNever()
                .HasColumnName("uuid");
            entity.Property(e => e.IsIndexed).HasColumnName("isIndexed");
            entity.Property(e => e.Nickname).HasColumnName("nickname");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.Token).HasColumnName("token");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
