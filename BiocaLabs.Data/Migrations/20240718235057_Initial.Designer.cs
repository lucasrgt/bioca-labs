﻿// <auto-generated />
using System;
using BiocaLabs.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BiocaLabs.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240718235057_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lab.Domain.Entities.Medicine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.Property<string>("CommercialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("Lab.Domain.Entities.Medicine", b =>
                {
                    b.OwnsOne("Lab.Domain.VOs.MedicineRegistration", "Registration", b1 =>
                        {
                            b1.Property<Guid>("MedicineId")
                                .HasColumnType("uuid");

                            b1.Property<string>("AnvisaNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PatentNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("MedicineId");

                            b1.ToTable("Medicines");

                            b1.WithOwner()
                                .HasForeignKey("MedicineId");
                        });

                    b.Navigation("Registration")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}