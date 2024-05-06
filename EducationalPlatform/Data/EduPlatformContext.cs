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

		    modelBuilder.Entity<Enrollment>()
	       .HasOne(e => e.Subject)
	       .WithMany()
	       .HasForeignKey(e => e.SubjectId)
	       .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Result>()
                .HasOne(r=>r.Student)
                .WithMany(s=>s.Result)
                .HasForeignKey(e=>e.StudentId);
            modelBuilder.Entity<AnswerResult>()
                .HasKey(a => a.AnswerId);
           
			modelBuilder.Entity<StudentAnswer>()
				.HasKey(e => new { e.AnswerId, e._StudentAnswer });
			modelBuilder.Entity<QuestionCorrectAnswer>()
				.HasKey(e => new { e.QuestionId,e.CorrectAnswer });
           modelBuilder.Entity<QuizStudent>()
                .HasKey(p => new {p.StudentId,p.QuizId});
            modelBuilder.Entity<QuizStudent>()
                .HasOne(p => p.Student)
                .WithMany(p => p.quizStudents)
                .HasForeignKey(p=>p.StudentId);
            modelBuilder.Entity<QuizStudent>()
               .HasOne(p => p.Quiz)
               .WithMany(p => p.QuizStudents)
               .HasForeignKey(p => p.QuizId);
            modelBuilder.Entity<QuizQuestion>()
                .HasKey(p => new { p.QuestionId, p.QuizId });
            modelBuilder.Entity<QuizQuestion>()
                .HasOne(p => p.Question)
                .WithMany(p => p.QuizQuestions)
                .HasForeignKey(p => p.QuestionId);
			modelBuilder.Entity<QuizQuestion>()
			  .HasOne(p => p.Quiz)
			  .WithMany(p => p.QuizQuestions)
			  .HasForeignKey(p => p.QuizId);
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
        public DbSet<QuizQuestion>QuizQuestions { get; set; }
        public DbSet<QuizStudent>QuizStudents { get; set; }

	}
}
