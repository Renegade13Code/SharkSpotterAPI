﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharkSpotterAPI.Data;

#nullable disable

namespace SharkSpotterAPI.Migrations
{
    [DbContext(typeof(SharkSpotterDbContext))]
    [Migration("20220518104514_Initial migration")]
    partial class Initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SharkSpotterAPI.Models.Beach", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Geolocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beaches");
                });

            modelBuilder.Entity("SharkSpotterAPI.Models.Domain.Flag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("SharkSpotterAPI.Models.Domain.SharkStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeachId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FlagId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BeachId");

                    b.HasIndex("FlagId");

                    b.ToTable("sharkStatuses");
                });

            modelBuilder.Entity("SharkSpotterAPI.Models.Domain.SharkStatus", b =>
                {
                    b.HasOne("SharkSpotterAPI.Models.Beach", "Beach")
                        .WithMany("StatusHistory")
                        .HasForeignKey("BeachId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharkSpotterAPI.Models.Domain.Flag", "Flag")
                        .WithMany("sharkStatuses")
                        .HasForeignKey("FlagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beach");

                    b.Navigation("Flag");
                });

            modelBuilder.Entity("SharkSpotterAPI.Models.Beach", b =>
                {
                    b.Navigation("StatusHistory");
                });

            modelBuilder.Entity("SharkSpotterAPI.Models.Domain.Flag", b =>
                {
                    b.Navigation("sharkStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}