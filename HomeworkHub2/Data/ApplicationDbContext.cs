using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HomeworkHub2.Models;

namespace HomeworkHub2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HomeworkHub2.Models.Admin> Admin { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.Assignment> Assignment { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.Class> Class { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.Homework> Homework { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.Student> Student { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.StudentAssignment> StudentAssignment { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.Subject> Subject { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.Teacher> Teacher { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.TeacherClass> TeacherClass { get; set; } = default!;
        public DbSet<HomeworkHub2.Models.TeacherSubject> TeacherSubject { get; set; } = default!;
    }
}
