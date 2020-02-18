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
    [Migration("20200217083421_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0-preview1.19506.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cReg_WebApp.Models.entities.CompletedCourse", b =>
                {
                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("grade")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("studentId", "courseId");

                    b.HasIndex("courseId");

                    b.ToTable("CompletedCourses");
                });

            modelBuilder.Entity("cReg_WebApp.Models.entities.Course", b =>
                {
                    b.Property<int>("courseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("courseName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("courseId");

                    b.ToTable("Courses");
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

            modelBuilder.Entity("cReg_WebApp.Models.entities.CompletedCourse", b =>
                {
                    b.HasOne("cReg_WebApp.Models.entities.Course", "course")
                        .WithMany()
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cReg_WebApp.Models.entities.Student", "student")
                        .WithMany("CompletedCourses")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}