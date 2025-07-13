using System;

namespace SkillSync.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public string LearnerId { get; set; } // FK to IdentityUser.Id

        public string ReportData { get; set; } // Raw or serialized JSON
        public string Details { get; set; }    // Human-readable notes or interpretation
        public bool GeneratedBySystem { get; set; }

        public LearnerProfile Learner { get; set; }
    }
}