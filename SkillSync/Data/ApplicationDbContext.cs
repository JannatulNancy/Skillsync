using Microsoft.EntityFrameworkCore;
using SkillSync.Models;

namespace SkillSync.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LearnerProfile> Learners { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LearningList> LearningLists { get; set; }
        public DbSet<Progress> ProgressRecords { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Learner ↔ LearningList
            modelBuilder.Entity<LearningList>()
                .HasOne(ll => ll.Learner)
                .WithMany(l => l.LearningLists)
                .HasForeignKey(ll => ll.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LearningList>()
                .HasOne(ll => ll.Skill)
                .WithMany(s => s.LearningLists)
                .HasForeignKey(ll => ll.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Progress ↔ LearningList
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.LearningList)
                .WithMany(ll => ll.ProgressRecords)
                .HasForeignKey(p => p.LearningListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Skill)
                .WithMany(s => s.ProgressRecords)
                .HasForeignKey(p => p.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Recommendation
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Learner)
                .WithMany(l => l.Recommendations)
                .HasForeignKey(r => r.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Skill)
                .WithMany(s => s.Recommendations)
                .HasForeignKey(r => r.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reminder
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Learner)
                .WithMany(l => l.Reminders)
                .HasForeignKey(r => r.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Report
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Learner)
                .WithMany(l => l.Reports)
                .HasForeignKey(r => r.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LearningPath (Admin or Learner created)
            modelBuilder.Entity<LearningPath>()
                .HasOne(lp => lp.Learner)
                .WithMany(l => l.LearningPaths)
                .HasForeignKey(lp => lp.LearnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LearningPath>()
                .HasOne(lp => lp.Admin)
                .WithMany(a => a.LearningPaths)
                .HasForeignKey(lp => lp.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Skill creator (Admin)
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Admin)
                .WithMany(a => a.Skills)
                .HasForeignKey(s => s.AdminId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
