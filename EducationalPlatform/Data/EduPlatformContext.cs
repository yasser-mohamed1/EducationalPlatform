using EducationalPlatform.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata;

namespace EducationalPlatform.Data
{
    public class EduPlatformContext : IdentityDbContext<ApplicationUser>
    {
        public EduPlatformContext()
        {
            
        }
        
        public EduPlatformContext(DbContextOptions options) 
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.Id)
                .HasPrincipalKey<ApplicationUser>(u => u.userId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Teacher)
                .WithOne(t => t.User)
                .HasForeignKey<Teacher>(t => t.Id)
                .HasPrincipalKey<ApplicationUser>(u => u.userId);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Enrollment)
                .WithOne(e => e.Subject)
                .HasForeignKey<Enrollment>(e => e.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Result>()
                .HasOne(r=>r.Student)
                .WithMany(s=>s.Result)
                .HasForeignKey(e=>e.StudentId);
            modelBuilder.Entity<AnswerResult>()
                .HasKey(a => a.AnswerId);
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithMany(qe => qe.quizzes)
                .UsingEntity(n => n.ToTable("Quiz_Question"));
			modelBuilder.Entity<StudentAnswer>()
				.HasKey(e => new { e.AnswerId, e._StudentAnswer });
			modelBuilder.Entity<QuestionCorrectAnswer>()
				.HasKey(e => new { e.QuestionId,e.CorrectAnswer });

			base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questiones { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<AnswerResult> AnswerResults { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Option> Options { get; set; }
		public DbSet<QuestionCorrectAnswer> QuestionCorrectAnswers { get; set; }
		public DbSet<Result> Results { get; set; }
		public DbSet<StudentAnswer> StudentAnswers { get; set; }
	}
}
