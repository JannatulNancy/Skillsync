using System;

namespace SkillSync.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }

        public int? AdminId { get; set; }
        public Admin Admin { get; set; }

        public ICollection<LearningList> LearningLists { get; set; }
        public ICollection<Progress> ProgressRecords { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }

}
