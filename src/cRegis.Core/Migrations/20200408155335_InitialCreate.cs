using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cRegis.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(nullable: true),
                    courseDescription = table.Column<string>(nullable: true),
                    creditHours = table.Column<int>(nullable: false),
                    space = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseId);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    facultyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    facultyName = table.Column<string>(nullable: true),
                    graduateCreditHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.facultyId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prerequisites",
                columns: table => new
                {
                    courseId = table.Column<int>(nullable: false),
                    prerequisiteId = table.Column<int>(nullable: false),
                    grade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisites", x => new { x.courseId, x.prerequisiteId });
                    table.ForeignKey(
                        name: "FK_Prerequisites_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisites_Courses_prerequisiteId",
                        column: x => x.prerequisiteId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Required",
                columns: table => new
                {
                    facultyId = table.Column<int>(nullable: false),
                    courseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Required", x => new { x.facultyId, x.courseId });
                    table.ForeignKey(
                        name: "FK_Required_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Required_Faculties_facultyId",
                        column: x => x.facultyId,
                        principalTable: "Faculties",
                        principalColumn: "facultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    studentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    majorId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.studentId);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_majorId",
                        column: x => x.majorId,
                        principalTable: "Faculties",
                        principalColumn: "facultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrolled",
                columns: table => new
                {
                    enrollId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseId = table.Column<int>(nullable: false),
                    studentId = table.Column<int>(nullable: false),
                    completed = table.Column<bool>(nullable: false),
                    grade = table.Column<int>(nullable: true),
                    rating = table.Column<int>(nullable: true),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolled", x => x.enrollId);
                    table.ForeignKey(
                        name: "FK_Enrolled_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrolled_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    studentId = table.Column<int>(nullable: false),
                    courseId = table.Column<int>(nullable: false),
                    priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => new { x.studentId, x.courseId });
                    table.ForeignKey(
                        name: "FK_Wishlist_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wishlist_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "489d4fc0-3892-441b-b555-082ab933ca59", "82aab839-36bb-4638-9f64-28b031aaac2f", "Student", "STUDENT" },
                    { "d7842d6b-1d12-440d-9dff-1a7c24897cb2", "d963210f-6440-46c1-a3de-25f710c7d02f", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "courseId", "courseDescription", "courseName", "creditHours", "date", "space" },
                values: new object[,]
                {
                    { 25, "Introduction to Macroeconomic Principles", "ECON 1020", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 24, "Introduction to Microeconomic Principles", "ECON 1010", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 23, "Basic Statistical Analysis 2", "STAT 2000", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 22, "Introduction to Calculus", "MATH 1500", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 21, "Basic Statistical Analysis 1", "STAT 1000", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 20, "Calculus 2", "MATH 1700", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 19, "Database Implementation", "COMP 4380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 18, "Machine Learning", "COMP 4360", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 17, "Professional Practice in Computer Science", "COMP 4620", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 16, "Software Engineer 2", "COMP 4350", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 15, "Computer Graphics", "COMP 4490", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 14, "Databases Concepts and Usage", "COMP 3380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 13, "Artificial Intelligence 1", "COMP 3190", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 12, "Computer Organization", "COMP 3370", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 11, "Operating Systems", "COMP 3430", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 10, "Analysis of Algorithms and Data Structures", "COMP 3170", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 9, "Software Engineer 1", "COMP 3350", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 8, "Graphics 1", "COMP 3490", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 7, "Object Orientation", "COMP 2150", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, "Introduction to Computer Systems", "COMP 2280", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "Analysis of Algorithms", "COMP 2080", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 4, "Program Practice", "COMP 2160", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 3, "Data Structure and Algorithm", "COMP 2140", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, "An Introduction to Computer Science 2", "COMP 1020", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 1, "An Introduction to Computer Science 1", "COMP 1010", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 26, "An Introduction to Modern World History: 1800 - Present(M)", "HIST 1380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "facultyId", "facultyName", "graduateCreditHours" },
                values: new object[] { 1, "Computer Science", 60 });

            migrationBuilder.InsertData(
                table: "Prerequisites",
                columns: new[] { "courseId", "prerequisiteId", "grade" },
                values: new object[,]
                {
                    { 2, 1, 60 },
                    { 19, 14, 60 },
                    { 19, 11, 60 },
                    { 18, 10, 60 },
                    { 17, 9, 60 },
                    { 16, 9, 60 },
                    { 15, 8, 60 },
                    { 14, 3, 60 },
                    { 13, 3, 60 },
                    { 12, 6, 60 },
                    { 11, 4, 60 },
                    { 18, 13, 70 },
                    { 10, 3, 70 },
                    { 9, 7, 60 },
                    { 8, 3, 60 },
                    { 7, 3, 60 },
                    { 6, 4, 60 },
                    { 6, 3, 60 },
                    { 5, 3, 60 },
                    { 4, 2, 60 },
                    { 3, 2, 60 },
                    { 11, 6, 60 }
                });

            migrationBuilder.InsertData(
                table: "Required",
                columns: new[] { "facultyId", "courseId" },
                values: new object[,]
                {
                    { 1, 17 },
                    { 1, 12 },
                    { 1, 11 },
                    { 1, 7 },
                    { 1, 6 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 1, 1 },
                    { 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "majorId", "name" },
                values: new object[,]
                {
                    { 7, 1, "Franklin Bristow" },
                    { 1, 1, "John Braico" },
                    { 2, 1, "Mike Zapp" },
                    { 3, 1, "Peter Graham" },
                    { 4, 1, "Robert Guderian" },
                    { 5, 1, "Gord Boyer" },
                    { 6, 1, "Carson Leung" },
                    { 8, 1, "Allan Marshall" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "StudentId" },
                values: new object[,]
                {
                    { "2065db0a-d127-4f54-8566-7a6894228be1", 0, "a2ac8256-b36a-4bc8-9919-ab364a555e72", "StudentUser", null, false, false, null, null, "FB", "AQAAAAEAACcQAAAAEGjpNDb2NJ1vevomHkShUf63bakWFtQ+udytCrXXOKMwnelyxuu1CCmnhdLca9Go+w==", null, false, "2643a991-e1a0-4c4e-9689-70f886f5f4b1", false, "fb", 7 },
                    { "cb558c44-71d7-44f5-bc6b-8a031528d7d6", 0, "234c717d-2f51-4c7b-9df1-dae57b82d1a8", "StudentUser", null, false, false, null, null, "CL", "AQAAAAEAACcQAAAAEFQfJKOfechObw36jPsJ/e+bWDe+B+7n4ZqprVoHnIt6iscdioTt75DDeZ84zkGa2A==", null, false, "1073cbc5-b0f4-4bbf-a2fc-6d99367f3860", false, "cl", 6 },
                    { "a88fa690-dd42-4336-a522-1ce9c83806b2", 0, "bc5033f3-ec4c-4f20-bfa9-1d15178fbac5", "StudentUser", null, false, false, null, null, "PG", "AQAAAAEAACcQAAAAEMPl4sMZH1LAb2sJZUZnT0fKWgpwNKU6piVZrN7dcSmv/c71ttwQWdS8fPGhVQD82Q==", null, false, "61a8363d-7334-45d7-9c35-2038de2e736e", false, "pg", 3 },
                    { "fab73280-5f0d-433e-b133-5c81c8adb9a6", 0, "87095d66-8ae7-4857-aaaa-33a3aa66e540", "StudentUser", null, false, false, null, null, "GB", "AQAAAAEAACcQAAAAEDBhXvr6SelSj5idsK/7t8glSixGvP6OEBXy+zJnbf0aXBqZ3DEF+BAuwkcQI5HDtg==", null, false, "25973fb3-5b05-4d58-9c13-a6e112e0f7db", false, "gb", 5 },
                    { "a9438dcb-76f0-4b39-ad5d-acd0aff6ca5b", 0, "82838b53-0fcd-4421-931e-27eba89e5ccc", "StudentUser", null, false, false, null, null, "RG", "AQAAAAEAACcQAAAAEKaawUKo9XZE3Mz23WWCd0hJvMQ+YDdkPLroGh3u7X5+ummzLVOAyHwxC6pGsQElaQ==", null, false, "75aad3b7-ac34-4380-973c-9b11df672ae1", false, "rg", 4 },
                    { "6cc5ed4e-2e83-4bc0-a7d7-1eafa2f11b65", 0, "0c76d47e-a5c4-48ea-ba0a-3e582af1781b", "StudentUser", null, false, false, null, null, "MZ", "AQAAAAEAACcQAAAAEADV72/39bk+tK8FPtPZC8c/Nn7ypg5ShYEm5pA22kM4fGF8quz4NKvJ27wnQCyV2w==", null, false, "773586aa-7015-4e00-823e-356eedbac338", false, "mz", 2 },
                    { "faf7288f-42d3-4002-9790-c6eae272a016", 0, "7994d5e2-fe56-49d9-bddd-df143913e905", "StudentUser", null, false, false, null, null, "JB", "AQAAAAEAACcQAAAAEDPWqJQsuo9unSHG6/BgRd6RVzo+JlXzrK4cXhx6Exm4E8kBYoGxGUYtmpvLF05SYA==", null, false, "0b5a0f0b-ccdb-4804-ab0d-a2ac043fff6f", false, "jb", 1 }
                });

            migrationBuilder.InsertData(
                table: "Enrolled",
                columns: new[] { "enrollId", "comment", "completed", "courseId", "grade", "rating", "studentId" },
                values: new object[,]
                {
                    { 26, null, true, 3, 80, null, 3 },
                    { 27, null, true, 4, 70, null, 3 },
                    { 28, null, false, 6, null, null, 3 },
                    { 29, "Excellent course", true, 1, 90, 87, 4 },
                    { 30, "Lectures were super easy to understand.", true, 2, 80, 90, 4 },
                    { 31, null, true, 3, 90, null, 4 },
                    { 32, null, true, 4, 75, null, 4 },
                    { 33, null, false, 6, null, null, 4 },
                    { 34, "Boring lectures. Easy mid term, harder final-assignments took a lot of time. ", true, 1, 72, 58, 5 },
                    { 35, "Don't bother going to the lectures.", true, 2, 85, 68, 5 },
                    { 37, null, true, 4, 75, null, 5 },
                    { 38, null, false, 6, null, null, 5 },
                    { 39, "The course is knowledgeable and well prepared.", true, 1, 98, 85, 6 },
                    { 40, "I enjoyed this class, as I usually do.", true, 2, 97, 98, 6 },
                    { 41, null, true, 3, 98, null, 6 },
                    { 42, null, true, 4, 88, null, 6 },
                    { 43, null, false, 6, null, null, 6 },
                    { 44, "Notes aren't good for studying.", true, 1, 75, 68, 7 },
                    { 45, "Semi-entertaining lectures. Interesting fun guy to talk to but thinks a midterm average of apx. 60% is normal and fine.", true, 2, 68, 67, 7 },
                    { 46, null, true, 3, 74, null, 7 },
                    { 36, null, true, 3, 90, null, 5 },
                    { 25, "Markers marks unnecessarily harsh. Like his intention is to fail you not to grade you. Nevertheless, he does do his best, and he is better than many.", true, 2, 70, 90, 3 },
                    { 24, "Very excellent professor. Very clear, informative, and very helpful. He knows his stuff very well! Can manage to balance his two positions in the Faculty of Science and Computer Science very well.", true, 1, 90, 87, 3 },
                    { 23, null, false, 6, null, null, 2 },
                    { 2, "Very good", true, 2, 90, 80, 1 },
                    { 3, "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand", true, 3, 95, 75, 1 },
                    { 4, "I like the instructor", true, 4, 95, 76, 1 },
                    { 5, "Very good", true, 5, 85, 82, 1 },
                    { 6, "Franklin cannot take this one", true, 6, 90, 87, 1 },
                    { 7, "Excellent", true, 7, 98, 93, 1 },
                    { 8, "Good instructor", true, 8, 97, 80, 1 },
                    { 9, null, true, 9, 96, null, 1 },
                    { 10, null, true, 11, 92, null, 1 },
                    { 11, null, true, 15, 87, null, 1 },
                    { 12, null, true, 14, 88, null, 1 },
                    { 47, null, true, 4, 72, null, 7 },
                    { 1, "I like that course", true, 1, 80, 90, 1 },
                    { 13, "NEVER AGAIN", true, 1, 70, 70, 2 },
                    { 14, "You will either love the course or hate time", true, 2, 73, 80, 2 },
                    { 15, "Awesome", true, 3, 85, 90, 2 },
                    { 16, "Very good professor, the best one I've had withery good professor, the best one I've had with", true, 4, 75, 60, 2 },
                    { 17, "The course thinks that everyone learns the same way (auditory). ", true, 7, 70, 40, 2 },
                    { 18, "Hard grader", true, 8, 50, 20, 2 },
                    { 19, "Nice Instructor", true, 9, 68, 10, 2 },
                    { 20, null, true, 10, 65, null, 2 },
                    { 21, null, true, 13, 65, null, 2 },
                    { 22, null, true, 14, 50, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Wishlist",
                columns: new[] { "studentId", "courseId", "priority" },
                values: new object[,]
                {
                    { 1, 17, 2 },
                    { 1, 16, 2 },
                    { 1, 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "faf7288f-42d3-4002-9790-c6eae272a016", "489d4fc0-3892-441b-b555-082ab933ca59" },
                    { "6cc5ed4e-2e83-4bc0-a7d7-1eafa2f11b65", "489d4fc0-3892-441b-b555-082ab933ca59" },
                    { "a88fa690-dd42-4336-a522-1ce9c83806b2", "489d4fc0-3892-441b-b555-082ab933ca59" },
                    { "a9438dcb-76f0-4b39-ad5d-acd0aff6ca5b", "489d4fc0-3892-441b-b555-082ab933ca59" },
                    { "fab73280-5f0d-433e-b133-5c81c8adb9a6", "489d4fc0-3892-441b-b555-082ab933ca59" },
                    { "cb558c44-71d7-44f5-bc6b-8a031528d7d6", "489d4fc0-3892-441b-b555-082ab933ca59" },
                    { "2065db0a-d127-4f54-8566-7a6894228be1", "489d4fc0-3892-441b-b555-082ab933ca59" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_courseId",
                table: "Enrolled",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_studentId",
                table: "Enrolled",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisites_prerequisiteId",
                table: "Prerequisites",
                column: "prerequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Required_courseId",
                table: "Required",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_majorId",
                table: "Students",
                column: "majorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_courseId",
                table: "Wishlist",
                column: "courseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Enrolled");

            migrationBuilder.DropTable(
                name: "Prerequisites");

            migrationBuilder.DropTable(
                name: "Required");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
