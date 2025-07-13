using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillSync.Models;
using SkillSync.Models;

namespace SkillSync.Data
{
    public class ApplicationDbContext : IdentityDbContext<LearnerProfile, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LearnerProfile> Learners { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LearningList> LearningLists { get; set; }
        public DbSet<Progress> ProgressRecords { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }

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

            // Progress ↔ Skill
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Skill)
                .WithMany(s => s.ProgressRecords)
                .HasForeignKey(p => p.SkillId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade conflict

            // Recommendation ↔ Learner
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Learner)
                .WithMany(l => l.Recommendations)
                .HasForeignKey(r => r.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Recommendation ↔ Skill
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Skill)
                .WithMany(s => s.Recommendations)
                .HasForeignKey(r => r.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reminder ↔ Learner
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Learner)
                .WithMany(l => l.Reminders)
                .HasForeignKey(r => r.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reminder ↔ Skill
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Skill)
                .WithMany(s => s.Reminders)
                .HasForeignKey(r => r.SkillId)
                .OnDelete(DeleteBehavior.SetNull);

            // Reminder ↔ LearningPath
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.LearningPath)
                .WithMany(lp => lp.Reminders)
                .HasForeignKey(r => r.LearningPathId)
                .OnDelete(DeleteBehavior.SetNull);

            // Report ↔ Learner
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Learner)
                .WithMany(l => l.Reports)
                .HasForeignKey(r => r.LearnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LearningPath ↔ Learner
            modelBuilder.Entity<LearningPath>()
                .HasOne(lp => lp.Learner)
                .WithMany(l => l.LearningPaths)
                .HasForeignKey(lp => lp.LearnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}