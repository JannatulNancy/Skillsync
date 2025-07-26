using System;

namespace SkillSync.Models
{
    public class Progress
    {
        public int ProgressId { get; set; }
        public int LearningListId { get; set; }
        public int SkillId { get; set; }
        public int Completion { get; set; } // Value from 0 to 100 (percentage)
        public DateTime LastUpdate { get; set; }

        public LearningList LearningList { get; set; }
        public Skill Skill { get; set; }
    }
}