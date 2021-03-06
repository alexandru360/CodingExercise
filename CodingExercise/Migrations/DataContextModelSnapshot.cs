﻿// <auto-generated />
using System;
using CodingExercise.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodingExercise.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("CodingExercise.Domain.ProcessPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardHolder")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(250);

                    b.Property<long>("CreditCardNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(19);

                    b.HasKey("Id");

                    b.ToTable("ProcessPayment");
                });

            modelBuilder.Entity("CodingExercise.Infrastructure.PaymentState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PmntState")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProcessPaymentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProcessPaymentId")
                        .IsUnique();

                    b.ToTable("PaymentState");
                });

            modelBuilder.Entity("CodingExercise.Infrastructure.PaymentState", b =>
                {
                    b.HasOne("CodingExercise.Domain.ProcessPayment", "ProcessPayment")
                        .WithOne("PaymentState")
                        .HasForeignKey("CodingExercise.Infrastructure.PaymentState", "ProcessPaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
