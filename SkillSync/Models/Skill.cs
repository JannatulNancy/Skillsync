using System;
using System.Collections.Generic;

namespace SkillSync.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Level { get; set; } // Beginner, Intermediate, Advanced

        public ICollection<LearningList> LearningLists { get; set; }
        public ICollection<Progress> ProgressRecords { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<Reminder> Reminders { get; set; } // ✅ Added for reminder mapping
    }
}