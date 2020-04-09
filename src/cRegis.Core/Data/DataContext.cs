using cRegis.Core.Entities;
using cRegis.Core.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace cRegis.Core.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Prerequisite> Prerequisites { get; set; }

        public DbSet<Required> Required { get; set; }

        public DbSet<Enrolled> Enrolled { get; set; }

        public DbSet<StudentUser> StuentUsers { get; set; }

        public DbSet<Wishlist> Wishlist { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Required>()
                .HasKey(r => new { r.facultyId, r.courseId });

            modelBuilder.Entity<Prerequisite>()
                .HasKey(p => new { p.courseId, p.prerequisiteId });

            modelBuilder.Entity<Wishlist>()
                .HasKey(p => new { p.studentId, p.courseId });


            seed(modelBuilder);


        }

        protected virtual void seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
            new Course { courseId = 1, courseName = "COMP 1010", courseDescription = "An Introduction to Computer Science 1", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 2, courseName = "COMP 1020", courseDescription = "An Introduction to Computer Science 2", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 3, courseName = "COMP 2140", courseDescription = "Data Structure and Algorithm", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 4, courseName = "COMP 2160", courseDescription = "Program Practice", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 5, courseName = "COMP 2080", courseDescription = "Analysis of Algorithms", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 6, courseName = "COMP 2280", courseDescription = "Introduction to Computer Systems", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 7, courseName = "COMP 2150", courseDescription = "Object Orientation", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 8, courseName = "COMP 3490", courseDescription = "Graphics 1", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 9, courseName = "COMP 3350", courseDescription = "Software Engineer 1", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 10, courseName = "COMP 3170", courseDescription = "Analysis of Algorithms and Data Structures", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 11, courseName = "COMP 3430", courseDescription = "Operating Systems", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 12, courseName = "COMP 3370", courseDescription = "Computer Organization", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 13, courseName = "COMP 3190", courseDescription = "Artificial Intelligence 1", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 14, courseName = "COMP 3380", courseDescription = "Databases Concepts and Usage", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 15, courseName = "COMP 4490", courseDescription = "Computer Graphics", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 16, courseName = "COMP 4350", courseDescription = "Software Engineer 2", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 17, courseName = "COMP 4620", courseDescription = "Professional Practice in Computer Science", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 18, courseName = "COMP 4360", courseDescription = "Machine Learning", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 19, courseName = "COMP 4380", courseDescription = "Database Implementation", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 20, courseName = "MATH 1700", courseDescription = "Calculus 2", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 21, courseName = "STAT 1000", courseDescription = "Basic Statistical Analysis 1", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 22, courseName = "MATH 1500", courseDescription = "Introduction to Calculus", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 23, courseName = "STAT 2000", courseDescription = "Basic Statistical Analysis 2", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 24, courseName = "ECON 1010", courseDescription = "Introduction to Microeconomic Principles", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 25, courseName = "ECON 1020", courseDescription = "Introduction to Macroeconomic Principles", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 26, courseName = "HIST 1380", courseDescription = "An Introduction to Modern World History: 1800 - Present(M)", creditHours = 3, space = 5, date = new DateTime(2019, 9, 6) }
            );

            modelBuilder.Entity<Enrolled>().HasData(
                //John Brico's enrollments
                new Enrolled { enrollId = 1, studentId = 1, courseId = 1, completed = true, grade = 80, rating = 90, comment = "I like that course" },
                new Enrolled { enrollId = 2, studentId = 1, courseId = 2, completed = true, grade = 90, rating = 80, comment = "Very good" },
                new Enrolled { enrollId = 3, studentId = 1, courseId = 3, completed = true, grade = 95, rating = 75, comment = "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand" },
                new Enrolled { enrollId = 4, studentId = 1, courseId = 4, completed = true, grade = 95, rating = 76, comment = "I like the instructor" },
                new Enrolled { enrollId = 5, studentId = 1, courseId = 5, completed = true, grade = 85, rating = 82, comment = "Very good" },
                new Enrolled { enrollId = 6, studentId = 1, courseId = 6, completed = true, grade = 90, rating = 87, comment = "Franklin cannot take this one" },
                new Enrolled { enrollId = 7, studentId = 1, courseId = 7, completed = true, grade = 98, rating = 93, comment = "Excellent" },
                new Enrolled { enrollId = 8, studentId = 1, courseId = 8, completed = true, grade = 97, rating = 80, comment = "Good instructor" },
                new Enrolled { enrollId = 9, studentId = 1, courseId = 9, completed = true, grade = 96, rating = null, comment = null },
                new Enrolled { enrollId = 10, studentId = 1, courseId = 11, completed = true, grade = 92, rating = null, comment = null },
                new Enrolled { enrollId = 11, studentId = 1, courseId = 15, completed = true, grade = 87, rating = null, comment = null },
                new Enrolled { enrollId = 12, studentId = 1, courseId = 14, completed = true, grade = 88, rating = null, comment = null },

                //Mike Zapp's enrollments
                new Enrolled { enrollId = 13, studentId = 2, courseId = 1, completed = true, grade = 70, rating = 70, comment = "NEVER AGAIN" },
                new Enrolled { enrollId = 14, studentId = 2, courseId = 2, completed = true, grade = 73, rating = 80, comment = "You will either love the course or hate time" },
                new Enrolled { enrollId = 15, studentId = 2, courseId = 3, completed = true, grade = 85, rating = 90, comment = "Awesome" },
                new Enrolled { enrollId = 16, studentId = 2, courseId = 4, completed = true, grade = 75, rating = 60, comment = "Very good professor, the best one I've had withery good professor, the best one I've had with" },
                new Enrolled { enrollId = 17, studentId = 2, courseId = 7, completed = true, grade = 70, rating = 40, comment = "The course thinks that everyone learns the same way (auditory). " },
                new Enrolled { enrollId = 18, studentId = 2, courseId = 8, completed = true, grade = 50, rating = 20, comment = "Hard grader" },
                new Enrolled { enrollId = 19, studentId = 2, courseId = 9, completed = true, grade = 68, rating = 10, comment = "Nice Instructor" },
                new Enrolled { enrollId = 20, studentId = 2, courseId = 10, completed = true, grade = 65, rating = null, comment = null },
                new Enrolled { enrollId = 21, studentId = 2, courseId = 13, completed = true, grade = 65, rating = null, comment = null },
                new Enrolled { enrollId = 22, studentId = 2, courseId = 14, completed = true, grade = 50, rating = null, comment = null },
                new Enrolled { enrollId = 23, studentId = 2, courseId = 6, completed = false, grade = null, rating = null, comment = null },

                //Peter Graham's enrollements
                new Enrolled { enrollId = 24, studentId = 3, courseId = 1, completed = true, grade = 90, rating = 87, comment = "Very excellent professor. Very clear, informative, and very helpful. He knows his stuff very well! Can manage to balance his two positions in the Faculty of Science and Computer Science very well." },
                new Enrolled { enrollId = 25, studentId = 3, courseId = 2, completed = true, grade = 70, rating = 90, comment = "Markers marks unnecessarily harsh. Like his intention is to fail you not to grade you. Nevertheless, he does do his best, and he is better than many." },
                new Enrolled { enrollId = 26, studentId = 3, courseId = 3, completed = true, grade = 80, rating = null, comment = null },
                new Enrolled { enrollId = 27, studentId = 3, courseId = 4, completed = true, grade = 70, rating = null, comment = null },
                new Enrolled { enrollId = 28, studentId = 3, courseId = 6, completed = false, grade = null, rating = null, comment = null },

                //Robert Guderian enrollments
                new Enrolled { enrollId = 29, studentId = 4, courseId = 1, completed = true, grade = 90, rating = 87, comment = "Excellent course" },
                new Enrolled { enrollId = 30, studentId = 4, courseId = 2, completed = true, grade = 80, rating = 90, comment = "Lectures were super easy to understand." },
                new Enrolled { enrollId = 31, studentId = 4, courseId = 3, completed = true, grade = 90, rating = null, comment = null },
                new Enrolled { enrollId = 32, studentId = 4, courseId = 4, completed = true, grade = 75, rating = null, comment = null },
                new Enrolled { enrollId = 33, studentId = 4, courseId = 6, completed = false, grade = null, rating = null, comment = null },

                //Gord Boyer enrollments
                new Enrolled { enrollId = 34, studentId = 5, courseId = 1, completed = true, grade = 72, rating = 58, comment = "Boring lectures. Easy mid term, harder final-assignments took a lot of time. " },
                new Enrolled { enrollId = 35, studentId = 5, courseId = 2, completed = true, grade = 85, rating = 68, comment = "Don't bother going to the lectures." },
                new Enrolled { enrollId = 36, studentId = 5, courseId = 3, completed = true, grade = 90, rating = null, comment = null },
                new Enrolled { enrollId = 37, studentId = 5, courseId = 4, completed = true, grade = 75, rating = null, comment = null },
                new Enrolled { enrollId = 38, studentId = 5, courseId = 6, completed = false, grade = null, rating = null, comment = null },

                //Carson Leung enrollments
                new Enrolled { enrollId = 39, studentId = 6, courseId = 1, completed = true, grade = 98, rating = 85, comment = "The course is knowledgeable and well prepared." },
                new Enrolled { enrollId = 40, studentId = 6, courseId = 2, completed = true, grade = 97, rating = 98, comment = "I enjoyed this class, as I usually do." },
                new Enrolled { enrollId = 41, studentId = 6, courseId = 3, completed = true, grade = 98, rating = null, comment = null },
                new Enrolled { enrollId = 42, studentId = 6, courseId = 4, completed = true, grade = 88, rating = null, comment = null },
                new Enrolled { enrollId = 43, studentId = 6, courseId = 6, completed = false, grade = null, rating = null, comment = null },


                //Franklin Bristow enrollments
                new Enrolled { enrollId = 44, studentId = 7, courseId = 1, completed = true, grade = 75, rating = 68, comment = "Notes aren't good for studying." },
                new Enrolled { enrollId = 45, studentId = 7, courseId = 2, completed = true, grade = 68, rating = 67, comment = "Semi-entertaining lectures. Interesting fun guy to talk to but thinks a midterm average of apx. 60% is normal and fine." },
                new Enrolled { enrollId = 46, studentId = 7, courseId = 3, completed = true, grade = 74, rating = null, comment = null },
                new Enrolled { enrollId = 47, studentId = 7, courseId = 4, completed = true, grade = 72, rating = null, comment = null }
                );

            modelBuilder.Entity<Student>().HasData(
                new Student { studentId = 1, name = "John Braico", majorId = 1 },
                new Student { studentId = 2, name = "Mike Zapp", majorId = 1 },
                new Student { studentId = 3, name = "Peter Graham", majorId = 1 },
                new Student { studentId = 4, name = "Robert Guderian", majorId = 1 },
                new Student { studentId = 5, name = "Gord Boyer", majorId = 1 },
                new Student { studentId = 6, name = "Carson Leung", majorId = 1 },
                new Student { studentId = 7, name = "Franklin Bristow", majorId = 1 },
                new Student { studentId = 8, name = "Allan Marshall", majorId = 1 }
                );


            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { facultyId = 1, facultyName = "Computer Science", graduateCreditHours = 60 }
                );

            modelBuilder.Entity<Prerequisite>().HasData(
                new Prerequisite { courseId = 2, prerequisiteId = 1, grade = 60 },
                new Prerequisite { courseId = 3, prerequisiteId = 2, grade = 60 },
                new Prerequisite { courseId = 4, prerequisiteId = 2, grade = 60 },
                new Prerequisite { courseId = 5, prerequisiteId = 3, grade = 60 },
                new Prerequisite { courseId = 6, prerequisiteId = 3, grade = 60 },
                new Prerequisite { courseId = 6, prerequisiteId = 4, grade = 60 },
                new Prerequisite { courseId = 7, prerequisiteId = 3, grade = 60 },
                new Prerequisite { courseId = 8, prerequisiteId = 3, grade = 60 },
                new Prerequisite { courseId = 9, prerequisiteId = 7, grade = 60 },
                new Prerequisite { courseId = 10, prerequisiteId = 3, grade = 70 },
                new Prerequisite { courseId = 11, prerequisiteId = 6, grade = 60 },
                new Prerequisite { courseId = 11, prerequisiteId = 4, grade = 60 },
                new Prerequisite { courseId = 12, prerequisiteId = 6, grade = 60 },
                new Prerequisite { courseId = 13, prerequisiteId = 3, grade = 60 },
                new Prerequisite { courseId = 14, prerequisiteId = 3, grade = 60 },
                new Prerequisite { courseId = 15, prerequisiteId = 8, grade = 60 },
                new Prerequisite { courseId = 16, prerequisiteId = 9, grade = 60 },
                new Prerequisite { courseId = 17, prerequisiteId = 9, grade = 60 },
                new Prerequisite { courseId = 18, prerequisiteId = 10, grade = 60 },
                new Prerequisite { courseId = 18, prerequisiteId = 13, grade = 70 },
                new Prerequisite { courseId = 19, prerequisiteId = 11, grade = 60 },
                new Prerequisite { courseId = 19, prerequisiteId = 14, grade = 60 }
               );

            modelBuilder.Entity<Required>().HasData(
                new Required { facultyId = 1, courseId = 1 },
                new Required { facultyId = 1, courseId = 2 },
                new Required { facultyId = 1, courseId = 3 },
                new Required { facultyId = 1, courseId = 4 },
                new Required { facultyId = 1, courseId = 5 },
                new Required { facultyId = 1, courseId = 6 },
                new Required { facultyId = 1, courseId = 7 },
                new Required { facultyId = 1, courseId = 11 },
                new Required { facultyId = 1, courseId = 12 },
                new Required { facultyId = 1, courseId = 17 }
                );

            modelBuilder.Entity<Wishlist>().HasData(
                //John Brico's wishlist
                new Wishlist { studentId = 1, courseId = 13, priority = 1 },
                new Wishlist { studentId = 1, courseId = 16, priority = 2 },
                new Wishlist { studentId = 1, courseId = 17, priority = 2 }
                );

            IdentityRole studentRole = new IdentityRole { Name = "Student", NormalizedName = "STUDENT" };
            IdentityRole adminRole = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };

            modelBuilder.Entity<IdentityRole>().HasData(
                studentRole, adminRole
                );

            StudentUser user1 = makeStudentUser("jb", 1, "Password1!");
            StudentUser user2 = makeStudentUser("mz", 2, "Password1!");
            StudentUser user3 = makeStudentUser("pg", 3, "Password1!");
            StudentUser user4 = makeStudentUser("rg", 4, "Password1!");
            StudentUser user5 = makeStudentUser("gb", 5, "Password1!");
            StudentUser user6 = makeStudentUser("cl", 6, "Password1!");
            StudentUser user7 = makeStudentUser("fb", 7, "Password1!");

            modelBuilder.Entity<StudentUser>().HasData(
                user1, user2, user3, user4, user5, user6, user7
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user1.Id },
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user2.Id },
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user3.Id },
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user4.Id },
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user5.Id },
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user6.Id },
                new IdentityUserRole<string> { RoleId = studentRole.Id, UserId = user7.Id }
                );
        }

        protected virtual StudentUser makeStudentUser(string UserName, int StudentId, string Password)
        {
            try
            {
                //filling up the mandatoryy fields in AspNetusers table
                StudentUser user = new StudentUser
                {
                    UserName = UserName,
                    StudentId = StudentId,
                    NormalizedUserName = UserName.ToUpper(),
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false
                };

                user.PasswordHash = new PasswordHasher<StudentUser>().HashPassword(user, Password);
                return user;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
