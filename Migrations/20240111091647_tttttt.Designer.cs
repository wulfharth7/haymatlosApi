﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using haymatlosApi.haymatlosApi.Models;

#nullable disable

namespace haymatlosApi.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20240111091647_tttttt")]
    partial class tttttt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.Comment", b =>
                {
                    b.Property<Guid>("PkeyUuidComment")
                        .HasColumnType("uuid")
                        .HasColumnName("pkey_uuid_comment");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<short?>("Dislike")
                        .HasColumnType("smallint")
                        .HasColumnName("dislike");

                    b.Property<Guid?>("FkeyUuidPost")
                        .HasColumnType("uuid")
                        .HasColumnName("fkey_uuid_post");

                    b.Property<Guid?>("FkeyUuidUser")
                        .HasColumnType("uuid")
                        .HasColumnName("fkey_uuid_user");

                    b.Property<short?>("Like")
                        .HasColumnType("smallint")
                        .HasColumnName("like");

                    b.Property<Guid?>("ParentComment")
                        .HasColumnType("uuid")
                        .HasColumnName("parentComment");

                    b.Property<DateTime?>("RegDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("regDate");

                    b.Property<string>("commenterUsername")
                        .HasColumnType("text");

                    b.HasKey("PkeyUuidComment")
                        .HasName("comments_pkey");

                    b.HasIndex("FkeyUuidUser");

                    b.HasIndex(new[] { "FkeyUuidPost" }, "IX_comments_fkey_uuid_post");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.Post", b =>
                {
                    b.Property<Guid>("PkeyUuidPost")
                        .HasColumnType("uuid")
                        .HasColumnName("pkey_uuid_post");

                    b.Property<short?>("CommentCount")
                        .HasColumnType("smallint");

                    b.Property<short?>("Dislike")
                        .HasColumnType("smallint")
                        .HasColumnName("dislike");

                    b.Property<Guid?>("FkeyUuidUser")
                        .HasColumnType("uuid")
                        .HasColumnName("fkey_uuid_user");

                    b.Property<short?>("Like")
                        .HasColumnType("smallint")
                        .HasColumnName("like");

                    b.Property<DateTime?>("RegDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("regDate");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("category")
                        .HasColumnType("text");

                    b.Property<string>("content")
                        .HasColumnType("text");

                    b.Property<string>("imageUrl")
                        .HasColumnType("text");

                    b.Property<string>("posterUsername")
                        .HasColumnType("text");

                    b.HasKey("PkeyUuidPost")
                        .HasName("posts_pkey");

                    b.HasIndex(new[] { "FkeyUuidUser" }, "IX_posts_fkey_uuid_user");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.User", b =>
                {
                    b.Property<Guid>("Uuid")
                        .HasColumnType("uuid")
                        .HasColumnName("uuid");

                    b.Property<string>("Nickname")
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime?>("RegDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("regDate");

                    b.Property<string>("Role")
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Salt")
                        .HasColumnType("text")
                        .HasColumnName("salt");

                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("Uuid")
                        .HasName("users_pkey");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.Comment", b =>
                {
                    b.HasOne("haymatlosApi.haymatlosApi.Models.Post", "FkeyUuidPostNavigation")
                        .WithMany("Comments")
                        .HasForeignKey("FkeyUuidPost")
                        .HasConstraintName("fkey_uuid_comment");

                    b.HasOne("haymatlosApi.haymatlosApi.Models.User", "FkeyUuidUserNavigation")
                        .WithMany("Comments")
                        .HasForeignKey("FkeyUuidUser")
                        .HasConstraintName("fkey_uuid_user_comment");

                    b.Navigation("FkeyUuidPostNavigation");

                    b.Navigation("FkeyUuidUserNavigation");
                });

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.Post", b =>
                {
                    b.HasOne("haymatlosApi.haymatlosApi.Models.User", "FkeyUuidUserNavigation")
                        .WithMany("Posts")
                        .HasForeignKey("FkeyUuidUser")
                        .HasConstraintName("fkey_uuid");

                    b.Navigation("FkeyUuidUserNavigation");
                });

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("haymatlosApi.haymatlosApi.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
