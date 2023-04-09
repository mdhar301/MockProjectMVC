using Microsoft.EntityFrameworkCore;
using MVCMock.Models;
using System.Collections.Generic;

namespace MVCMock.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Hobbies> Hobbies { get; set; }
        public DbSet<StudentHobby> StudentHobbies { get; set; }



    }
}
