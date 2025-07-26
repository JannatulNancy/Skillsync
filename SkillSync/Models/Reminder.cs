using System;

namespace SkillSync.Models
{
    public class Reminder
    {
        public int ReminderId { get; set; }

        public string LearnerId { get; set; } // FK to IdentityUser.Id
        public string Message { get; set; }
        public string Priority { get; set; } // Consider using enum if priorities expand

        public DateTime DueDate { get; set; }
        public DateTime DateSent { get; set; }

        public int? SkillId { get; set; } // ✅ Optional FK (can be null)
        public int? LearningPathId { get; set; } // ✅ Optional FK

        public LearnerProfile Learner { get; set; }
        public Skill Skill { get; set; }
        public LearningPath LearningPath { get; set; }
    }
}