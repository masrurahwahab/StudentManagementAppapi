using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;
using StudentManagementAppapi.PasswordValidation;

namespace StudentManagementAppapi.Data
{
    public class StudentManagementDbContext : DbContext
    {
        
        public StudentManagementDbContext(DbContextOptions<StudentManagementDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<AcademicTerm> AcademicTerms { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<TermReport> TermReports { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Disciplinary> Disciplinaries { get; set; }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var passwordHashing = new PasswordHashing();
            var salt = passwordHashing.GenerateSalt();

            var defaultPassword = Environment.GetEnvironmentVariable("Kay@123#!") ?? "Admin123!";
            var passwordHash = passwordHashing.HashPassword(defaultPassword, salt);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Username = "Kay_ay",
                Email = "kkay@gmail.com",
                FirstName = "Masrurah",
                LastName = "Abdul Wahab",
                Role = UserRole.Admin,
                IsActive = true,
                PasswordHash = passwordHash,
                Salt = salt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);

            
            ConfigureUserRelationships(modelBuilder);
            ConfigureStudentRelationships(modelBuilder);
            ConfigureClassRelationships(modelBuilder);
            ConfigureAssessmentRelationships(modelBuilder);
            ConfigureAttendanceRelationships(modelBuilder);
            ConfigureDisciplinaryRelationships(modelBuilder);
            ConfigureAnnouncementRelationships(modelBuilder);
            ConfigureNotificationRelationships(modelBuilder);

            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.AdmissionNumber)
                .IsUnique();

            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<Subject>()
                .HasIndex(s => s.Code)
                .IsUnique();

            
            modelBuilder.Entity<Assessment>()
                .Property(a => a.MaxScore)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Result>()
                .Property(r => r.Score)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TermReport>()
                .Property(tr => tr.TotalScore)
                .HasPrecision(7, 2);

            modelBuilder.Entity<TermReport>()
                .Property(tr => tr.AverageScore)
                .HasPrecision(5, 2);
        }

       
        private void ConfigureUserRelationships(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.User)
                .WithOne(u => u.Teacher)
                .HasForeignKey<Teacher>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Parent>()
                .HasOne(p => p.User)
                .WithOne(u => u.Parent)
                .HasForeignKey<Parent>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureStudentRelationships(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Parent>()
                .HasOne(p => p.Student)
                .WithMany(s => s.Parents)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureClassRelationships(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Class>()
                .HasOne(c => c.ClassTeacher)
                .WithMany()
                .HasForeignKey(c => c.ClassTeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            
            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.ClassSubjects)
                .HasForeignKey(cs => cs.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Teacher)
                .WithMany(t => t.ClassSubjects)
                .HasForeignKey(cs => cs.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureAssessmentRelationships(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.ClassSubject)
                .WithMany(cs => cs.Assessments)
                .HasForeignKey(a => a.ClassSubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.Term)
                .WithMany(t => t.Assessments)
                .HasForeignKey(a => a.TermId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Results)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Assessment)
                .WithMany(a => a.Results)
                .HasForeignKey(r => r.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<TermReport>()
                .HasOne(tr => tr.Student)
                .WithMany()
                .HasForeignKey(tr => tr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TermReport>()
                .HasOne(tr => tr.Term)
                .WithMany(t => t.TermReports)
                .HasForeignKey(tr => tr.TermId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureAttendanceRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.MarkedBy)
                .WithMany(t => t.AttendanceRecords)
                .HasForeignKey(a => a.MarkedById)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureDisciplinaryRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disciplinary>()
                .HasOne(d => d.Student)
                .WithMany(s => s.DisciplinaryRecords)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Disciplinary>()
                .HasOne(d => d.ReportedBy)
                .WithMany()
                .HasForeignKey(d => d.ReportedById)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureAnnouncementRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Author)
                .WithMany()
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Class)
                .WithMany()
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        private void ConfigureNotificationRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
