﻿// <auto-generated />
using System;
using LucruIndividual.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LucruIndividual.Migrations
{
    [DbContext(typeof(SocialPlatformContext))]
    partial class SocialPlatformContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.FriendRelation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.Property<int>("user1Id")
                        .HasColumnType("int");

                    b.Property<int>("user2Id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user1Id");

                    b.HasIndex("user2Id");

                    b.ToTable("friendRelations");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("createdTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("idImage")
                        .HasColumnType("int");

                    b.Property<int>("idProfile")
                        .HasColumnType("int");

                    b.Property<string>("postText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("idImage");

                    b.HasIndex("idProfile");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.PostImage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("postImages");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.Profile", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<string>("aboutUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("profileImageid")
                        .HasColumnType("int");

                    b.Property<int>("sex")
                        .HasColumnType("int");

                    b.Property<string>("surrname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.HasIndex("profileImageid");

                    b.ToTable("profiles");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.ProfileImage", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ProfileImage");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<bool>("status")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.FriendRelation", b =>
                {
                    b.HasOne("LucruIndividual.Models.DbEntities.User", "user1")
                        .WithMany()
                        .HasForeignKey("user1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LucruIndividual.Models.DbEntities.User", "user2")
                        .WithMany()
                        .HasForeignKey("user2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("user1");

                    b.Navigation("user2");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.Post", b =>
                {
                    b.HasOne("LucruIndividual.Models.DbEntities.PostImage", "image")
                        .WithMany()
                        .HasForeignKey("idImage");

                    b.HasOne("LucruIndividual.Models.DbEntities.Profile", "profile")
                        .WithMany()
                        .HasForeignKey("idProfile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("image");

                    b.Navigation("profile");
                });

            modelBuilder.Entity("LucruIndividual.Models.DbEntities.Profile", b =>
                {
                    b.HasOne("LucruIndividual.Models.DbEntities.ProfileImage", "profileImage")
                        .WithMany()
                        .HasForeignKey("profileImageid");

                    b.HasOne("LucruIndividual.Models.DbEntities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("profileImage");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
