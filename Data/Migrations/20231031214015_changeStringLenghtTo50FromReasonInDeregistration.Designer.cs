﻿// <auto-generated />
using System;
using Bot_Balu_Ass_DB.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bot_Balu_Ass_DB.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231031214015_changeStringLenghtTo50FromReasonInDeregistration")]
    partial class changeStringLenghtTo50FromReasonInDeregistration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bot_Balu_Ass_DB.Data.Model.ChildModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Father")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mother")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("Bot_Balu_Ass_DB.Data.Model.Deregistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("ChildName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeregistrationAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeregistrationForOneDay")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeregistrationFrom")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeregistrationPerformedFromParents")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeregistrationTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("deregistrations");
                });
#pragma warning restore 612, 618
        }
    }
}
