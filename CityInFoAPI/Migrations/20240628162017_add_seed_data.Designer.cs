﻿// <auto-generated />
using CityInFoAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInFoAPI.Migrations
{
    [DbContext(typeof(CityInfoDbContext))]
    [Migration("20240628162017_add_seed_data")]
    partial class add_seed_data
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("CityInFoAPI.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is Tehran",
                            Name = "Tehran"
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is Shiraz",
                            Name = "Shiraz"
                        },
                        new
                        {
                            Id = 3,
                            Description = "This is Tabriz",
                            Name = "Tabriz"
                        });
                });

            modelBuilder.Entity("CityInFoAPI.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "AZADI       Tower",
                            Name = "Azadi Tower"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "This is Shemiran",
                            Name = "Shemiran"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 1,
                            Description = "This is ToopKhoone",
                            Name = "Meydan ToopKhoone"
                        });
                });

            modelBuilder.Entity("CityInFoAPI.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInFoAPI.Entities.City", "City")
                        .WithMany("PointOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInFoAPI.Entities.City", b =>
                {
                    b.Navigation("PointOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
