using System;

namespace SkillSync.Models
{
    public class Recommendation
    {
        public int RecommendationId { get; set; }

        public int SkillId { get; set; }
        public string LearnerId { get; set; } // ✅ Added for proper FK mapping
        public string Reason { get; set; }
        public DateTime Date { get; set; }

        public LearnerProfile Learner { get; set; }
        public Skill Skill { get; set; }
    }
}