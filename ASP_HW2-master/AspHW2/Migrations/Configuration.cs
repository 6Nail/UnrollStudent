namespace AspHW2.Migrations
{
    using AspHW2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspHW2.Data.StudentDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AspHW2.Data.StudentDataContext";
        }

        protected override void Seed(AspHW2.Data.StudentDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var student = new Student
            {
                Courses = new List<Course>(),
                Id = 1,
                LastName = "Oralbayev",
                Name = "Galymzhan",
                Mark = 5
            };
            context.Students.Add(student);
            context.Subjects.Add(new Subject
            {
                 Id = 1,
                 CourseName = "Math",
                 Room = 5,
                 Students = new System.Collections.Generic.List<Student> { student },
                 Title = "qwe"
            });
            context.SaveChanges();
        }
    }
}
