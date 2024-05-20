﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventsAppServer.Migrations
{
    [DbContext(typeof(AppContrext))]
    partial class AppContrextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
#pragma warning restore 612, 618
        }
    }
}
