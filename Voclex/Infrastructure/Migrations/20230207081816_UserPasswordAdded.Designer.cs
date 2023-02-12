﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230207081816_UserPasswordAdded")]
    partial class UserPasswordAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("Application.Models.Definition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TermId");

                    b.ToTable("Definitions");
                });

            modelBuilder.Entity("Application.Models.Example", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TermId");

                    b.ToTable("Examples");
                });

            modelBuilder.Entity("Application.Models.GuessedTimesCountToHoursWaiting", b =>
                {
                    b.Property<byte>("GuessedTimesCount")
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("HoursWaiting")
                        .HasColumnType("INTEGER");

                    b.HasKey("GuessedTimesCount");

                    b.ToTable("GuessedTimesCountToHoursWaiting");

                    b.HasData(
                        new
                        {
                            GuessedTimesCount = (byte)0,
                            HoursWaiting = (ushort)0
                        },
                        new
                        {
                            GuessedTimesCount = (byte)1,
                            HoursWaiting = (ushort)12
                        },
                        new
                        {
                            GuessedTimesCount = (byte)2,
                            HoursWaiting = (ushort)24
                        },
                        new
                        {
                            GuessedTimesCount = (byte)3,
                            HoursWaiting = (ushort)120
                        },
                        new
                        {
                            GuessedTimesCount = (byte)4,
                            HoursWaiting = (ushort)336
                        },
                        new
                        {
                            GuessedTimesCount = (byte)5,
                            HoursWaiting = (ushort)1080
                        },
                        new
                        {
                            GuessedTimesCount = (byte)6,
                            HoursWaiting = (ushort)2880
                        });
                });

            modelBuilder.Entity("Application.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("TermId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TermId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Application.Models.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TermsDictionaryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TermsDictionaryId");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("Application.Models.TermProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("GuessedTimesCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastGuessDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TermId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TermId");

                    b.HasIndex("UserId");

                    b.ToTable("TermProgresses");
                });

            modelBuilder.Entity("Application.Models.TermsDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TermsDictionaries");
                });

            modelBuilder.Entity("Application.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Application.Models.Definition", b =>
                {
                    b.HasOne("Application.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Term");
                });

            modelBuilder.Entity("Application.Models.Example", b =>
                {
                    b.HasOne("Application.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Term");
                });

            modelBuilder.Entity("Application.Models.Picture", b =>
                {
                    b.HasOne("Application.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Term");
                });

            modelBuilder.Entity("Application.Models.Term", b =>
                {
                    b.HasOne("Application.Models.TermsDictionary", "TermsDictionary")
                        .WithMany()
                        .HasForeignKey("TermsDictionaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TermsDictionary");
                });

            modelBuilder.Entity("Application.Models.TermProgress", b =>
                {
                    b.HasOne("Application.Models.Term", "Term")
                        .WithMany()
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Term");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
