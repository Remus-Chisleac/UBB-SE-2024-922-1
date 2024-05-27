﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventsAppServer.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20240523135005_AlterUserInfo")]
    partial class AlterUserInfo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventsAppServer.Entities.AdminInfo", b =>
                {
                    b.Property<Guid>("GUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GUID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("EventsAppServer.Entities.Award", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AwardTypeObj")
                        .HasColumnType("int");

                    b.Property<Guid?>("TextPostId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TextPostId");

                    b.ToTable("Awards");
                });

            modelBuilder.Entity("EventsAppServer.Entities.DonationInfo", b =>
                {
                    b.Property<Guid>("GUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<Guid>("EventGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GUID");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("EventsAppServer.Entities.EventInfo", b =>
                {
                    b.Property<Guid>("GUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AgeLimit")
                        .HasColumnType("int");

                    b.Property<string>("BannerURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("EntryFee")
                        .HasColumnType("real");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<Guid>("OrganizerGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GUID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventsAppServer.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorGUID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("EventsAppServer.Entities.GroupUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MarketplaceScore")
                        .HasColumnType("int");

                    b.Property<int>("PostScore")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("EventsAppServer.Entities.JoinRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("JoinRequests");
                });

            modelBuilder.Entity("EventsAppServer.Entities.JoinRequestAnswerToOneQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("QuestionAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("JoinRequestAnswerToOneQuestion");
                });

            modelBuilder.Entity("EventsAppServer.Entities.PostReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PostReports");
                });

            modelBuilder.Entity("EventsAppServer.Entities.ReportInfo", b =>
                {
                    b.Property<Guid>("EventGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReportTypeValue")
                        .HasColumnType("int");

                    b.HasKey("EventGUID", "UserGUID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("EventsAppServer.Entities.ReviewInfo", b =>
                {
                    b.Property<Guid>("EventGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReviewDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Score")
                        .HasColumnType("real");

                    b.HasKey("EventGUID", "UserGUID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EventsAppServer.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EventsAppServer.Entities.TextPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("TextPosts");
                });

            modelBuilder.Entity("EventsAppServer.Entities.UserEventRelationInfo", b =>
                {
                    b.Property<Guid>("EventGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventGUID", "UserGUID");

                    b.ToTable("UserEventRelations");
                });

            modelBuilder.Entity("EventsAppServer.Entities.UserInfo", b =>
                {
                    b.Property<Guid>("GUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GUID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EventsAppServer.Entities.Award", b =>
                {
                    b.HasOne("EventsAppServer.Entities.TextPost", null)
                        .WithMany("Awards")
                        .HasForeignKey("TextPostId");
                });

            modelBuilder.Entity("EventsAppServer.Entities.Group", b =>
                {
                    b.HasOne("EventsAppServer.Entities.UserInfo", "Creator")
                        .WithMany("Groups")
                        .HasForeignKey("CreatorGUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("EventsAppServer.Entities.TextPost", b =>
                {
                    b.HasOne("EventsAppServer.Entities.GroupUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("EventsAppServer.Entities.TextPost", b =>
                {
                    b.Navigation("Awards");
                });

            modelBuilder.Entity("EventsAppServer.Entities.UserInfo", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}