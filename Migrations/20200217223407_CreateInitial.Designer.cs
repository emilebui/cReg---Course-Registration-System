﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cReg_WebApp.Models.context;

namespace cReg_WebApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200217223407_CreateInitial")]
    partial class CreateInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0-preview1.19506.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cReg_WebApp.Models.entities.Course", b =>
                {
                    b.Property<int>("courseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("courseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("courseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("creditHours")
                        .HasColumnType("int");

                    b.Property<string>("date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("space")
                        .HasColumnType("int");

                    b.HasKey("courseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Enrolled", b =>
                {
                    b.Property<int>("enrollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("completed")
                        .HasColumnType("bit");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<int>("grade")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("enrollId");

                    b.HasIndex("courseId");

                    b.HasIndex("studentId");

                    b.ToTable("Enrolled");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Faculty", b =>
                {
                    b.Property<int>("facultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("facultyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("facultyId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Prerequisite", b =>
                {
                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<int>("prerequisiteId")
                        .HasColumnType("int");

                    b.Property<int>("grade")
                        .HasColumnType("int");

                    b.HasKey("courseId", "prerequisiteId");

                    b.HasIndex("prerequisiteId");

                    b.ToTable("Prerequisites");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Required", b =>
                {
                    b.Property<int>("facultyId")
                        .HasColumnType("int");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.HasKey("facultyId", "courseId");

                    b.HasIndex("courseId");

                    b.ToTable("Required");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Student", b =>
                {
                    b.Property<int>("studentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("majorId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("studentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Enrolled", b =>
                {
                    b.HasOne("cReg_WebApp.Models.entities.Course", "course")
                        .WithMany()
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("cReg_WebApp.Models.entities.Student", "student")
                        .WithMany("CompletedCourses")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Prerequisite", b =>
                {
                    b.HasOne("cReg_WebApp.Models.entities.Course", "course")
                        .WithMany()
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("cReg_WebApp.Models.entities.Course", "prerequisite")
                        .WithMany()
                        .HasForeignKey("prerequisiteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Required", b =>
                {
                    b.HasOne("cReg_WebApp.Models.entities.Course", "course")
                        .WithMany()
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("cReg_WebApp.Models.entities.Faculty", "faculty")
                        .WithMany()
                        .HasForeignKey("facultyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
