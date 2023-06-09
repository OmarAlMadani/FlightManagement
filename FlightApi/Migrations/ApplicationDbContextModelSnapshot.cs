﻿// <auto-generated />
using FlightApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("FlightApi.Data.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IATA")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Airports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IATA = "LAX",
                            Latitude = 33.941600000000001,
                            Longitude = -118.4085,
                            Name = "Los Angeles International Airport"
                        },
                        new
                        {
                            Id = 2,
                            IATA = "JFK",
                            Latitude = 40.641300000000001,
                            Longitude = -73.778099999999995,
                            Name = "John F. Kennedy International Airport"
                        },
                        new
                        {
                            Id = 3,
                            IATA = "LHR",
                            Latitude = 51.469999999999999,
                            Longitude = -0.45429999999999998,
                            Name = "Heathrow Airport"
                        },
                        new
                        {
                            Id = 4,
                            IATA = "CDG",
                            Latitude = 49.009700000000002,
                            Longitude = 2.5478999999999998,
                            Name = "Charles de Gaulle Airport"
                        });
                });

            modelBuilder.Entity("FlightApi.Data.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArrivalAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartureAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Distance")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("DepartureAirportId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightApi.Data.Models.Flight", b =>
                {
                    b.HasOne("FlightApi.Data.Models.Airport", "ArrivalAirport")
                        .WithMany()
                        .HasForeignKey("ArrivalAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightApi.Data.Models.Airport", "DepartureAirport")
                        .WithMany()
                        .HasForeignKey("DepartureAirportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");
                });
#pragma warning restore 612, 618
        }
    }
}
