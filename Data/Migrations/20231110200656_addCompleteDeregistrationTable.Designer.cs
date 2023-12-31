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
    [Migration("20231110200656_addCompleteDeregistrationTable")]
    partial class addCompleteDeregistrationTable
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

            modelBuilder.Entity("Bot_Balu_Ass_DB.Data.Model.CompleteDeregistration", b =>
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

                    b.Property<DateTime>("DateOfAction")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeregistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeregistrationPerformedFromParents")
                        .HasColumnType("bit");

                    b.Property<string>("Reason")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("completeDeregistrations");
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

                    b.Property<DateTime>("DateOfAction")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeregistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeregistrationPerformedFromParents")
                        .HasColumnType("bit");

                    b.Property<string>("Reason")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Deregistrations");
                });

            modelBuilder.Entity("Bot_Balu_Ass_DB.Data.Model.WithdrawnDeregistration", b =>
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

                    b.Property<DateTime>("DateOfDeregistrationAction")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfWithdrawn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeregistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DeregistrationPerformedFromParents")
                        .HasColumnType("bit");

                    b.Property<string>("DeregistrationReason")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("WithdrawnPerformedFromParents")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("WithdrawnDeregistrations");
                });
#pragma warning restore 612, 618
        }
    }
}
