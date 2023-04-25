﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingSpace.Data;

#nullable disable

namespace ParkingSpace.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20230421104208_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.16");

            modelBuilder.Entity("ParkingSpace.Features.Customer.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("LastVisited")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ParkingSpace.Features.Incident.Entities.Incident", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("OccurredAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SpaceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("ParkingSpace.Features.Price.Entities.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("ChargeModel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("MaximumTime")
                        .HasColumnType("REAL");

                    b.Property<Guid?>("SpaceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SumPrice")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("SpaceId", "VehicleType", "ChargeModel", "MaximumTime");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("ParkingSpace.Features.Space.Entities.Space", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("ParkingSpace.Features.Space.Entities.Spot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("AvailableSpot")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaximumSpot")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("SpaceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("SpaceId", "VehicleType");

                    b.ToTable("Spots");
                });

            modelBuilder.Entity("ParkingSpace.Features.Staff.Entities.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("ParkingSpace.Features.Ticket.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Paid")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("SpotId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpotPosition")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("StartedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("TicketNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SpotId");

                    b.HasIndex("TicketNumber")
                        .IsUnique();

                    b.HasIndex("VehicleId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ParkingSpace.Features.Vehicle.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ArchivedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchivedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistrationNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RegistrationNo")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("ParkingSpace.Features.Incident.Entities.Incident", b =>
                {
                    b.HasOne("ParkingSpace.Features.Space.Entities.Space", "Space")
                        .WithMany()
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ParkingSpace.Features.Vehicle.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Space");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("ParkingSpace.Features.Price.Entities.Price", b =>
                {
                    b.HasOne("ParkingSpace.Features.Space.Entities.Space", "Space")
                        .WithMany("Prices")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Space");
                });

            modelBuilder.Entity("ParkingSpace.Features.Space.Entities.Spot", b =>
                {
                    b.HasOne("ParkingSpace.Features.Space.Entities.Space", "Space")
                        .WithMany("Spots")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Space");
                });

            modelBuilder.Entity("ParkingSpace.Features.Ticket.Entities.Ticket", b =>
                {
                    b.HasOne("ParkingSpace.Features.Space.Entities.Spot", "Spot")
                        .WithMany()
                        .HasForeignKey("SpotId");

                    b.HasOne("ParkingSpace.Features.Vehicle.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Spot");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("ParkingSpace.Features.Vehicle.Entities.Vehicle", b =>
                {
                    b.HasOne("ParkingSpace.Features.Customer.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ParkingSpace.Features.Space.Entities.Space", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("Spots");
                });
#pragma warning restore 612, 618
        }
    }
}
