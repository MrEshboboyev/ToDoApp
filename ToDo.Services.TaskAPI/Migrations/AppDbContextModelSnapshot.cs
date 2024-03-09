﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ToDo.Services.TaskAPI.Data;

#nullable disable

namespace ToDo.Services.TaskAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.1.24081.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToDo.Services.TaskAPI.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Draft and finalize the project proposal for client review.",
                            DueDate = new DateTime(2024, 3, 16, 12, 10, 50, 642, DateTimeKind.Utc).AddTicks(7463),
                            IsCompleted = false,
                            Priority = 2,
                            Title = "Complete Project Proposal"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Participate in the weekly team meeting to discuss project updates.",
                            DueDate = new DateTime(2024, 3, 11, 12, 10, 50, 642, DateTimeKind.Utc).AddTicks(7510),
                            IsCompleted = false,
                            Priority = 1,
                            Title = "Attend Team Meeting"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Create a presentation for the upcoming client meeting.",
                            DueDate = new DateTime(2024, 3, 14, 12, 10, 50, 642, DateTimeKind.Utc).AddTicks(7514),
                            IsCompleted = false,
                            Priority = 2,
                            Title = "Prepare Presentation"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
