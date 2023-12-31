﻿// <auto-generated />
using System;
using CleanArchAttendanceApp.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchAttendanceApp.Infrastructure.Migrations
{
    [DbContext(typeof(AttendanceContext))]
    partial class AttendanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("CleanArchAttendanceApp.Core.Entities.Attendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AttendanceDay")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ClockedIn")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ClockedInAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ClockedOut")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ClockedOutAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AttenanceRecords");
                });

            modelBuilder.Entity("CleanArchAttendanceApp.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswrodHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b9e19b58-9b89-45da-a126-36e2974fb7ce"),
                            FullName = "User 1 Full Name",
                            PasswrodHash = "user 1 password hashed",
                            Role = "employee",
                            UserName = "UserName1"
                        },
                        new
                        {
                            Id = new Guid("97000964-dcd4-474f-8347-35d670af8588"),
                            FullName = "User 2 Full Name",
                            PasswrodHash = "user 2 password hashed",
                            Role = "employee",
                            UserName = "UserName2"
                        },
                        new
                        {
                            Id = new Guid("4ed0544a-5aa3-4dc8-ba7c-640e4bc99b64"),
                            FullName = "User 3 Full Name",
                            PasswrodHash = "user 3 password hashed",
                            Role = "employee",
                            UserName = "UserName3"
                        });
                });

            modelBuilder.Entity("CleanArchAttendanceApp.Core.Entities.Attendance", b =>
                {
                    b.HasOne("CleanArchAttendanceApp.Core.Entities.User", "User")
                        .WithMany("AttendanceRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CleanArchAttendanceApp.Core.Entities.User", b =>
                {
                    b.Navigation("AttendanceRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
