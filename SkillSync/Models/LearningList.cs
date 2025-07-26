using System;
using System.Collections.Generic;

namespace SkillSync.Models
{
    public class LearningList
    {
        public int LearningListId { get; set; }
        public string LearnerId { get; set; }
        public int SkillId { get; set; }
        public string Status { get; set; }
        public DateTime AddedOn { get; set; }

        public LearnerProfile Learner { get; set; }
        public Skill Skill { get; set; }
        public ICollection<Progress> ProgressRecords { get; set; }
    }
}