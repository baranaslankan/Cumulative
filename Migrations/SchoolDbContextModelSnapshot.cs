﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolApi.Models;

#nullable disable

namespace SchoolApi.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    partial class SchoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("TeacherFname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TeacherLname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });
#pragma warning restore 612, 618
        }
    }
}
